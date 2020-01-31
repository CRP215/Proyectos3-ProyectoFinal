using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int enemiesNumber;
    public bool canPassLevel;
    public bool lastEnemy;
    public bool noEnemies = false;

    void Update()
    {
        enemiesNumber = GameObject.FindGameObjectsWithTag("Enemy").Length;
        //enemiesNumber = GameObject.FindObjectsOfType<Enemy>().Length;
        Debug.Log("Hay " + enemiesNumber + " enemigos");

        if(enemiesNumber <= 0)
        {
            canPassLevel = true;
        }
        if (enemiesNumber == 1)
        {
            lastEnemy = true;
        }
        if (enemiesNumber == 0)
        {
            noEnemies = true;
        }
    }
}
