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
    Animator animator;

    [SerializeField]
    private float detectDistance = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = EnemyTarget.instance.target.transform;
        enemyControl = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        // Code will slow down game if scene contains many objects.
        // playerControl = FindObjectOfType<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        detectDistance = Vector3.Distance(target.position, transform.position);
        if (detectDistance <= detectionRange) {
            agent.SetDestination(target.position);
            animator.SetBool("Run",true);
            if (detectDistance <= agent.stoppingDistance + 0.5f) {
                transform.LookAt(playerControl.transform.position);
                animator.SetBool("Attack", true);
                GetComponent<Enemy>().DoDamage();
            }
            else
            {
                animator.SetBool("Attack", false);
            }
        }
        else
        {
            animator.SetBool("Run", false);
        }
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }

}
