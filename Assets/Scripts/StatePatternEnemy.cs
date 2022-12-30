using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StatePatternEnemy : MonoBehaviour
{

    public float searchDuration;      //kauanko etsii alert tilassa pelaajaa 
    public float searchTurningSpeed; //kuinka nopeasti kääntyy
    public float sightRange;         //kuinka pitkälle näkee. Raycast
    public Transform[] waypoints;    //patrol tilan reitin waypointit, taulukossa
    public Transform eye;            //sightrange-näköRaycastin lähtöpaikka-silmä
    public MeshRenderer indicator;    //laatikko vihun päällä, ilmoittaa tilan


    [HideInInspector] public Transform chaseTarget; //tämä on pelaaja, kun aletaan jahtaamaan
    [HideInInspector] public IEnemyState currentState; //vihun tämänhetkinen tila, vaihtuu tilanteen mukaan
    [HideInInspector] public PatrolState patrolState;
    [HideInInspector] public AlertState alertState;
    [HideInInspector] public ChaseState chaseState;
    [HideInInspector] public TrackingState trackingState;
    [HideInInspector] public NavMeshAgent navMeshAgent; // agentti osaa liikkua navigoida NavMeshissä
    public Vector3 lastSight;
    public PatrolPath patrolPath = null;

    private void Awake()
    {
        //tehdään heti herätyksessä asioita
        patrolState = new PatrolState(this);
        alertState = new AlertState(this);
        chaseState = new ChaseState(this);
        trackingState = new TrackingState(this);
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
            currentState = patrolState;
    }

    // Update is called once per frame
    void Update()
    {
        //voi myöhemmin olla mikä vaan state, ajetaan aktiivisen staten UpdateState-metodi
        currentState.UpdateState();
    }

    private void OnTriggerEnter(Collider other)
    {
        currentState.OnTriggerEnter(other);
    }

    public Transform[] GetWaypoints()
    {
        return waypoints;
    }
}
