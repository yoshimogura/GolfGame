using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class Button : MonoBehaviour
{
    public string sceneName = "";

    void Start(){
        
    }
    public void SwitchScene()
    {
        SceneManager.LoadScene(sceneName);
    }


}
