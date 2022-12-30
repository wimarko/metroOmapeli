using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class SceneSettings : MonoBehaviour
{

    public int enemiesLeft = 10;

    [SerializeField] GameObject[] dropPoints;
    [SerializeField] GameObject[] dropObjects;
    [SerializeField] float droptimer = 20f;
    [SerializeField] float lifetime = 2f;
    float counter;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.manager.enemyCount = enemiesLeft;
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

        GameObject droppedObject = Instantiate(dropObjects[num], dropPoints[dropIndex].transform.position, dropPoints[dropIndex].transform.rotation);

        Destroy(droppedObject, lifetime);
    }
}
