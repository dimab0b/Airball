using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class Score : NetworkBehaviour
{
    [SerializeField] int scorePlayer1, scorePlayer2;
    public Text ScoreText1, ScoreText2;
    private Rigidbody2D BallPrefabRb, Player1PrefabRb, Player2PrefabRb;
    public void Start()
    {
        scorePlayer1 = 0; 
        ScoreText1.text = 0.ToString();
        scorePlayer2 = 0;
        ScoreText2.text = 0.ToString();
    }
    private void Reset()
    {
        Start();

        Player1PrefabRb = GameObject.Find("player 1(Clone)").GetComponent<Rigidbody2D>();
        Player2PrefabRb = GameObject.Find("player 2(Clone)").GetComponent<Rigidbody2D>();

        BallPrefabRb.transform.position = Vector2.zero;
        BallPrefabRb.velocity = Vector2.zero;
        Player1PrefabRb.transform.position = new Vector2(-7.6f, 0);
        Player2PrefabRb.transform.position = new Vector2(7.6f, 0);
    }

    [ClientRpc]
    public void Increment(int whichScore)
    {
        
        BallPrefabRb = GameObject.Find("Ball(Clone)").GetComponent<Rigidbody2D>();
        if (BallPrefabRb == null)
            Debug.Log("There's no GameObject ball");

        if (whichScore == 1)
        {
            BallPrefabRb.transform.position = new Vector2(2, 0);
            BallPrefabRb.velocity = Vector2.zero;
            

            scorePlayer1++;
            ScoreText1.text = scorePlayer1.ToString();

            Debug.Log("Plus Score for Player 1 in ScoreScript.Increment(), Yeah!");
        }
        if (whichScore == 2)
        { 
            BallPrefabRb.transform.position = new Vector2(-2, 0);
            BallPrefabRb.velocity = Vector2.zero;
            

            scorePlayer2++;
            ScoreText2.text = scorePlayer2.ToString();
            
            Debug.Log("Plus Score for Player 2 in ScoreScript.Increment(), Yeah!");
        }
        ResetScore();
    }
    private void ResetScore()
    {
        if (ScoreText1.text == 7.ToString() || ScoreText2.text == 7.ToString())
        {
            Reset();
        }
    }
}
