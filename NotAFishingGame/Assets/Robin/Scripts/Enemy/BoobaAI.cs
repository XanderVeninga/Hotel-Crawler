using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoobaAI : MonoBehaviour
{
    public Rigidbody rb;
    public Transform player;

    public float boobaMovSpeed;
    public float boobaRotSpeed;
    public float drag;

    public float sphereRange;
    public LayerMask layerPlayer;
    RaycastHit hit;

    public Vector3 playerPos;

    public bool found;
    public bool seePlayer;

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
            playerPos = new Vector3(player.position.x, transform.position.y, player.position.z);

            transform.position = Vector3.Lerp(transform.position, playerPos, boobaMovSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerPos - transform.position), boobaRotSpeed * Time.deltaTime);
        }
    }

    void FieldOfView()
    {
        Collider[] range = Physics.OverlapSphere(transform.position, sphereRange, layerPlayer);

        if(range.Length != 0)
        {
            seePlayer = true;

            player = range[0].transform;
        }
        //else
        //{
        //    seePlayer = false;
        //    player = null;
        //}

        if(seePlayer)
        {
            Vector3 dirToPlayer = (player.position - transform.position).normalized;
            float disToPlayer = Vector3.Distance(transform.position, player.position);

            Debug.DrawRay(transform.position, dirToPlayer * disToPlayer, Color.red);
            if(Physics.Raycast(transform.position, dirToPlayer, out hit, disToPlayer))
            {
                if(hit.transform.tag == "Player")
                {
                    found = true;
                }
                else
                {
                    found = false;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sphereRange);
    }
}
