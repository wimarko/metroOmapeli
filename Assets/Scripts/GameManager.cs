using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager manager;
    // Start is called before the first frame update

    public void Awake()
    {
        //singleton
        //katotaan onko manageria jo olemassa
        if (manager == null)
        {
            //jos ei manageria, t�m� luokka on manageria
            //kerrotaan ,ettei manageri saa tuhoutua scenen vaihtuessa
            DontDestroyOnLoad(gameObject);
            manager = this;
        }
        else
        {
            //jos on valmiiksi olemassa manageri
            //tuhotaan t�m� manageri
            Destroy(gameObject);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Save()
    {
        Debug.Log("Seivataan");
    }

    public void Load()
    {
        Debug.Log("Loadataan");
    }
}
