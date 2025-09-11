using UnityEngine;
using UnityEngine.AI;

public class Enemy : PoolableObject
{
    public NavMeshAgent agent;
    public EnemyMovement movement;
    private float Health =100.0f;

    public override void OnDisable()
    {
        base.OnDisable();
        agent.enabled= false;
    }
}
