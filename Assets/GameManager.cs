using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    

    void Update()
    {
        //Check for mouse click 
        if (Input.GetMouseButtonDown(0)) // esto es solo para la pistola de bits
        {
            RaycastHit raycastHit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out raycastHit, 100f))
            {
                if (raycastHit.transform != null)
                {
                    //Our custom method. 
                    LeftClickedGameObject(raycastHit.transform.gameObject);
                }
            }
            else
                print("target is null");
        }

        if (Input.GetMouseButtonDown(1)) // esto es solo para la pistola de bits
        {
            RaycastHit raycastHit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out raycastHit, 100f))
            {
                if (raycastHit.transform != null)
                {
                    //Our custom method. 
                    RightClickedGameObject(raycastHit.transform.gameObject);
                }
            }
            else
                print("target is null");
        }
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
        }
    }
}
