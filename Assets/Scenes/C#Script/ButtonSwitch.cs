using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class Button : MonoBehaviour
{
    public string sceneName = "";
    void Update(){
        if (Input.GetKeyDown(KeyCode.Space)){
        SceneManager.LoadScene(sceneName);
        }
    }
}
