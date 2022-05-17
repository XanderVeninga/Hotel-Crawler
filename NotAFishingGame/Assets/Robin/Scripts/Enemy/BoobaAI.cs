using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoobaAI : MonoBehaviour
{
    public Rigidbody rb;
    public Transform transPlayer;

    public float boobaMovSpeed;
    public float boobaRotSpeed;
    public float drag;

    public float sphereRange;
    public LayerMask layerPlayer;

    public Vector3 playerPos;

    public bool found;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        FieldOfView();
        Walk();
    }

    void EnemyState()
    {

    }

    void Attack()
    {
        
    }

    void Walk()
    {
        rb.drag = drag;

        if(found)
        {
            playerPos = new Vector3(transPlayer.position.x, transform.position.y, transPlayer.position.z);

            transform.position = Vector3.Lerp(transform.position, playerPos, boobaMovSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerPos - transform.position), boobaRotSpeed * Time.deltaTime);
        }
    }

    void FieldOfView()
    {
        Collider[] range = Physics.OverlapSphere(transform.position, sphereRange, layerPlayer);

        if(range.Length != 0)
        {
            transPlayer = range[0].transform;
            found = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sphereRange);
    }
}
