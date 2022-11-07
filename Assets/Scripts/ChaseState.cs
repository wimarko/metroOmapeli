using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : IEnemyState
{
    private StatePatternEnemy enemy;

    public ChaseState(StatePatternEnemy statePatternEnemy)
    {
        this.enemy = statePatternEnemy;
    }

    public void UpdateState()
    {
        Look();
        Chase();
        
    }

    public void OnTriggerEnter(Collider other)
    {
        
    }

    public void ToAlertState()
    {
        enemy.currentState = enemy.alertState;
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
        enemy.currentState = enemy.trackingState;
    }

    void Chase()
    {
        Debug.Log("chasing");
        enemy.indicator.material.color = Color.black;
        //enemy.navMeshAgent.destination = enemy.chaseTarget.position;
        enemy.navMeshAgent.destination = enemy.lastSight;
        enemy.navMeshAgent.isStopped = false;
    }

    void Look()
    {

        Vector3 enemyToTarget = enemy.chaseTarget.position - enemy.eye.position; 
        //B-A eli suuntavetori silmästä kohteeseen


        //Debuggisäde visualisointia varten
        Debug.DrawRay(enemy.eye.position, enemyToTarget, Color.red, 1f);

        //TÄMÄ ON PERUSJUTTU MITÄ KÄYTETÄÄN USEIN raycasti ja jos osuu oikeanlaiseen kohteeseen..
        RaycastHit hit;
        if (Physics.Raycast(enemy.eye.position,enemyToTarget, out hit, enemy.sightRange)
            && hit.collider.CompareTag("Player"))
        {
            //jos katsesädeosuu Playeriin, laitetaan enemy chase-tilaan
            enemy.chaseTarget = hit.transform;
            enemy.lastSight = hit.transform.position;

        }
        
        else
        {
            Debug.Log("toTracking");
            ToTrackingState();
            //tämä else toteutuu jos enemy ei enää näe pelaajaa (vähän liian herkästi)?
            //ToAlertState();
            
        }
        
    }


}
