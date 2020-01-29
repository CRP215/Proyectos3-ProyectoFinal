using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroup : MonoBehaviour
{
    public enum GROUP_TYPE { PING_PONG, STAR, RANDOM }

    public GROUP_TYPE m_groupType;

    [SerializeField]
    public List<EnemyInScene> m_enemyList;

    private bool m_enableGroup  = false;
    private int  m_currentEnemy = 0;
    
    public void InitEnemy (Enemy enemy)
    {
        switch (m_groupType)
        {
            case GROUP_TYPE.PING_PONG:
                int modifier = (m_currentEnemy % 2 == 0) ? 2 : -2;
                m_enemyList[m_currentEnemy].m_enemyObject.GetComponent<Enemy>().m_desviation = new Vector2 (modifier, 0);
                break;
            case GROUP_TYPE.STAR:

                float desviationX = 0;
                float desviationY = 0;

                // Rellenar

                m_enemyList[m_currentEnemy].m_enemyObject.GetComponent<Enemy>().m_desviation = new Vector2 (desviationX, desviationY);
                break;
            case GROUP_TYPE.RANDOM:
                m_enemyList[m_currentEnemy].m_enemyObject.GetComponent<Enemy>().m_desviation = new Vector2 (Random.Range(-15, 15), Random.Range(-5, 5));
                break;
            default:
                break;
        }
        
        enemy.gameObject.SetActive(true);
        m_currentEnemy++;
    }

    private void Start    ()
    {
        for (int i = 0; i < m_enemyList.Count; i++)
        {
            m_enemyList[i].SetMyGroup(this);
        }
    }

    private void Update   ()
    {
        if (m_enableGroup)
        { 
            for (int i = 0; i < m_enemyList.Count; i++)
            {
                m_enemyList[i].AddTime(Time.deltaTime);
            }
        }
    }

    void OnTriggerEnter2D (Collider2D coll)
    {
        if (coll.tag.Equals("Player"))
        {
            m_enableGroup = true;
        }
    }
}
