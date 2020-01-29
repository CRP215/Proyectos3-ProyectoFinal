using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OfficeTrigger : MonoBehaviour
{
    public GameManager gameManager;

    public BoxCollider2D Player;
    public int m_enemies;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>();
    }


    private void Update()
    {
        m_enemies = gameManager.m_enemiesNumber;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.tag.Equals("Player"))
       {
            if (m_enemies == 0)
            {
                Debug.Log("Voy a la office");
                SceneManager.LoadScene("3_Office");
            }
            
       }
    }

}
