using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBooster : MonoBehaviour
{
    [SerializeField] float speedBoost = 1.5f;
    [SerializeField] float time = 5f;

    public float GetSpeedBoost()
    {
        return speedBoost;
    }

    public float GetBoostTime()
    {
        return time;
    }
}
