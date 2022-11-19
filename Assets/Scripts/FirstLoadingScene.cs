using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstLoadingScene : MonoBehaviour
{
    [SerializeField] int WaitingTime = 2;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GoToMainMenu());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator GoToMainMenu()
    {
        yield return new WaitForSeconds(WaitingTime);
        LoadScene();
    }

    private void LoadScene()
    {
        SceneManager.LoadScene("StartMenuScene");
    }

}
