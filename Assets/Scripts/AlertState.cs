using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertState : IEnemyState
{
    private StatePatternEnemy enemy;
    float searchTimer;

    public AlertState(StatePatternEnemy statePatternEnemy)
    {
        this.enemy = statePatternEnemy;
    }

    public void UpdateState()
    {
        Look();
        Search();
    }

    public void OnTriggerEnter(Collider other)
    {
       
    }

    public void ToAlertState()
    {
                //ollaan jo alert-tilassa, ei k�ytet� t�t� metodia
    }

    public void ToChaseState()
    {
        searchTimer = 0;
        enemy.currentState = enemy.chaseState;
    }

    public void ToPatrolState()
    {
        searchTimer = 0;
        enemy.currentState = enemy.patrolState;
    }
    public void ToTrackingState()
    {

    }

    void Search ()
    {
        enemy.indicator.material.color = Color.yellow;
        enemy.navMeshAgent.isStopped = true;    //pys�ytet��n alert tilassa
        enemy.transform.Rotate(0, enemy.searchTurningSpeed * Time.deltaTime, 0);
        searchTimer += Time.deltaTime;

        if(searchTimer >= enemy.searchDuration)
        {
            //vihu v�syy etsimiseen ja jatkaa patrollointia
            ToPatrolState();
        }

    }

    void Look()
    {
        //Debuggis�de visualisointia varten
        Debug.DrawRay(enemy.eye.position, enemy.eye.forward * enemy.sightRange, Color.yellow);

        //T�M� ON PERUSJUTTU MIT� K�YTET��N USEIN raycasti ja jos osuu oikeanlaiseen kohteeseen..
        RaycastHit hit;
        if (Physics.Raycast(enemy.eye.position, enemy.eye.forward, out hit, enemy.sightRange)
            && hit.collider.CompareTag("Player"))
        {
            //jos katses�deosuu Playeriin, laitetaan enemy chase-tilaan
            enemy.chaseTarget = hit.transform;
            ToChaseState();
        }
    }


}
