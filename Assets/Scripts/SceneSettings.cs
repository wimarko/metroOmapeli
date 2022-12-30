using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSettings : MonoBehaviour
{

    public int enemiesLeft = 10;

    [SerializeField] GameObject[] dropPoints;
    [SerializeField] GameObject[] dropObjects;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.manager.enemyCount = enemiesLeft;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
