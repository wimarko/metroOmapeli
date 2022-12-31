using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RewardScene : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] float fasterROF;
    [SerializeField] float moreHealth;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseRof()
    {
        GameManager.manager.playerRofMultiplier += fasterROF;
        Debug.Log("updated ROF " + GameManager.manager.playerRofMultiplier);
        GameManager.manager.LoadNextScene();
    }

    public void IncreaseHealth()
    {
        GameManager.manager.playerMaxHealth += moreHealth;
        Debug.Log("updated health " + GameManager.manager.playerMaxHealth);
        //GameManager.manager.LoadNextScene();
        SceneManager.LoadScene(GameManager.manager.nextLevel);
    }
}
