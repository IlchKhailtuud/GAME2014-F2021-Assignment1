using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    public void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    public void GoToScene(string scene)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }
}
