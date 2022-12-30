using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] float speed = 5;
    [SerializeField] GameObject model;
    //[SerializeField] GameObject model = null;
    // Start is called before the first frame update
    [SerializeField]float  lifeTime = 3;
    [SerializeField] float projectileDamage = 10;
    public float damageMultiplier = 1;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Osuttiin vihuun");
            collision.gameObject.GetComponent<Enemy>().TakeDamage(projectileDamage * damageMultiplier);
            Destroy(gameObject);
        }
    }
}
