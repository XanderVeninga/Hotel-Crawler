using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoobaAI : MonoBehaviour
{
    public float sphereRange;
    public LayerMask layerPlayer;
    public Vector3 playerPos;
    public bool found;
    public Transform transformPlayer;

    void Start()
    {
        
    }

    void Update()
    {
        FieldOfView();
    }

    void Attack()
    {
        
    }

    void Running()
    {

    }

    void FieldOfView()
    {
        Collider[] range = Physics.OverlapSphere(transform.position, sphereRange, layerPlayer);

        if(range.Length != 0)
        {
            transformPlayer = range[0].transform;
            found = true;
        }

        if(found)
        {
            playerPos = transformPlayer.position;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sphereRange);
    }
}
