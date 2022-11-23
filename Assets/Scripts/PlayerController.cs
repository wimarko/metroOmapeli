using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;

    [SerializeField] float moveSpeed;
    [SerializeField] float rotateSpeed;
    [SerializeField] int startingHealth = 200;
    public float currentHealth;
    [SerializeField] int armorValue = 0;
    public float damageMultiplier = 1;

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

    Weapon currentWeapon = null;

    private void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
        mainCamera = FindObjectOfType<Camera>();
        EquipWeapon(defaultWeapon);
        currentHealth = startingHealth;
    }

    

    void Update()
    {
        if(firePause >0)
        {
            firePause -= Time.deltaTime;
        }

        Move();

        if(Input.GetButton("Fire1"))
        {
            Shoot();
        }
        
    }

    private void Move()
    {
        float xMovement = Input.GetAxis(horizontal) * moveSpeed * Time.deltaTime;
        float yMovement = Input.GetAxis(vertical) * moveSpeed * Time.deltaTime;

        transform.Translate(xMovement, 0, yMovement, Space.World);

        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.cyan, 1f);

            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }
    }

    public void Shoot()
    {
        //Debug.Log("SHOOT");
        if(firePause <= 0)
        {
            currentWeapon.LaunchProjectile(ammoSpawn.transform);
            firePause = rateOfFire;
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
        float finalDamage = damage - (armorValue * damage);
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

}
