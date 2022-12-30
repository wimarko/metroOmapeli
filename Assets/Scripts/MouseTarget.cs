using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTarget : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;


    // Update is called once per frame
    void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            transform.position = new Vector3(raycastHit.point.x, 0.5f, raycastHit.point.z) ;
            //Debug.Log("c" + raycastHit.point);
        }
    }
}
