using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mainmenu : MonoBehaviour
{
    public string SceneName;
    public string Level1;
    public string Level2;
    public string Level3;
   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void play()
    {
        SceneManager.LoadScene(SceneName);
    }

    void options()
    {
       
    }

    public void one()
    {
        SceneManager.LoadScene(Level1);
    }
    public void two()
    {
        SceneManager.LoadScene(Level2);
    }

    public void three()
    {
        SceneManager.LoadScene(Level3);
    }

    public void quit()
    {
        Application.Quit();
        Debug.Log("Quitting");
    }
}
