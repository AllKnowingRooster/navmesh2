using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof(NavMeshAgent),typeof(AgentLinkMover))]
public class EnemyMovement : MonoBehaviour
{
    
    private NavMeshAgent agent;
    public float changeRouteTime=0.1f;
    [HideInInspector] public GameObject player;
    private bool isEnabled=true;
    private Animator enemyAnimator;
    private string isMovingParameter = "isMoving";
    private string jumpParameter = "isJumping";
    private AgentLinkMover linkMover;
    void Awake()
    {
        agent= GetComponent<NavMeshAgent>();
        linkMover = GetComponent<AgentLinkMover>();
        enemyAnimator = GetComponentInChildren<Animator>();
        linkMover.OnStartJump += StartJumpAnimation;
        linkMover.OnEndJump += EndJumpAnimation;
    }

    public void StartMoving()
    {
        StartCoroutine(FollowPlayer());
    }

    public void StartJumpAnimation()
    {
        enemyAnimator.SetBool(jumpParameter, true);
    }

    public void EndJumpAnimation()
    {
        enemyAnimator.SetBool(jumpParameter,false);
    }


    IEnumerator FollowPlayer()
    {
        while (isEnabled)
        {
            agent.SetDestination(player.transform.position);
            enemyAnimator.SetBool(isMovingParameter, agent.velocity.magnitude > 0.01f);
            yield return new WaitForSeconds(changeRouteTime);
        }
    }

}
