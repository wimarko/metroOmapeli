using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;

    [SerializeField] float moveSpeed;
    [SerializeField] float rotateSpeed;

    [SerializeField] Weapon defaultWeapon = null;  //k�yt�ss� oleva ase
    [SerializeField] Transform holdingTransform = null; //kohta mist� pidet��n asetta kiinni

    //stringint nopeuttamaan, ehk� turhaan
    private string horizontal = "Horizontal";
    private string vertical = "Vertical";

    public GameObject ammo;
    public GameObject ammoSpawn;
    Camera mainCamera;

    Weapon currentWeapon = null;

    private void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
        mainCamera = FindObjectOfType<Camera>();
        EquipWeapon(defaultWeapon);
    }

    

    void Update()
    {
        Move();

        if(Input.GetButtonDown("Fire1"))
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
        Debug.Log("SHOOT");
        /*
        GameObject ammoInstance = Instantiate(ammo, ammoSpawn.transform.position, Quaternion.identity);
        
        ammoInstance.GetComponent<Rigidbody>().AddForce(ammoSpawn.transform.forward * 400);
        Destroy(ammoInstance, 3);
        */
        currentWeapon.LaunchProjectile(ammoSpawn.transform);
    }

    public void EquipWeapon(Weapon newWeapon)
    {
        if(newWeapon == null)
        {
            Debug.Log("Ei ole asetta");
            return;
        }
        currentWeapon = newWeapon;
        Debug.Log("on ase");
       newWeapon.Spawn(holdingTransform);
        //Instantiate(equippedWeapon, holdingTransform);
    }




}
