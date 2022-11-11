using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{

    public CharacterController enemyControl;
    public Character playerControl;

    public float detectionRange = 10f;
    Transform target;
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = EnemyTarget.instance.target.transform;
        enemyControl = GetComponent<CharacterController>();

        // Code will slow down game if scene contains many objects.
        // playerControl = FindObjectOfType<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        float detectDistance = Vector3.Distance(target.position, transform.forward);
        if (detectDistance <= detectionRange) {
            agent.SetDestination(target.position);
            if (detectDistance <= agent.stoppingDistance) {
                transform.LookAt(playerControl.transform.position);
            }
            // Now using NavMesh
            
            //enemyControl.Move(transform.forward * enemySpeed);
        }
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }

}
