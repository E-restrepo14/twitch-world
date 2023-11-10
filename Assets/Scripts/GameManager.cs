using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool isPlaying = true;
    public float fireDelta = 0.5F; // esto es la velocidad de spawn de objetos, publico para cambiarse desde fuera

    private float nextFire = 1F;
    private float myTime = 0.0F;

    void Awake()
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

    void Update()
    {
        myTime += Time.deltaTime;

        if (myTime > nextFire & isPlaying)
        {
            SpawnObject();
        }

        if (
               (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1)) & isPlaying
           ) // esto es solo para la pistola de bits
        {
            UiManager.Instance.TurnAimWhite();
        }

        if (Input.GetMouseButtonDown(0) & isPlaying) // esto es solo para la pistola de bits
        {
            UiManager.Instance.TurnAimRed();

            RaycastHit raycastHit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out raycastHit, 100f))
            {
                if (raycastHit.transform != null)
                {
                    LeftClickedGameObject(raycastHit.transform.gameObject);
                }
            }
        }

        if (Input.GetMouseButtonDown(1) & isPlaying) // esto es solo para la pistola de bits
        {
            UiManager.Instance.TurnAimRed();

            RaycastHit raycastHit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out raycastHit, 100f))
            {
                if (raycastHit.transform != null)
                {
                    RightClickedGameObject(raycastHit.transform.gameObject);
                }
            }
        }
    }

    void SpawnObject()
    {
        
        
            nextFire = myTime + fireDelta;
            int randomInt = Random.Range(0, 3);
            switch (randomInt)
            {
                case 0:
                    ObjectPooler.Instance.ActivarCoin();
                    break;
                case 1:
                    ObjectPooler.Instance.ActivarEnemigo();
                    break;
                case 2:
                    ObjectPooler.Instance.ActivarBot();
                    break;
                default:
                    break;
            }
            nextFire = nextFire - myTime;
            myTime = 0.0F;
        
    }

    public void LeftClickedGameObject(GameObject m_gameObject)
    {
        if (m_gameObject.tag == "Enemy")
        {
            ObjectPooler.Instance.ReturnObstacleToPool(m_gameObject.transform.parent.gameObject);
        }

        if (m_gameObject.tag == "Coin")
        {
            ObjectPooler.Instance.ReturnObstacleToPool(m_gameObject.transform.parent.gameObject);
        }
    }

    public void RightClickedGameObject(GameObject m_gameObject)
    {
        if (m_gameObject.tag == "Bot")
        {
            ObjectPooler.Instance.ReturnObstacleToPool(m_gameObject.transform.parent.gameObject);
            UiManager.Instance.AnimateComboBybool();
        }
    }
}
