using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public    string       m_itemName;
    public    bool         m_expiresImmediately;
    public    int          m_expiresWithTime;
    public    GameObject   m_effect;
    public    AudioSource  m_sfx;
    public    Material     m_playerMaterial;

    private   float        m_counterTime = 0;
    protected Player m_player;
    private   bool         m_enterActionDone = false;

    public virtual void EnterAction   (Player player)
    {
        m_player = player;

        if (m_sfx != null)
        {
            
        }

        if (m_effect != null)
        {

        }

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        m_counterTime = 0;
        
        ExecuteAction ();

        m_enterActionDone = true;
    }

    public virtual void ExecuteAction ()
    {
        Debug.Log("Recibo Item");
        if (m_expiresImmediately)
        {
            ExitAction ();
        }
    }

    public virtual void ExitAction    ()
    {
        m_enterActionDone = false;

        if (m_sfx != null)
        {

        }
    }

    private void Update()
    {
        if (m_enterActionDone && !m_expiresImmediately && m_expiresWithTime > 0)
        {
            m_counterTime += Time.deltaTime;

            if (m_counterTime < m_expiresWithTime)
            {
                ExecuteAction();
            }
            else
            {
                ExitAction();
            }
        }
    }
}
