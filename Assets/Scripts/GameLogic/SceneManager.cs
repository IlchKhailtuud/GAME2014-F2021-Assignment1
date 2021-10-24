using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: SceneManager.cs
//Author: Yiliqi
//Student Number: 101289355
//Last Modified On : 10/2/2021
//Description : Class for scene transitions
////////////////////////////////////////////////////////////////////////////////////////////////////////

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
