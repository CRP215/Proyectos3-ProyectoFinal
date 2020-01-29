using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject Player;
    public float screenXRight;
    public float screenXLeft;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
        float w = Mathf.Clamp(Player.transform.position.x, screenXLeft, screenXRight);
        transform.position = new Vector3(w, 0, -10);
    }
}
