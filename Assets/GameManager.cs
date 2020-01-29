using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int m_enemiesNumber;
    

    void Update()
    {
        m_enemiesNumber = GameObject.FindGameObjectsWithTag("Enemy").Length;
        //m_enemiesNumber = GameObject.FindObjectsOfType<Enemy>().Length;
        Debug.Log("Hay " + m_enemiesNumber + " enemigos");
    }
}
