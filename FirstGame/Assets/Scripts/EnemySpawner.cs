using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyToSpawn;

    public float timeToSpawn;
    private float spawnCounter;

    public Transform minSpawn, maxSpawn;

    private Transform target;

    private float despawnDistance;

    private List<GameObject> spawnedEnemies = new List<GameObject>();

    public int checkPerFrame;//每一帧需要检查的敌人数量
    private int enemyToCheck;//需要检查的敌人索引

    public List<WaveInfo> waves;//将波数类型的数据存储在列表中

    private int currentWave;//当前波数

    private float waveCounter;//波数持续时间计时器

    // Start is called before the first frame update
    void Start()
    {
        spawnCounter = timeToSpawn;

        target = PlayerHealthController.instance.transform;//让健康系统找到玩家位置，性能更高

        despawnDistance = Vector3.Distance(transform.position, maxSpawn.position) + 4f;//设置消失距离：消失距离比生成距离远一点

        currentWave = -1;//-1，未开始
        GoToNextWave();
    }

    // Update is called once per frame
    void Update()
    {
        //每帧减少计时器spawnerCounter，当计时器小于等于0时，重置计时器并在指定位置生成一个新的敌人对象，然后将该敌人添加到spawnedEnemies列表中进行管理。
        /* spawnerCounter -= Time.deltaTime;
        if(spawnerCounter <= 0)
        {
            spawnerCounter = timeToSpawn;

            //Instantiate(enemyToSpawn,transform.position,transform.rotation);

            GameObject newEnemy = Instantiate(enemyToSpawn,SelectSpawnPoint(),transform.rotation);//生成新的敌人

            spawnedEnemies.Add(newEnemy);//添加到列表中
        } */

        if (PlayerHealthController.instance.gameObject.activeSelf)//如果玩家处于激活状态（存活）
        {
            if (currentWave < waves.Count - 1)//未遍历所有波数
            {
                waveCounter -= Time.deltaTime;//波数计时器递减直0

                if (waveCounter <= 0)//当计时器减到0才会进入下一波
                {
                    GoToNextWave();//下一波
                }

                spawnCounter -= Time.deltaTime;//生成计时器递减直0

                if (spawnCounter <= 0)//生成计时器减到0，生成新的敌人
                {
                    spawnCounter = waves[currentWave].timeBetweenSpawns;//重置生成计时器为设定好的时间间隔

                    GameObject newEnemy = Instantiate(waves[currentWave].enemyToSpawn, SelectSpawnPoint(), Quaternion.identity);//生成新的敌人

                    spawnedEnemies.Add(newEnemy);//添加到列表中
                }
            }
            else if (currentWave == waves.Count - 1)//遍历所有波数
            {
                waveCounter -= Time.deltaTime;//波数计时器递减直0

                if (waveCounter <= 0)//当计时器减到0才会进入下一波
                {
                    currentWave++;
                }

                spawnCounter -= Time.deltaTime;//生成计时器递减直0

                if (spawnCounter <= 0)//生成计时器减到0，生成新的敌人
                {
                    spawnCounter = waves[currentWave].timeBetweenSpawns;//重置生成计时器为设定好的时间间隔

                    GameObject newEnemy = Instantiate(waves[currentWave].enemyToSpawn, SelectSpawnPoint(), Quaternion.identity);//生成新的敌人

                    spawnedEnemies.Add(newEnemy);//添加到列表中
                }
            }
            else
            {
                //添加boss场景
                //按下N键切换到boss场景
                if (Input.GetKeyDown(KeyCode.N))//检测T键被按下就启动函数扣除伤害
                {
                    PlayerStatController.instance.SavePlayerStats();//保存数据

                    //GameManager.instance.SaveWeapons();

                    SceneManager.LoadScene("Boss Scene");
                }
            }
        }

        transform.position = target.position;//敌人生成器位置跟随玩家位置

        int checkTarget = enemyToCheck + checkPerFrame;//当前帧需要检查到的怪物索引数

        while (enemyToCheck < checkTarget)//没到结束，继续检查是否有怪物超出消失距离，有则销毁
        {
            if (enemyToCheck < spawnedEnemies.Count)//需要检查的怪物数小于总怪物数
            {
                if (spawnedEnemies[enemyToCheck] != null)//有怪物
                {
                    if (Vector3.Distance(transform.position, spawnedEnemies[enemyToCheck].transform.position) > despawnDistance)//超出消失距离
                    {
                        Destroy(spawnedEnemies[enemyToCheck]);//销毁

                        spawnedEnemies.RemoveAt(enemyToCheck);//从列表中去掉
                        checkTarget--;//索引减一
                    }
                    else//索引加一
                    {
                        enemyToCheck++;//判断下一个怪物
                    }
                }
                else//如果列表中元素为空
                {
                    spawnedEnemies.RemoveAt(enemyToCheck);//去掉空的那个
                    checkTarget--;//索引减一
                }
            }
            else //防止错误
            {
                enemyToCheck = 0;
                checkTarget = 0;
            }
        }
    }

    public Vector3 SelectSpawnPoint()//生成地点随机函数
    {
        Vector3 spawnPoint = Vector3.zero;

        bool spawnVerticalEdge = Random.Range(0f, 1f) > .5f;//50% 0or1
        if (spawnVerticalEdge)
        {
            spawnPoint.y = Random.Range(minSpawn.position.y, maxSpawn.position.y);

            if (Random.Range(0f, 1f) > .5f)
            {
                spawnPoint.x = maxSpawn.position.x;
            }
            else
            {
                spawnPoint.x = minSpawn.position.x;
            }
        }
        else
        {
            spawnPoint.x = Random.Range(minSpawn.position.x, maxSpawn.position.x);

            if (Random.Range(0f, 1f) > .5f)
            {
                spawnPoint.y = maxSpawn.position.y;
            }
            else
            {
                spawnPoint.y = minSpawn.position.y;
            }
        }

        return spawnPoint;
    }

    public void GoToNextWave()//重置下一波的参数：波数计时器和生成计时器
    {
        currentWave++;//当前波数索引加一
        if (currentWave >= waves.Count)//如果遍历到最后，甚至溢出列表，回到最后一波
        {
            currentWave = waves.Count - 1;
        }

        waveCounter = waves[currentWave].waveLength;//持续十秒的倒计时
        spawnCounter = waves[currentWave].timeBetweenSpawns;//一秒的怪物生成间隔
    }
}

[System.Serializable]//标签，使类可序列化，展示在Inspector中
public class WaveInfo//波数信息类，包含生成的敌人类型和波数持续时间，敌人生成间隔
{
    public GameObject enemyToSpawn;
    public float waveLength = 10f;
    public float timeBetweenSpawns = 1f;


}
