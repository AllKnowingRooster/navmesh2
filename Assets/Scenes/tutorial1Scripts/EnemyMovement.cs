using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    
    private NavMeshAgent agent;
    private float changeRouteTime=0.1f;
    public GameObject player;
    private bool isEnabled=true;
    void Start()
    {
        agent= GetComponent<NavMeshAgent>();
        StartCoroutine(FollowPlayer());        
    }

    IEnumerator FollowPlayer()
    {
        while (isEnabled)
        {
            agent.SetDestination(player.transform.position);
            yield return new WaitForSeconds(changeRouteTime);
        }
    }

}
