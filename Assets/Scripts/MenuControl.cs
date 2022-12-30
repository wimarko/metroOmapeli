using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    public void LoadLevel(string level)
    {
        SceneManager.LoadScene(level);
        Debug.Log("Ladataab leveli " + level);
    }

    public void Save ()
    {
        GameManager.manager.Save();
    }

    public void Load (string savename)
    {
        GameManager.manager.Load(savename);
    }
}
