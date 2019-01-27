using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Obstacle : MonoBehaviour
{
    public enum Type
    {
        BikeRack,
        FireHydrant,
        Scaffold,
        ShopSign,
        TrashCan
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public Type GetObstacleType()
	{
		string current = transform.GetChild(0).name;
		for (int i = 0; i < transform.childCount; ++i)
		{
			var child = transform.GetChild(i);
			if (child.gameObject.activeSelf)
			{
				current = child.name;
				break;
			}
		}
		return (Obstacle.Type)System.Enum.Parse(typeof(Obstacle.Type), current);
	}
}

#if UNITY_EDITOR

[CustomEditor(typeof(Obstacle))]
public class ObstacleEditor : Editor 
{
    public override void OnInspectorGUI()
    {
        Obstacle myTarget = (Obstacle)target;
        
        var type = EditorGUILayout.EnumPopup("Type", myTarget.GetObstacleType(), GUIStyle.none, null);
        for (int j = 0; j < myTarget.transform.childCount; ++j)
        {
            var child = myTarget.transform.GetChild(j);
            child.gameObject.SetActive(child.name == type.ToString());
        }
    }
}

#endif
