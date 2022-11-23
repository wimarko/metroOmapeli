using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : IEnemyState
{
    private StatePatternEnemy enemyState;
    private Enemy enemy;
    private Collider targetPlayer;

    [SerializeField] float timeSinceLastAttack;


    public ChaseState(StatePatternEnemy statePatternEnemy)
    {
        this.enemyState = statePatternEnemy;
    }

    public void UpdateState()
    {
        Look();
        Chase();
        Attack();
        
    }

    public void OnTriggerEnter(Collider other)
    {
        
    }

    public void ToAlertState()
    {
        enemyState.currentState = enemyState.alertState;
    }

    public void ToChaseState()
    {
        //ei käytössä tässä
    }

    public void ToPatrolState()
    {
        //periaatteessa ei käytetä, paitsi jos vihu saa pelaajan kiinni ja neutralisoitua.
    }

    public void ToTrackingState()
    {
        enemyState.currentState = enemyState.trackingState;
    }

    void Chase()
    {
        Debug.Log("chasing");
        enemyState.indicator.material.color = Color.black;
        //enemy.navMeshAgent.destination = enemy.chaseTarget.position;
        enemyState.navMeshAgent.destination = enemyState.lastSight;
        enemyState.navMeshAgent.isStopped = false;
    }

    void Look()
    {

        Vector3 enemyToTarget = enemyState.chaseTarget.position - enemyState.eye.position; 
        //B-A eli suuntavetori silmästä kohteeseen


        //Debuggisäde visualisointia varten
        Debug.DrawRay(enemyState.eye.position, enemyToTarget, Color.red, 1f);

        //TÄMÄ ON PERUSJUTTU MITÄ KÄYTETÄÄN USEIN raycasti ja jos osuu oikeanlaiseen kohteeseen..
        RaycastHit hit;
        if (Physics.Raycast(enemyState.eye.position, enemyToTarget, out hit, enemyState.sightRange)
            && hit.collider.CompareTag("Player"))
        {
            //jos katsesädeosuu Playeriin, laitetaan enemy chase-tilaan
            enemyState.chaseTarget = hit.transform;
            enemyState.lastSight = hit.transform.position;
            targetPlayer = hit.collider;
        }
        else
        {
            Debug.Log("toTracking");
            ToTrackingState();
            //tämä else toteutuu jos enemy ei enää näe pelaajaa (vähän liian herkästi)?
            //ToAlertState();

        }       
    }

    void Attack()
    {
        timeSinceLastAttack += Time.deltaTime; 
        if(timeSinceLastAttack > enemyState.GetComponentInParent<Enemy>().GetAttackSpeed()
            && InAttackRange(targetPlayer))
        {
            timeSinceLastAttack = 0;
            causeDamage(targetPlayer);
        }
    }

    private void causeDamage (Collider target)
    {
        //tähän timerin perusteella aiheuttaa aina välillä damagea targetille
        //eli tarvitsee jonkun timerin ja kohteen(playerin)
        Debug.Log("Vihu iskee");
        
        target.GetComponentInChildren<PlayerController>().TakeDamage(
            enemyState.GetComponentInParent<Enemy>().GetDamage());
    }

    private bool InAttackRange(Collider other)
    {
        float distanceToPlayer = Vector3.Distance(other.transform.position,enemyState.transform.position);
        return distanceToPlayer < enemyState.GetComponentInParent<Enemy>().GetAttackRange();
        
    }


}
