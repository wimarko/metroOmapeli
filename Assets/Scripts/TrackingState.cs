using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingState : IEnemyState
{

    private StatePatternEnemy enemy;

    public TrackingState(StatePatternEnemy statePatternEnemy)
    {
        //konstruktorissa saatu parametri on tämän "olion/skriptin" 'enemy'
        //jotta löytää enemy:n StatePatternEnemy:n metodit/funktiot tarvittaessa
        this.enemy = statePatternEnemy;
    }
    public void OnTriggerEnter(Collider other)
    {
        
    }

    public void UpdateState()
    {
        Track();
        Look();
    }

    private void Track()
    {
        enemy.navMeshAgent.destination = enemy.lastSight;
        enemy.navMeshAgent.isStopped = false;
        enemy.indicator.material.color = new Color(0.4f, 0.6f, 0.7f);

        //vaihdetaan waypoint, kun p��st��n kohdewaypointiin
        if (enemy.navMeshAgent.remainingDistance <= enemy.navMeshAgent.stoppingDistance)
            //&& !enemy.navMeshAgent.pathPending)  //eli liikkumisprosessi on p��ttynyt (ei ole en�� pathPending)
        {
            ToAlertState();
        }
    }

    public void ToAlertState()
    {
        enemy.currentState = enemy.alertState;
    }

    public void ToChaseState()
    {
        
    }

    public void ToPatrolState()
    {
        
    }

    public void ToTrackingState()
    {
        
    }

    void Look()
    {
        //Debuggis�de visualisointia varten
        Debug.DrawRay(enemy.eye.position, enemy.eye.forward * enemy.sightRange, new Color(0.2583451f, 0f, 0.2641509f));

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
