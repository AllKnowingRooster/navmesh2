using UnityEngine;
using UnityEngine.AI;

public class Enemy : PoolableObject
{
    public NavMeshAgent agent;
    public EnemyMovement movement;
    public ScriptableEnemyObject scriptableEnemyObject;

    private float Health =100.0f;

    public virtual void OnEnable()
    {
        SetupAgent();
    }
    public override void OnDisable()
    {
        base.OnDisable();
        agent.enabled= false;
    }

    public virtual void SetupAgent()
    {
        agent.acceleration = scriptableEnemyObject.accelaration;
        agent.angularSpeed = scriptableEnemyObject.angularSpeed;
        agent.areaMask= scriptableEnemyObject.areaMask;
        agent.avoidancePriority = scriptableEnemyObject.avoidancePriority;
        agent.baseOffset = scriptableEnemyObject.baseOffset;
        agent.height = scriptableEnemyObject.height;
        agent.obstacleAvoidanceType=scriptableEnemyObject.obstacleAvoidanceType;
        agent.radius = scriptableEnemyObject.radius;
        agent.speed = scriptableEnemyObject.speed;
        agent.stoppingDistance= scriptableEnemyObject.stoppingDistance;
        movement.changeRouteTime = scriptableEnemyObject.aIUpdateInterval;
        Health = scriptableEnemyObject.health;
    }

}
