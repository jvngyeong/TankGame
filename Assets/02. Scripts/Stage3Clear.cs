using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage3Clear : MonoBehaviour
{
    public void OnClickButton1()
    {
        SceneManager.LoadScene("Intro");
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
