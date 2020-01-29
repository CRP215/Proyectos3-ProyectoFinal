using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : MonoBehaviour
{
    public GameObject m_item;
    public Transform m_pivot;
    public int healt = 3;



    public void DestroyObject()
    {
        Instantiate(m_item, m_pivot.position, Quaternion.identity);

        Destroy(gameObject);
    }
    public void RecieveHit()
    {
        healt--;
        if (healt <= 0)
        {
            DestroyObject();
        }
    }
}
