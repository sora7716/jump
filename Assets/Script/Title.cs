using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public string _sceneName = "GameScene";

    public void LoadScene()
    {
        Debug.Log("Load Scene");
        SceneManager.LoadScene(_sceneName);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
