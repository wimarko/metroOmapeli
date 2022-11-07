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
                //ollaan jo alert-tilassa, ei käytetä tätä metodia
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
        enemy.navMeshAgent.isStopped = true;    //pysäytetään alert tilassa
        enemy.transform.Rotate(0, enemy.searchTurningSpeed * Time.deltaTime, 0);
        searchTimer += Time.deltaTime;

        if(searchTimer >= enemy.searchDuration)
        {
            //vihu väsyy etsimiseen ja jatkaa patrollointia
            ToPatrolState();
        }

    }

    void Look()
    {
        //Debuggisäde visualisointia varten
        Debug.DrawRay(enemy.eye.position, enemy.eye.forward * enemy.sightRange, Color.yellow);

        //TÄMÄ ON PERUSJUTTU MITÄ KÄYTETÄÄN USEIN raycasti ja jos osuu oikeanlaiseen kohteeseen..
        RaycastHit hit;
        if (Physics.Raycast(enemy.eye.position, enemy.eye.forward, out hit, enemy.sightRange)
            && hit.collider.CompareTag("Player"))
        {
            //jos katsesädeosuu Playeriin, laitetaan enemy chase-tilaan
            enemy.chaseTarget = hit.transform;
            ToChaseState();
        }
    }


}
