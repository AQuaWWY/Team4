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

    private float despawnDistance;

    private List<GameObject> spawnedEnemies = new List<GameObject>();

    public int checkPerFrame;//每一帧需要检查的敌人数量
    private int enemyToCheck;//需要检查的敌人索引

    // Start is called before the first frame update
    void Start()
    {
        spawnerCounter = timeToSpawn;

        target = PlayerHealthController.instance.transform;//让健康系统找到玩家位置，性能更高

        despawnDistance = Vector3.Distance(transform.position,maxSpawn.position) + 4f;//消失距离比生成距离远一点
    }

    // Update is called once per frame
    void Update()
    {
        spawnerCounter -= Time.deltaTime;
        if(spawnerCounter <= 0)
        {
            spawnerCounter = timeToSpawn;

            //Instantiate(enemyToSpawn,transform.position,transform.rotation);

            GameObject newEnemy = Instantiate(enemyToSpawn,SelectSpawnPoint(),transform.rotation);//生成新的敌人

            spawnedEnemies.Add(newEnemy);//添加到列表中
        }

        transform.position = target.position;

        int  checkTarget = enemyToCheck + checkPerFrame;//检查结束？

        while(enemyToCheck < checkTarget)//没到结束
        {
            if(enemyToCheck < spawnedEnemies.Count)//需要检查的怪物数小于总怪物数
            {
                if(spawnedEnemies[enemyToCheck] != null)//有怪物
                {
                    if(Vector3.Distance(transform.position,spawnedEnemies[enemyToCheck].transform.position) > despawnDistance)//超出消失距离
                    {
                        Destroy(spawnedEnemies[enemyToCheck]);//销毁

                        spawnedEnemies.RemoveAt(enemyToCheck);//从列表中去掉
                        checkTarget--;//索引减一
                    } else
                    {
                        enemyToCheck++;//判断下一个怪物
                    }
                } else//如果列表中元素为空
                {
                    spawnedEnemies.RemoveAt(enemyToCheck);//去掉空的那个
                    checkTarget --;//索引减一
                }
            } else 
            {
                enemyToCheck = 0;
                checkTarget = 0;
            }
        }
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
