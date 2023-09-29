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

    public int poolSize = 3;

    [SerializeField] private List<GameObject> enemyPool = new List<GameObject>();
    [SerializeField] private List<GameObject> coinPool = new List<GameObject>();
    [SerializeField] private List<GameObject> botPool = new List<GameObject>();



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
        }
    }

    public void ActivarEnemigo ()
    {
        GetObjectFromPool(enemyPool);
    }

    public void ActivarBot()
    {
        GetObjectFromPool(botPool);
    }

    public void ActivarCoin()
    {
        GetObjectFromPool(coinPool);
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
        obstacle.SetActive(false);
        // pendiente restablecer la posición del obstáculo.
    }

}
