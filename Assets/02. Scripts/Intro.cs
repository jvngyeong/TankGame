using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    public void OnClickButton1()
    {
        SceneManager.LoadScene("Stage1");
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
