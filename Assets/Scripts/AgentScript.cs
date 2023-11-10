using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentScript : MonoBehaviour
{
   void AutoDesactivarse()
    {
        ObjectPooler.Instance.ReturnObstacleToPool(gameObject.transform.parent.gameObject);
    }
}
