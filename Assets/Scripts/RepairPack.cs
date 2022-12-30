using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairPack : MonoBehaviour
{

    [SerializeField] int repairAmount = 20;

    public int GetRepairAmount()
    {
        return repairAmount;
    }
}