using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class button : MonoBehaviour
{
    public string sceneName = "";
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnMouseDown()
    {
        if (sceneName != "")
        {
            Debug.Log("change scene");
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.Log("no scene");
        }

    }
}
