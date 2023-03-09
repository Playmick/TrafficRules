using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Load3dScene : MonoBehaviour
{
    [SerializeField] string sceneName = "Perekrestok";
    // Start is called before the first frame update
    void Awake() => SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
}
