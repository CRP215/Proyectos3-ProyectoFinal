using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OfficeTrigger : MonoBehaviour
{
    public GameManager gameManager;

    public BoxCollider2D Player;
    public int m_enemies;

    public string nextScene;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>();
        Debug.Log(SceneManager.GetActiveScene().name);
        switch (SceneManager.GetActiveScene().name)
        {
            case "2_Gym":
                nextScene = "3_Office";
                break;
            case "4_Apollos":
                nextScene = "5_Call2";
                break;
            case "6_ApolloHouse":
                nextScene = "7_End";
                break;

        }
        Debug.Log(nextScene);
    }


    private void Update()
    {
        m_enemies = gameManager.enemiesNumber;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.tag.Equals("Player"))
       {
            if (m_enemies == 0)
            {
                Debug.Log("Voy a la sigiente");
                SceneManager.LoadScene(nextScene);
            }
            
       }
    }

}
