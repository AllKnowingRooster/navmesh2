using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "Enemy Configuration", menuName = "ScriptableObject/Enemy Configuration")]
public class ScriptableEnemyObject : ScriptableObject
{
    public int health = 100;
    public float aIUpdateInterval = 0.1f;
    public float accelaration = 8;
    public float angularSpeed = 120;
    public int areaMask = -1;
    public int avoidancePriority = 50;
    public float baseOffset = 0;
    public float height = 2.0f;
    public ObstacleAvoidanceType obstacleAvoidanceType = ObstacleAvoidanceType.LowQualityObstacleAvoidance;
    public float radius = 0.5f;
    public float speed = 3.0f;
    public float stoppingDistance = 0.5f;
}
