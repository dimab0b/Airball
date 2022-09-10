using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player2behaviour : MonoBehaviour
{
    public float speed = 10.0f;
    public float border = 3.4f;
    public Rigidbody2D rb;
    void Update()
    {
        Vector3 pos = transform.position;
        
        if (Input.GetKey(KeyCode.UpArrow) && pos.y <= border)
        {
            pos.y += speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow) && pos.y >= -border)
        {
            pos.y -= speed * Time.deltaTime;
        }
        transform.position = pos;
    }

}
