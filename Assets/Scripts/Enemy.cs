using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamaegable
{



    [SerializeField] float health = 10;
    //[SerializeField] PatrolPath patrolPath = null;
    [SerializeField] float damage = 5;
    //[SerializeField] int pointsValue = 5;
    [SerializeField] float attackSpeed = 1f;
    
    [SerializeField] float attackRange = 1;

    SceneSettings sceneSettings;



    // Start is called before the first frame update
    void Start()
    {
        sceneSettings = FindObjectOfType<SceneSettings>();
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
        //GameManager.manager.AddPoints(pointsValue);
        Destroy(gameObject);
        sceneSettings.EnemyEliminated();
        
    }
    



    private void causeDamage(GameObject target)
    {
        //tähän timerin perusteella aiheuttaa aina välillä damagea targetille
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
    {   //AttackRangen pituinen raycast, (pitää osua playerin keskustaan asti kai)
        Gizmos.DrawRay(transform.position, transform.forward * attackRange);
    }
}
