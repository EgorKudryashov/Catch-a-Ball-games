using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public void SelectCatchABall()
    {
        SceneManager.LoadScene("CatchABall");
    }

    public void SelectWhackABall()
    {
        SceneManager.LoadScene("WhackABall");
    }
}
