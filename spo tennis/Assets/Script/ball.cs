using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball : MonoBehaviour
{
    public Vector2 BallForce;
    public bool BallIsActive;
    public Vector3 PosBall;
    public Rigidbody2D rb;
    public GameObject pl1, pl2;
    public int ScorePlayer1, ScorePlayer2;

    void Start()
    {
        transform.position = Vector2.zero;
        BallForce = new Vector2(-250.0f, 250.0f);
    }

    // «ачем нам нужна проверка активности м€ча (BallIsAcrive), если он всегда активен?
    void Update()
    {
        PosBall = transform.position;
        if (!BallIsActive)
        {
             rb.isKinematic = false;
             rb.AddForce(BallForce);
            BallIsActive = !BallIsActive;
        }
        // –естарт игры при вылете м€ча за рамки
        if (BallIsActive && PosBall.x < pl1.transform.position.x - 3)
        {
            Start();
            ScorePlayer2 += 1;
        }

        if (BallIsActive && PosBall.x > pl2.transform.position.x + 3)
        {
            Start();
            ScorePlayer1 += 1;
        }

        /* ¬роде это не нужно
        if (BallIsActive && transform.position.y < -7.6f)
        {
            BallIsActive = !BallIsActive;
            PosBall.x = pl.transform.position.x;
            PosBall.y = -4.2f;
            transform.position = PosBall;
            rb.isKinematic = true;
        }
        */
    }

}
