using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.PlayerSettings;

public class SceneSettings : MonoBehaviour
{

    public int enemiesLeft = 10;

    [SerializeField] GameObject[] dropPoints;
    [SerializeField] GameObject[] dropObjects;
    [SerializeField] float droptimer = 20f;
    [SerializeField] float lifetime = 2f;
    [SerializeField] GameObject[] enemySpawn;
    [SerializeField] float enemySpawnTime;
    [SerializeField] int enemies = 10;
    float enemytimer;
    float counter;
    [SerializeField] string nextLevel;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.manager.Load(GameManager.manager.savename);
        GameManager.manager.nextLevel = nextLevel;
    }

    // Update is called once per frame
    void Update()
    {
        if(counter < droptimer)
        {
            counter += Time.deltaTime;
            
        }
        else
        {
            SpawnObject();
            counter = 0;
        }
    }

    public void SpawnObject()
    {
        Debug.Log("Spawnataab objekti");
        int num = Random.Range(0, dropObjects.Length);
        Debug.Log("randomi " + num);

        int dropIndex = Random.Range(0, dropPoints.Length);

        GameObject droppedObject =
            Instantiate(dropObjects[num], dropPoints[dropIndex].transform.position, dropPoints[dropIndex].transform.rotation);

        Destroy(droppedObject, lifetime);
    }

    public void EnemyEliminated()
    {
        enemies--;
        if(enemies <= 0)
        {
            GameManager.manager.Save();
            SceneManager.LoadScene("RewardScene");
        }
    }
}
