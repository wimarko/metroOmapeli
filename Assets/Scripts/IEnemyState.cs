using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyState
{

    //mit� metodeja kuuluu olla t�t� interfacea toteuttavissa luokissa
    void UpdateState();

    void OnTriggerEnter(Collider other);

    void ToPatrolState();

    void ToAlertState();

    void ToChaseState();

    void ToTrackingState();
}
