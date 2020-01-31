using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public GameManager gm;

    public GameObject iddle;
    public GameObject moving;
    public GameObject bossBattleImage;
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        moving.SetActive(false);
        
    }

    void Update()
    {
        if (!gm.noEnemies)
        {
            if (gm.lastEnemy)
            {
                iddle.SetActive(false);
                moving.SetActive(true);
                bossBattleImage.SetActive(true);
            }
        }
        if (gm.noEnemies)
        {
            Destroy(this);
        }
    }
}
