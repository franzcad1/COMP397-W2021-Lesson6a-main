using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EveState
{
    IDLE,
    RUN,
    JUMP
}

public class EveBehaviour : MonoBehaviour
{
    [Header("Line of Sight")]
    public LayerMask collisionLayer;
    public Vector3  LOSoffset = new Vector3(0.0f, 2.0f, -5.0f);
    public bool hasLOS;

    private Animator animator;
    private NavMeshAgent agent;
    private Vector3 playerLocation;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //var size = new Vector3(4.0f, 4.0f, 10.0f);
        if (hasLOS)
        {
            agent.SetDestination(playerLocation);
        }
 


        if (Input.GetKeyDown(KeyCode.I))
        {
            animator.SetInteger("AnimState", (int)EveState.IDLE);
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            animator.SetInteger("AnimState", (int) EveState.RUN);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            animator.SetInteger("AnimState", (int)EveState.JUMP);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            hasLOS = true;
            animator.SetInteger("AnimState", (int)EveState.RUN);
        }
            
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            hasLOS = false;
            animator.SetInteger("AnimState", (int)EveState.IDLE);
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            playerLocation = other.transform.position;
        }
    }
}
