using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyState
{

    //mitä metodeja kuuluu olla tätä interfacea toteuttavissa luokissa
    void UpdateState();

    void OnTriggerEnter(Collider other);

    void ToPatrolState();

    void ToAlertState();

    void ToChaseState();

    void ToTrackingState();
}
