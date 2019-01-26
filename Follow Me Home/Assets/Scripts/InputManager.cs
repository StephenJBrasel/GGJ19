using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
  private static Dictionary<int, KeyCode> keyCodesForMouseButtons = new Dictionary<int,KeyCode>
  {
    { 0, KeyCode.Mouse0 },
    { 1, KeyCode.Mouse1 },
    { 2, KeyCode.Mouse2 },
    { 3, KeyCode.Mouse3 },
    { 4, KeyCode.Mouse4 },
    { 5, KeyCode.Mouse5 },
    { 6, KeyCode.Mouse6 }
  };

  private static InputManager _liaison;

  public static bool enabled = true;
  public delegate void KeyEventCallback();
  private Dictionary<KeyCode, KeyEventCallback> _keyActivatedEventRegistry = new Dictionary<KeyCode,KeyEventCallback>();
  private Dictionary<KeyCode, KeyEventCallback> _keyDeactivatedEventRegistry = new Dictionary<KeyCode,KeyEventCallback>();
  private Dictionary<KeyCode, KeyEventCallback> _keyActiveEventRegistry = new Dictionary<KeyCode,KeyEventCallback>();
  public delegate void ScrollEventCallback( Vector2 delta );
  private ScrollEventCallback _scrollEventRegistry;
  public delegate void MouseMoveEventCallback( Vector2 delta );
  private MouseMoveEventCallback _mouseMoveEventRegistry;

  private static Queue<Event> _inputEventQueue = new Queue<Event>();

  private static HashSet<KeyCode> _activeKeys = new HashSet<KeyCode>();

  private static KeyCode MouseButtonToKeyCode( int mouseButton )
  {
    KeyCode keyCode = new KeyCode();
    keyCodesForMouseButtons.TryGetValue(mouseButton, out keyCode);
    return keyCode;
  }

  private static void ReferenceLiaison()
  {
    if( _liaison == null )
    {
      _liaison = new GameObject( "Input Manager Liaison", typeof(InputManager) ).GetComponent<InputManager>();
    }
  }

  private static void DereferenceLiaison()
  {
    if(_liaison != null
      && _liaison._keyActivatedEventRegistry.Count == 0
      && _liaison._keyDeactivatedEventRegistry.Count == 0
      && _liaison._keyActiveEventRegistry.Count == 0
      && _liaison._scrollEventRegistry == null
      && _liaison._mouseMoveEventRegistry == null )
    {
      DestroyImmediate( _liaison );
    }
  }

  private static void RegisterKey( Dictionary<KeyCode, KeyEventCallback> registry, KeyCode keyCode, KeyEventCallback callback )
  {
    if( registry.ContainsKey(keyCode) )
    {
      registry[keyCode] += callback;
    }
    else
    {
      registry.Add( keyCode, callback );
    }
  }

  private static void UnregisterKey( Dictionary<KeyCode, KeyEventCallback> registry, KeyCode keyCode, KeyEventCallback callback )
  {
    if( registry.ContainsKey(keyCode) )
    {
      registry[keyCode] -= callback;
      if( registry[keyCode] == null )
      {
        registry.Remove(keyCode);
      }
    }
  }

  public static void RegisterKeyActivated( KeyCode keyCode, KeyEventCallback callback )
  {
    ReferenceLiaison();
    RegisterKey(_liaison._keyActivatedEventRegistry, keyCode, callback );
  }

  public static void RegisterKeyDeactivated( KeyCode keyCode, KeyEventCallback callback )
  {
    ReferenceLiaison();
    RegisterKey(_liaison._keyDeactivatedEventRegistry, keyCode, callback );
  }

  public static void RegisterKeyActive( KeyCode keyCode, KeyEventCallback callback )
  {
    ReferenceLiaison();
    RegisterKey(_liaison._keyActiveEventRegistry, keyCode, callback );
  }

  public static void RegisterScroll( ScrollEventCallback callback )
  {
    ReferenceLiaison();
    _liaison._scrollEventRegistry += callback;
  }

  public static void RegisterMouseMove( MouseMoveEventCallback callback )
  {
    ReferenceLiaison();
    _liaison._mouseMoveEventRegistry += callback;
  }

  public static void UnregisterKeyActivated( KeyCode keyCode, KeyEventCallback callback )
  {
    UnregisterKey(_liaison._keyActivatedEventRegistry, keyCode, callback );
    DereferenceLiaison();
  }

  public static void UnregisterKeyDeactivated( KeyCode keyCode, KeyEventCallback callback )
  {
    UnregisterKey(_liaison._keyDeactivatedEventRegistry, keyCode, callback );
    DereferenceLiaison();
  }

  public static void UnregisterKeyActive( KeyCode keyCode, KeyEventCallback callback )
  {
    UnregisterKey(_liaison._keyActiveEventRegistry, keyCode, callback );
    DereferenceLiaison();
  }

  public static void UnregisterScroll( ScrollEventCallback callback )
  {
    _liaison._scrollEventRegistry -= callback;
    DereferenceLiaison();
  }

  public static void UnregisterMouseMove( MouseMoveEventCallback callback )
  {
    _liaison._mouseMoveEventRegistry -= callback;
    DereferenceLiaison();
  }

  public static bool IsKeyActive( KeyCode keyCode )
  {
    return _activeKeys.Contains( keyCode );
  }

  void OnDestroy()
  {
    if ( _liaison == gameObject )
    {
      _liaison = null;
    }
  }

  void OnGUI()
  {
    if(enabled == false)
    {
      return;
    }
    
    switch( Event.current.type )
    {
    case EventType.KeyDown:
      if( _keyActivatedEventRegistry.ContainsKey(Event.current.keyCode)
          || _keyActiveEventRegistry.ContainsKey(Event.current.keyCode) )
      {
        _inputEventQueue.Enqueue( new Event(Event.current) );
      }
      break;
    case EventType.KeyUp:
      if( _keyDeactivatedEventRegistry.ContainsKey(Event.current.keyCode)
          || _keyActiveEventRegistry.ContainsKey(Event.current.keyCode) )
      {
        _inputEventQueue.Enqueue( new Event(Event.current) );
      }
      break;
    case EventType.MouseDown:
      Event.current.keyCode = MouseButtonToKeyCode( Event.current.button );
      if( _keyActivatedEventRegistry.ContainsKey(Event.current.keyCode)
          || _keyActiveEventRegistry.ContainsKey(Event.current.keyCode) )
      {
        _inputEventQueue.Enqueue( new Event(Event.current) );
      }
      break;
    case EventType.MouseUp:
      Event.current.keyCode = MouseButtonToKeyCode( Event.current.button );
      if( _keyDeactivatedEventRegistry.ContainsKey(Event.current.keyCode)
          || _keyActiveEventRegistry.ContainsKey(Event.current.keyCode) )
      {
        _inputEventQueue.Enqueue( new Event(Event.current) );
      }
      break;
    case EventType.ScrollWheel:
      if( _scrollEventRegistry != null )
      {
        _inputEventQueue.Enqueue( new Event(Event.current) );
      }
      break;
    default:
      if( (_mouseMoveEventRegistry != null && Event.current.isMouse && Event.current.delta != Vector2.zero)
          || _activeKeys.Contains(KeyCode.LeftShift) != Event.current.shift)
      {
        _inputEventQueue.Enqueue( new Event(Event.current) );
      }
      break;
    }
  }

  void FixedUpdate()
  {
    while( _inputEventQueue.Count > 0 )
    {
      Event inputEvent = _inputEventQueue.Dequeue();
      switch( inputEvent.type )
      {
      case EventType.KeyDown:
      case EventType.MouseDown:
        if( _keyActivatedEventRegistry.ContainsKey(inputEvent.keyCode)
            && _keyActivatedEventRegistry[inputEvent.keyCode] != null )
        {
          _keyActivatedEventRegistry[inputEvent.keyCode]();
        }
        if( _activeKeys.Contains(inputEvent.keyCode) == false )
        {
          _activeKeys.Add( inputEvent.keyCode );
        }
        break;
      case EventType.KeyUp:
      case EventType.MouseUp:
        if( _keyDeactivatedEventRegistry.ContainsKey(inputEvent.keyCode)
            && _keyDeactivatedEventRegistry[inputEvent.keyCode] != null )
        {
          _keyDeactivatedEventRegistry[inputEvent.keyCode]();
        }
        if( _activeKeys.Contains(inputEvent.keyCode) == true )
        {
          _activeKeys.Remove( inputEvent.keyCode );
        }
        break;
      case EventType.ScrollWheel:
        if( _scrollEventRegistry != null )
        {
          _scrollEventRegistry( inputEvent.delta );
        }
        break;
      default:
        if( _mouseMoveEventRegistry != null )
        {
          _mouseMoveEventRegistry( inputEvent.delta );
        }
        if( _activeKeys.Contains(KeyCode.LeftShift) )
        {
          if(inputEvent.shift == false)
          {
            if( _keyDeactivatedEventRegistry.ContainsKey(KeyCode.LeftShift)
                && _keyDeactivatedEventRegistry[KeyCode.LeftShift] != null )
            {
              _keyDeactivatedEventRegistry[KeyCode.LeftShift]();
            }
            if( _keyDeactivatedEventRegistry.ContainsKey(KeyCode.RightShift)
                && _keyDeactivatedEventRegistry[KeyCode.RightShift] != null )
            {
              _keyDeactivatedEventRegistry[KeyCode.RightShift]();
            }
            _activeKeys.Remove( KeyCode.LeftShift );
            _activeKeys.Remove( KeyCode.RightShift );
          }
        }
        else
        {
          if(inputEvent.shift == true)
          {
            if( _keyActivatedEventRegistry.ContainsKey(KeyCode.LeftShift)
                && _keyActivatedEventRegistry[KeyCode.LeftShift] != null )
            {
              _keyActivatedEventRegistry[KeyCode.LeftShift]();
            }
            if( _keyActivatedEventRegistry.ContainsKey(KeyCode.RightShift)
                && _keyActivatedEventRegistry[KeyCode.RightShift] != null )
            {
              _keyActivatedEventRegistry[KeyCode.RightShift]();
            }
            _activeKeys.Add( KeyCode.LeftShift );
            _activeKeys.Add( KeyCode.RightShift );
          }
        }
        break;
      }
    }

    foreach( KeyCode keyCode in _activeKeys )
    {
      if( _keyActiveEventRegistry.ContainsKey(keyCode)
          && _keyActiveEventRegistry[keyCode] != null )
      {
        _keyActiveEventRegistry[keyCode]();
      }
    }
  }
}
