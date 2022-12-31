using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//tallentamista ja lataamista varten
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //GAME MANAGER METRO OMA PELI
    public int playerPoints = 0;
    public bool gamePaused = false;
    public int finishedLevel;
    public string savename = "";

    //player data
    public float playerMaxHealth = 100;
    public float playerCurrentHealth;
    public float playerArmorValue = 1;
    public float playerDamageMultiplier = 1;
    public float playerRofMultiplier = 1;
   public Weapon playerWeapon = null;
    public string[] levels;
    public string nextLevel;
    public float rateOfFire = 1;

    public static GameManager manager;
    // Start is called before the first frame update

    public void Awake()
    {
        //singleton
        //katotaan onko manageria jo olemassa
        if (manager == null)
        {
            //jos ei manageria, tämä luokka on manageria
            //kerrotaan ,ettei manageri saa tuhoutua scenen vaihtuessa
            DontDestroyOnLoad(gameObject);
            manager = this;
        }
        else
        {
            //jos on valmiiksi olemassa manageri
            //tuhotaan tämä manageri
            Destroy(gameObject);
        }
    }
    void Start()
    {
        levels = new string[] {"Scene1","Scene2", "Scene3", "EndScene"};
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPoints(int points)
    {
        //playerPoints =+ points;
    }

    public int GetPoints()
    {
        return playerPoints;
    }

    public void Save()
    {
        Debug.Log("Seivataan" + savename);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/" + savename +".dat");
        PlayerData data = new PlayerData();

        data.health = playerMaxHealth;
        //data.armorValue = playerArmorValue;
        data.playerDamageMultiplier = playerDamageMultiplier;
        data.playerRofMultiplier = playerRofMultiplier;
        data.points = playerPoints;
        data.level = finishedLevel;
        data.savedgameId = savename;

        bf.Serialize(file, data);
        file.Close();

        Debug.Log("seivattu nimi " + data.savedgameId);
        Debug.Log("healthi " + playerMaxHealth + data.health);
    }

    public void Load(string loadgame)
    {
        Debug.Log("Loadataan " + loadgame);
        Debug.Log("seivin nimi " + savename);
        if(File.Exists(Application.persistentDataPath + "/" + loadgame + ".dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file =
                File.Open(Application.persistentDataPath + "/" + loadgame + ".dat", FileMode.Open);

            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            playerMaxHealth = data.health;
            //playerArmorValue = data.armorValue;
            playerRofMultiplier = data.playerRofMultiplier;
            playerDamageMultiplier = data.playerDamageMultiplier;
            playerPoints = data.points;
            finishedLevel = data.level;
            savename = data.savedgameId;
        }
    }

    public void SetSaveSlot(string name)
    {
        savename = name;
    }

    public void LoadScene(string scenename)
    {
        Debug.Log("ladataan scene " + scenename);
        SceneManager.LoadScene(scenename);
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(nextLevel);
    }


}



[Serializable]
class PlayerData
{
    public float health;
    public float armorValue;
    public float playerRofMultiplier;
    public float playerDamageMultiplier;
    public int points;
    public int level;
    public string savedgameId;

}
