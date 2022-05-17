using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRaycast : MonoBehaviour
{
    FPSController move;

    public LayerMask ground;
    RaycastHit hitGround;
    GameObject groundObject;
    public float rayDistanceGround; //0.58
    public float sphereRadius; //0.45

    void Awake()
    {
        move = GetComponent<FPSController>();
    }

    void Update()
    {
        RayGround();
    }

    void RayGround()
    {
        if(Physics.SphereCast(transform.position, sphereRadius, -transform.up, out hitGround, rayDistanceGround, ground))
        {
            move.grounded = true;
        }
        else
        {
            move.grounded = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        //RayGround
        Gizmos.color = Color.red;
        Debug.DrawRay(transform.position, -transform.up * rayDistanceGround);
        Gizmos.DrawWireSphere(transform.position + -transform.up * rayDistanceGround, sphereRadius);
    }
}
