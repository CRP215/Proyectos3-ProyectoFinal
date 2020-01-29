using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FirstSceneController : MonoBehaviour
{
    public SpriteRenderer       image;
    public Sprite[]             frames;
    public Button               m_button;
    public Text                 m_buttonText;
    public int                  m_currentFrame = -1;

    void Start()
    {
        image = GameObject.Find("ImageCall").GetComponent<SpriteRenderer>();
        m_button = GameObject.Find("Answer_Btn").GetComponent<Button>();
        m_buttonText = m_button.GetComponentInChildren<Text>();
    }

    public void Next()
    {
        
        switch (m_currentFrame)
        {
            case 0:
                Destroy(GameObject.Find("ImageCall").GetComponent<Animator>());
                image.sprite = frames[0];
                m_currentFrame++;
                m_buttonText.text = "Next";
                break;
            case 1:
                image.sprite = frames[1];
                m_currentFrame++;
                break;
            case 2:
                image.sprite = frames[2];
                m_currentFrame++;
                break;
            case 3:
                image.sprite = frames[3];
                m_currentFrame++;
                break;
            case 4:
                image.sprite = frames[4];
                Wait();
                m_button.gameObject.SetActive(false);
                break;
        }
    }

    public void Wait()
    {
        StartCoroutine("wait");
    }

    public IEnumerator wait()
    {
        Debug.Log("Empiezo a esperar");
        yield return new WaitForSeconds(2f);
        Debug.Log("Chaito");
        SceneManager.LoadScene("2_Gym");
    }

}
