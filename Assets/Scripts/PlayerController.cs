using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;


public class PlayerController : MonoBehaviour
{
    private CharacterController controller;

    float moveSpeed;
    [SerializeField] float startingSpeed;
    [SerializeField] float rotateSpeed;
    [SerializeField] float startingHealth = 200;
    public float currentHealth;
    [SerializeField] float armorValue = 0;
    public float damageMultiplier = 1;
    public bool active = true;
    public bool boosted = false;
    public float boostTime = 0f;

    [SerializeField] Weapon defaultWeapon = null;  //käytössä oleva ase
    [SerializeField] Transform holdingTransform = null; //kohta mistä pidetään asetta kiinni

    public Image heatlhFilled;

    //stringint nopeuttamaan, ehkä turhaan
    private string horizontal = "Horizontal";
    private string vertical = "Vertical";
    [SerializeField] float rateOfFire = 1;
    private float firePause;
    public GameObject ammo;
    public GameObject ammoSpawn;
    Camera mainCamera;
    public float rofMultiplier = 1; //adjust speed of fire, bigger = faster

    Weapon currentWeapon = null;
    [SerializeField] MouseTarget mouseTarget;

    

    private void Start()
    {

        startingHealth = GameManager.manager.playerMaxHealth;
        controller = gameObject.AddComponent<CharacterController>();
        mainCamera = FindObjectOfType<Camera>();
        EquipWeapon(defaultWeapon);
        currentHealth = startingHealth;
        moveSpeed = startingSpeed;
        
    }

    

    void Update()
    {
        if (active)
        {
            if (firePause > 0)
            {
                firePause -= Time.deltaTime;
            }

            Move();

            if (Input.GetButton("Fire1"))
            {
                Shoot();
            }
        }
        if(boosted)
        {
            boostTime -= Time.deltaTime;
            if (boostTime < 0)
            {
                moveSpeed = startingSpeed;
                boosted = false;
            }
            
        }
        
    }

    private void Move()
    {
        float xMovement = Input.GetAxis(horizontal) * moveSpeed * Time.deltaTime;
        float yMovement = Input.GetAxis(vertical) * moveSpeed * Time.deltaTime;

        transform.Translate(xMovement, 0, yMovement, Space.World);

        transform.LookAt(new Vector3(mouseTarget.transform.position.x, 0f, mouseTarget.transform.position.z));
    }

    public void Shoot()
    {
        //Debug.Log("SHOOT");
        if(firePause <= 0)
        {
            currentWeapon.LaunchProjectile(ammoSpawn.transform, damageMultiplier);
            firePause = rateOfFire / rofMultiplier;
        }
        
    }

    public void EquipWeapon(Weapon newWeapon)
    {
        if(newWeapon == null)
        {
            //Debug.Log("Ei ole asetta");
            return;
        }
        currentWeapon = newWeapon;
        //Debug.Log("on ase");
       newWeapon.Spawn(holdingTransform);
        //Instantiate(equippedWeapon, holdingTransform);
    }

    public void SetRateOfFire(float rof)
    {
        rateOfFire = rof;
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("pelaaja vahingoittui");
        float finalDamage = damage;// armorValue;
        currentHealth -= finalDamage;
        heatlhFilled.fillAmount = currentHealth / startingHealth;
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("Pelaaja kuoli");
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("pelaaja osui johonkin");
        if (collision.gameObject.CompareTag("Ansa"))
        {
            Debug.Log("Osuttiin ansaan");
            TakeDamage(10);
        }
    }

    public void RepairArmor(int amount)
    {
        currentHealth += amount;
        if(currentHealth > startingHealth)
        {
            currentHealth = startingHealth;
        }
    }

    private void OnTriggerEnter(Collider other)
    {  
        string tag = other.tag;
        switch(tag)
        {
            case "RepairPack":
                if (currentHealth < startingHealth)
                {
                    //repairpack kertoo paljonko korjataan armouria(healthia)
                    RepairArmor(other.GetComponentInParent<RepairPack>().GetRepairAmount());
                    Destroy(other.gameObject);
                    heatlhFilled.fillAmount = currentHealth / startingHealth;
                }
                break;

            case "SpeedBoost":
                if(!boosted)
                {
                    //Annetaan BoostSpeed-metodille parametreiksi boostin määrä ja aika, boosteriobjektista
                    BoostSpeed(other.GetComponentInParent<SpeedBooster>().GetSpeedBoost()
                        , other.GetComponentInParent<SpeedBooster>().GetBoostTime());
                    Destroy(other.gameObject);
                }   
                break;
        }

    }

    public void BoostSpeed(float amount, float time )
    {
        moveSpeed = amount * moveSpeed;
        boostTime = time;
        boosted = true;

    }

    public void SavePlayerData()
    {
        GameManager.manager.playerMaxHealth = startingHealth;
        GameManager.manager.playerArmorValue = armorValue;
        GameManager.manager.playerDamageMultiplier = damageMultiplier;
        GameManager.manager.playerRofMultiplier = rofMultiplier;
        GameManager.manager.playerWeapon = currentWeapon;
    }



}
