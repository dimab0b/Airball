using Mirror;
using UnityEngine;
using System.Collections;
public class ball : NetworkBehaviour
{
    public Vector2 PosBall;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] public int ScorePlayer1, ScorePlayer2;
    private float MaxSpeed = 15f;

    private float timeSinceLastScoreUpdate = 0f;
    private float timeBetweenScoreUpdates = 1f;

    private Score ScoreScript;
    
    public override void OnStartServer()
    {
        base.OnStartServer();
        ScoreScript = GameObject.Find("ScoreManager").GetComponent<Score>();
        if (ScoreScript == null)
            Debug.Log("There's no GameObject ScoreScript");

        rb.velocity = Vector2.zero;
    }

    void FixedUpdate()
    {
        SpeedLimit();

        //Goal Update
        timeSinceLastScoreUpdate += Time.deltaTime;
        if (hasAuthority && timeSinceLastScoreUpdate >= timeBetweenScoreUpdates)
        {
            GoalUpdate();
            timeSinceLastScoreUpdate -= timeBetweenScoreUpdates;
        }
        //Goal Update
    }
    [Command]
    private void GoalUpdate()
    {
        
        if (rb.transform.position.x >= 10.00)
        {
            ScoreScript.Increment(1); //increment score for p1
            Debug.Log("Goal for Player 1 in GoalUpdate, Yeah!");
        }
        else if (rb.transform.position.x <= -10.00)
        {
            ScoreScript.Increment(2); //increment score for p2
            Debug.Log("Goal for Player 2 in GoalUpdate, Yeah!");
        }
    }
    
    private void SpeedLimit()
    {
        if (rb.velocity.magnitude > MaxSpeed)
        {
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, MaxSpeed);
        }
    }
}