using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIcontroller : MonoBehaviour
{
    public void MoveToScene(string name)
    {
        SceneManager.LoadScene(name);
    }
    // Start is called before the first frame update
    public void Exit()
    {
        Application.Quit();
    }
}
