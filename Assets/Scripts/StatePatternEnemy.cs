using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StatePatternEnemy : MonoBehaviour
{

    public float searchDuration;      //kauanko etsii alert tilassa pelaajaa 
    public float searchTurningSpeed; //kuinka nopeasti k‰‰ntyy
    public float sightRange;         //kuinka pitk‰lle n‰kee. Raycast
    public Transform[] waypoints;    //patrol tilan reitin waypointit, taulukossa
    public Transform eye;            //sightrange-n‰kˆRaycastin l‰htˆpaikka-silm‰
    public MeshRenderer indicator;    //laatikko vihun p‰‰ll‰, ilmoittaa tilan


    [HideInInspector] public Transform chaseTarget; //t‰m‰ on pelaaja, kun aletaan jahtaamaan
    [HideInInspector] public IEnemyState currentState; //vihun t‰m‰nhetkinen tila, vaihtuu tilanteen mukaan
    [HideInInspector] public PatrolState patrolState;
    [HideInInspector] public AlertState alertState;
    [HideInInspector] public ChaseState chaseState;
    [HideInInspector] public TrackingState trackingState;
    [HideInInspector] public NavMeshAgent navMeshAgent; // agentti osaa liikkua navigoida NavMeshiss‰
    public Vector3 lastSight;
    public PatrolPath patrolPath = null;

    private void Awake()
    {
        //tehd‰‰n heti her‰tyksess‰ asioita
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
        //voi myˆhemmin olla mik‰ vaan state, ajetaan aktiivisen staten UpdateState-metodi
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


    //Wapaaehtoinen l‰ksy
    /*
     Tee nelj‰s tila. Tilan nimi on Tracking State. Kun enemy on Chase-tilassa, eik‰ en‰‰ n‰e pelaajaa
    Tallentaa se muistiin pelaajan viimeisimm‰n tiedetyn sijainnin.
    T‰m‰n j‰lkeen enemy menee trackin-stateen ja liikkuu t‰h‰n sijaintiin
    Perille p‰‰sty‰‰n menee Alert-stateen
     */
}
