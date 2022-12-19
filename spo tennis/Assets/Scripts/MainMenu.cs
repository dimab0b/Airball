using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
using System;
using System.Security.Cryptography;
using System.Text;

[System.Serializable]
public class Match : NetworkBehaviour
{
    public string ID;
    public readonly List<GameObject> players = new List<GameObject>();

    public Match(string ID, GameObject player)
    {
        this.ID = ID;
        players.Add(player);
    }
}
public class MainMenu : NetworkBehaviour
{
    public static MainMenu instance;
    public readonly SyncList<Match> matches = new SyncList<Match>();
    public readonly SyncList<string> matchIDs = new SyncList<string>();
    public InputField JoinInput;
    public Button HostButton;
    public Button JoinButton;
    public Canvas LobbyCanvas;
    public GameObject UIPlayerPrefab;

    public Transform UIPLayerParent;
    public Text IDText;
    public Button BeginGameButton;
    public GameObject TurnManager;
    public bool inGame;

    private NetMan netMan;

    private void Start()
    {
        
        instance = this;
    }

    private void Update()
    {
        if (!inGame)
        {
            player1behaviour[] players1 = FindObjectsOfType<player1behaviour>();
            player2behaviour[] players2 = FindObjectsOfType<player2behaviour>();
            ball balls = FindObjectOfType<ball>();

            for (int i = 0; i < players1.Length; i++)
            {
                players1[i].gameObject.transform.localScale = Vector3.zero;
            }
            for (int i = 0; i < players2.Length; i++)
            {
                players2[i].gameObject.transform.localScale = Vector3.zero;
                balls.gameObject.transform.localScale = Vector3.zero;
            }
        }
    }

    public void Host()
    {
        JoinInput.interactable = false;
        HostButton.interactable = false;
        JoinButton.interactable = false;
        player1behaviour[] players1 = FindObjectsOfType<player1behaviour>();

        players1[0].HostGame();
    }

    public void HostSuccess(bool success, string matchID)
    {
        if (success)
        {
            LobbyCanvas.enabled = true;
            player1behaviour[] players1 = FindObjectsOfType<player1behaviour>();
            SpawnPlayerUIPrefab(players1[0], null);
            IDText.text = matchID;
            BeginGameButton.interactable = true;
        }
        else
        {
            JoinInput.interactable = true;
            HostButton.interactable = true;
            JoinButton.interactable = true;
        }
    }

    public void Join()
    {
        JoinInput.interactable = false;
        HostButton.interactable = false;
        JoinButton.interactable = false;
        player2behaviour[] players2 = FindObjectsOfType<player2behaviour>();
        players2[0].JoinGame(JoinInput.text.ToUpper());
    }

    public void JoinSuccess(bool success, string matchID)
    {
        if (success)
        {
            LobbyCanvas.enabled = true;
            player2behaviour[] players2 = FindObjectsOfType<player2behaviour>();

            SpawnPlayerUIPrefab(null, players2[0]); ;
            IDText.text = matchID;
            BeginGameButton.interactable = false;
        }
        else
        {
            JoinInput.interactable = true;
            HostButton.interactable = true;
            JoinButton.interactable = true;
        }
    }

    public bool HostGame(string matchID, GameObject player)
    {
        if (!matchIDs.Contains(matchID))
        {
            matchIDs.Add(matchID);
            matches.Add(new Match(matchID, player));
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool JoinGame(string matchID, GameObject player)
    {
        if (matchIDs.Contains(matchID))
        {
            for (int i = 0; i < matches.Count; i++)
            {
                if (matches[i].ID == matchID)
                {
                    matches[i].players.Add(player);
                    break;
                }
            }

            return true;
        }
        else
        {
            return false;
        }
    }

    public static string GetRandomID()
    {
        string ID = string.Empty;
        for (int i = 0; i < 5; i++)
        {
            int rand = UnityEngine.Random.Range(0, 36);
            if (rand < 26)
            {
                ID += (char)(rand + 65);
            }
            else
            {
                ID += (rand - 26).ToString();
            }
        }
        return ID;
    }

    public void SpawnPlayerUIPrefab(player1behaviour player1, player2behaviour player2)
    {
        GameObject newUIPlayer = Instantiate(UIPlayerPrefab, UIPLayerParent);
        
        if (PlayerUI.n == 0)
        {
            newUIPlayer.GetComponent<PlayerUI>().SetPlayer(player1, null);
        }
        else if (PlayerUI.n == 1)
        {
            newUIPlayer.GetComponent<PlayerUI>().SetPlayer(null, player2);
        }
        
    }

    public void StartGame()
    {
        player1behaviour[] players1 = FindObjectsOfType<player1behaviour>();
        player2behaviour[] players2 = FindObjectsOfType<player2behaviour>();

        players1[0].BeginGame();
        players2[0].BeginGame();
    }

    public void BeginGame(string matchID)
    {
        GameObject newTurnManager = Instantiate(TurnManager);
        NetworkServer.Spawn(newTurnManager);
        newTurnManager.GetComponent<NetworkMatch>().matchId = matchID.ToGuid();
       // TurnManager turnManager = newTurnManager.GetComponent<TurnManager>();
        
        for (int i = 0; i < matches.Count; i++)
        {
            if (matches[i].ID == matchID)
            {
                int k = 1;
                foreach (var player in matches[i].players)
                {
                    if (k == 1)
                    {
                        player1behaviour player1 = player.GetComponent<player1behaviour>();
                        //turnManager.AddPlayer(netMan);
                        player1.StartGame();
                        k++;
                    }
                    else if(k == 2)
                    {
                        player2behaviour player2 = player.GetComponent<player2behaviour>();
                      //  turnManager.AddPlayer(netMan);
                        player2.StartGame();
                        k--;
                    }
                    
                }
                break;
            }
        }
    }
}

public static class MatchExtension
{
    public static Guid ToGuid(this string id)
    {
        MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
        byte[] inputBytes = Encoding.Default.GetBytes(id);
        byte[] hasBytes = provider.ComputeHash(inputBytes);

        return new Guid(hasBytes);
    }
}