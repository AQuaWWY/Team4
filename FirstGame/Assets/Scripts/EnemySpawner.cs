using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.Video;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyToSpawn;

    public float timeToSpawn;
    private float spawnerCounter;

    public Transform minSpawn,maxSpawn;

    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        spawnerCounter = timeToSpawn;

        target = PlayerHealthController.instance.transform;//让健康系统找到玩家位置，性能更高
    }

    // Update is called once per frame
    void Update()
    {
        spawnerCounter -= Time.deltaTime;
        if(spawnerCounter <= 0)
        {
            spawnerCounter = timeToSpawn;

            //Instantiate(enemyToSpawn,transform.position,transform.rotation);

            Instantiate(enemyToSpawn,SelectSpawnPoint(),transform.rotation);
        }

        transform.position = target.position;
    }

    public Vector3 SelectSpawnPoint()
    {
        Vector3 spawnPoint = Vector3.zero;

        bool spawnVerticalEdge = Random.Range(0f , 1f) > .5f;//50% 0or1
        if(spawnVerticalEdge)
        {
            spawnPoint.y = Random.Range(minSpawn.position.y , maxSpawn.position.y);
            
            if(Random.Range(0f , 1f) > .5f)
            {
                spawnPoint.x = maxSpawn.position.x;
            } else 
            {
                spawnPoint.x = minSpawn.position.x;
            }
        } else
        {
            spawnPoint.x = Random.Range(minSpawn.position.x , maxSpawn.position.x);
            
            if(Random.Range(0f , 1f) > .5f)
            {
                spawnPoint.y = maxSpawn.position.y;
            } else 
            {
                spawnPoint.y = minSpawn.position.y;
            }
        }

        return spawnPoint;
    }
}
