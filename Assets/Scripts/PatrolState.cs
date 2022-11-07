using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PatrolState : IEnemyState
{

    private StatePatternEnemy enemyState;
    
    int currentWaypointIndex = 0;

    
    //construktori(samanniminen kuin luokan noimi)
    //saa parametrina StatePatternEnemyn (joka on luonut PatrolStaten, antaen itsens� parametrin�)
    public PatrolState(StatePatternEnemy statePatternEnemy)
    {
        //t�m� enemy on StatePatternEnemy, eli sen skriptin metodit toimii kun kirjoiteteaan enemy
        this.enemyState = statePatternEnemy;
    }

    public void UpdateState()
    {
        Patrol();
        Look();
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Joku osui triggeriin");
        if(other.CompareTag("Player"))
        {
            //pelaaja on kuuloet�isyydell�!
            ToAlertState();
        }
    }

    public void ToAlertState()
    {
        //enemy:n tila muutetaan alert-Stateksi
        enemyState.currentState = enemyState.alertState;
    }

    public void ToChaseState()
    {
        enemyState.currentState = enemyState.chaseState;
    }

    public void ToPatrolState()
    {
        //ei voida ajaa t�t�, koska ollaan jo patrol tilassa
    }

    public void ToTrackingState()
    {

    }


    //katselee 
    void Look ()
    {
        //Debuggis�de visualisointia varten
        Debug.DrawRay(enemyState.eye.position, enemyState.eye.forward * enemyState.sightRange, Color.green);

        //T�M� ON PERUSJUTTU MIT� K�YTET��N USEIN raycasti ja jos osuu oikeanlaiseen kohteeseen..
        RaycastHit hit;
        if (Physics.Raycast(enemyState.eye.position, enemyState.eye.forward, out hit, enemyState.sightRange) 
            && hit.collider.CompareTag("Player"))
        {
            //jos katses�deosuu Playeriin, laitetaan enemy chase-tilaan
            enemyState.chaseTarget = hit.transform;
           // enemy.indicator.material.color = Color.yellow; //omaa testi�
            ToChaseState();
        }
    }

    void Patrol()
    {
        //enemyState.transform.LookAt(GetCurrentWaypoint()); k��ntyy katsomaan liian nopesti
        //enemyState.indicator.material.color = Color.green;
        enemyState.navMeshAgent.destination = GetCurrentWaypoint() ;
        enemyState.navMeshAgent.isStopped = false;

        //vaihdetaan waypoint, kun p��st��n kohdewaypointiin
        if(enemyState.navMeshAgent.remainingDistance <= enemyState.navMeshAgent.stoppingDistance 
            && !enemyState.navMeshAgent.pathPending)  //eli liikkumisprosessi on p��ttynyt (ei ole en�� pathPending)
        {
                

             CycleWaypoint();

        }
    }

    private void CycleWaypoint()
    {
        currentWaypointIndex = enemyState.patrolPath.GetNextIndex(currentWaypointIndex);

    }

    private Vector3 GetCurrentWaypoint()
    {
        return enemyState.patrolPath.GetWaypoint(currentWaypointIndex);
    }

    
}
