using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    [SerializeField] GameObject pauseMenu;


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            {
            Debug.Log("Painettiin esciä");
            PauseGame();
            }
    }
    public void PauseGame()
    {
        FindObjectOfType<PlayerController>().active = false;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        FindObjectOfType<PlayerController>().active = true;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Quit()
    {

    }


}
