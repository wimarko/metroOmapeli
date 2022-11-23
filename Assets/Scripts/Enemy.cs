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
    [SerializeField] float attackSpeed = 1f;
    
    [SerializeField] float attackRange = 1;



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
    
    //Aiheutetaan damagea
    //TODO muuta vain et�isyydeksi.. kuten corecombatcreatorissa?
    //T�m� chasemodeen tai omaan modeensa.. kuten attack Range? 
    //kuuluu menn� Updateen "jos rangessa ja voi hy�k�t�"
    private void OnTriggerStay(Collider other)
    {
        /*
        if(other.gameObject.tag == "Player")
        {
            if (timeSinceLastAttack > attackSpeed)
            {
                Debug.Log("vihu t�rm�si pelaajaan");
                other.GetComponentInChildren<PlayerController>().TakeDamage(damage);
                timeSinceLastAttack = 0;
            }
            
        }
        else
        {
            Debug.Log("T�rm�ttiin vaan");
            Debug.Log("kohde: "+other.GetComponentInChildren<PlayerController>());
            Debug.Log("sen Tagi " + other.gameObject.tag);
        }
        */
    }


    private void causeDamage(GameObject target)
    {
        //t�h�n timerin perusteella aiheuttaa aina v�lill� damagea targetille
        //eli tarvitsee jonkun timerin ja kohteen(playerin)

    }

    private bool InAttackRange(Collider other)
    {
        float distanceToPlayer = Vector3.Distance(other.transform.position, transform.position);
        return distanceToPlayer < attackRange; 
    }

    public float GetAttackRange()
    {
        return attackRange;
    }

    public float GetAttackSpeed()
    {
        return attackSpeed;
    }

    public float GetDamage()
    {
        return damage;
    }

    private void OnDrawGizmos()
    {   //AttackRangen pituinen raycast, (pit�� osua playerin keskustaan asti kai)
        Gizmos.DrawRay(transform.position, transform.forward * attackRange);
    }
}
