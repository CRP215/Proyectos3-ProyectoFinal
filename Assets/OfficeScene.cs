using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OfficeScene : MonoBehaviour
{
    public GameObject[] images;
    private int num = 1;
    public Button but;
    public string nextScene;

    // Start is called before the first frame update
    void Start()
    {
        images[0].SetActive(true);
        but = GameObject.Find("Next").GetComponent<Button>();

    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject g in images)
        {
            if(g.name == num.ToString())
            {
                g.SetActive(true);
            } else g.SetActive(false);
        }
        Debug.Log(num);
    }

    public void AddImage(string scene1)
    {
        nextScene = scene1;
        if (num<4) { num++; }
        else if (num >= 4)
        {
            num++;
            StartCoroutine("Next");
            but.gameObject.SetActive(false);
        }
    }

    public IEnumerator Next()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(nextScene);
    }
    public void callNext()
    {
        StartCoroutine("Next");
    }
}
