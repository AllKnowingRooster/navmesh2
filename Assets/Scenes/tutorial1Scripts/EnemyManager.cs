using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public enum SpawnMethod
{
    RoundRobin,
    Random,
}

public class EnemyManager : MonoBehaviour
{

    private int maxEnemies = 1;
    private float spawnInterval = 1.5f;
    private NavMeshTriangulation triangulation;
    private Dictionary<int,Pool> poolDict = new Dictionary<int,Pool>();
    public SpawnMethod enemyManagerSpawnMethod;
    public List<Enemy> enemyList;
    public GameObject player;

    private void Awake()
    {
        triangulation = NavMesh.CalculateTriangulation(); 
        for (int i=0;i<enemyList.Count;i++)
        {
            poolDict.Add(i, Pool.CreatePoolInstance(enemyList[i], maxEnemies));
        }
    }

    private void Start()
    {
        StartCoroutine(StartSpawning());
    }

    private IEnumerator StartSpawning()
    {
        WaitForSeconds time=new WaitForSeconds(spawnInterval);
        int currentTotalEnemies = 0;
        while(currentTotalEnemies<maxEnemies)
        {
            if (enemyManagerSpawnMethod==SpawnMethod.Random)
            {
                SpawnEnemies(RandomSpawnMode());
            }else if(enemyManagerSpawnMethod==SpawnMethod.RoundRobin){
                SpawnEnemies(RoundRobinSpawnMode(currentTotalEnemies));
            }
            currentTotalEnemies += 1;
            yield return time;
        }
    }

    private int RoundRobinSpawnMode(int currentTotalEnemies)
    {
        return currentTotalEnemies % enemyList.Count;
    }

    private int RandomSpawnMode()
    {
        return Random.Range(0,enemyList.Count);
    }

    private void SpawnEnemies(int index)
    {
        PoolableObject enemyObject = poolDict[index].GetObject();
        if (enemyObject != null)
        {
            int randomVertex = Random.Range(0,triangulation.vertices.Length);
            NavMeshHit hit;
            if (NavMesh.SamplePosition(triangulation.vertices[randomVertex],out hit,2.0f,-1))
            {
                Enemy enemy=enemyObject.GetComponent<Enemy>();
                enemy.agent.Warp(hit.position);
                enemy.movement.player = player;
                enemy.agent.enabled = true;
                enemy.movement.StartMoving();
            }
        }
        else
        {
            Debug.LogError("Failed Getting Enemy Object " + poolDict[index].poolableObjectPrefab + "from pool");
        }
    }

}
