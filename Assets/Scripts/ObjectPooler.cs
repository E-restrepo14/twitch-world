using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public GameObject enemy1Prefab;
    public GameObject coinPrefab;
    public GameObject twitchBotPrefab;

    public GameObject efectDeadEnemyPrefab;
    //public GameObject efectGrabbedCoinPrefab;
    public GameObject efectBotPrefab;

    public int poolSize = 3;

    [SerializeField] private List<GameObject> enemyPool = new List<GameObject>();
    [SerializeField] private List<GameObject> coinPool = new List<GameObject>();
    [SerializeField] private List<GameObject> botPool = new List<GameObject>();

    [SerializeField] private List<GameObject> efectEnemyDeadPool = new List<GameObject>();
    //[SerializeField] private List<GameObject> efectGrabbedCoinPool = new List<GameObject>();
    [SerializeField] private List<GameObject> efectBotPool = new List<GameObject>();



    private void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject enemy = Instantiate(enemy1Prefab);
            enemy.SetActive(false);
            enemyPool.Add(enemy);

            GameObject coin = Instantiate(coinPrefab);
            coin.SetActive(false);
            coinPool.Add(coin);

            GameObject bot = Instantiate(twitchBotPrefab);
            bot.SetActive(false);
            botPool.Add(bot);


            GameObject efectDeadEnemy = Instantiate(efectDeadEnemyPrefab);
            efectDeadEnemy.SetActive(false);
            efectEnemyDeadPool.Add(efectDeadEnemy);

            //GameObject efectGrabbedCoin = Instantiate(efectGrabbedCoinPrefab);
            //efectGrabbedCoin.SetActive(false);
            //efectGrabbedCoinPool.Add(efectGrabbedCoin);

            GameObject efectBot = Instantiate(efectBotPrefab);
            efectBot.SetActive(false);
            efectBotPool.Add(efectBot);
        }
    }

    public void ActivarEnemigo ()
    {
        GetObjectFromPool(enemyPool);
    }

    private void ActivarEfectoEnemigo(GameObject enemyPosition)
    {
        print("y el enemigo?");
        GameObject m_efect = InstantiateEfect(efectEnemyDeadPool, enemyPosition.transform.GetChild(0).transform.position);
    }

    private void ActivarEfectoBot(GameObject botPosition)
    {
        print("y el bot");
        GameObject m_efect = InstantiateEfect(efectBotPool, botPosition.transform.GetChild(0).transform.position);
    }


    public void ActivarBot()
    {
        GetObjectFromPool(botPool);
    }

    public void ActivarCoin()
    {
        GetObjectFromPool(coinPool);
    }


    private GameObject InstantiateEfect(List<GameObject> myList, Vector3 position)
    {
        foreach (GameObject m_object in myList)
        {
            if (!m_object.transform.GetChild(0).GetChild(0).gameObject.activeInHierarchy)
            {
                m_object.SetActive(false);
                m_object.SetActive(true);
                m_object.transform.position = position;
                return m_object;
            }
        }
        return null;
    }

    public GameObject GetObjectFromPool( List<GameObject> myList)
    {
        foreach (GameObject m_object in myList)
        {
            if (!m_object.activeInHierarchy)
            {
                m_object.SetActive(true);
                return m_object;
            }
        }
        print("no hay enemigos disponibles");
        return null;
    }

    public void ReturnObstacleToPool(GameObject obstacle) // esto es muy util, debería de ponerlo en un singleton
    {
        if (obstacle.transform.GetChild(0).tag == "Enemy")
            ActivarEfectoEnemigo(obstacle);
        if (obstacle.transform.GetChild(0).tag == "Bot")
            ActivarEfectoBot(obstacle);

        obstacle.SetActive(false);
        // pendiente restablecer la posición del obstáculo.
    }
}
