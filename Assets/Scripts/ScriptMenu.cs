using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptMenu : MonoBehaviour
{
    public void MudarScene(int idScene)
    {
        SceneManager.LoadScene(idScene);
    }
}
