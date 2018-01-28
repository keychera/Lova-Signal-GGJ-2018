using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickToNextScene : MonoBehaviour
{
    public string whatScene;
    public bool exitGame = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!exitGame)
            {
                SceneManager.LoadScene(whatScene);
            }
            else
            {
				Application.Quit();
            }
        }
    }
}
