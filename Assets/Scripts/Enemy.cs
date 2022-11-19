using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamaegable
{



    [SerializeField] float health = 10;
    //[SerializeField] PatrolPath patrolPath = null;
    [SerializeField] float damage = 5;
    [SerializeField] int pointsValue = 5;



    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
 
    }

   

    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
