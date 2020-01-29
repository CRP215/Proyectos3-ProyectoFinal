using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstLoadSceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Wait();
    }

    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("2_Gym");
    }
}
