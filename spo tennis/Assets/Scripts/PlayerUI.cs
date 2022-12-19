using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : NetworkBehaviour
{
    public Text PlayerText;
    static public int n = 0;
    private player1behaviour player1;
    private player2behaviour player2;
    NetMan player;
    //TurnManager turnManager;
    public void SetPlayer(player1behaviour player1, player2behaviour player2)
    {
        if (n == 0)
        {
            this.player1 = player1;
            PlayerText.text = "Player 1";
            n = 1;
        }
        else if(n == 1)
        {
            this.player2 = player2;
            PlayerText.text = "Player 2";
            n = 0;
        }
    }
}
