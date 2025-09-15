using UnityEngine;
using UnityEngine.AI;

public class Enemy : PoolableObject,IDamagable,ISetupable
{
    public NavMeshAgent agent;
    public EnemyMovement movement;
    public ScriptableEnemyObject scriptableEnemyObject;
    public AttackRadius attackRadius;
    public Animator enemyAnimator;
    [SerializeField] private float health =100.0f;
    private string attackTrigger = "attack";

    public virtual void OnEnable()
    {
        SetupAgent();
    }
    public override void OnDisable()
    {
        base.OnDisable();
        agent.enabled= false;
    }

    public void SetupAgent()
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
        health = scriptableEnemyObject.health;
    }


    private void Awake()
    {
        attackRadius.setAttackAnimation += SetAttackAnimation;
    }
   

    public Transform GetTransform()
    {
        return transform;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void SetAttackAnimation(IDamagable damagable)
    {
        enemyAnimator.SetTrigger(attackTrigger);
        Vector3 direction = damagable.GetTransform().position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = lookRotation;
    }
  
}
