using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class SecondLevel : MonoBehaviour
{
    public string endScene;
    public GameObject firstPart;
    public GameObject secondPart;

    public bool canPassPart = false;
    public float partNumber;

    public GameManager gm;


    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (canPassPart)
            {
                partNumber++;
            }
        }
    }

    public void Update()
    {
        canPassPart = gm.canPassLevel;
        switch (partNumber)
        {
            case 0:
                firstPart.SetActive(true);
                secondPart.SetActive(false);
                break;
            case 1:
                secondPart.SetActive(true);
                firstPart.SetActive(false);
                break;
            case 2:
                SceneManager.LoadScene(endScene);
                break;
        }
    }

}
