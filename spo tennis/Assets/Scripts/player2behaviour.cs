using Mirror;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player2behaviour : NetworkBehaviour
{
    private float borderY = 3.9f; private float borderX1 = 0.8f; private float borderX2 = 8.1f;
    Rigidbody2D rb;
    Vector3 oldMousePos;
    public static player2behaviour localPlayer2;
    bool facingRight = true;
    public player1behaviour player1Behaviour;
    
    private NetworkMatch networkMatch;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        networkMatch = GetComponent<NetworkMatch>();
    }
    void FixedUpdate()
    {
        if (hasAuthority && isLocalPlayer && Input.GetMouseButton(0)) // ���� �� ����� �������� ������
        {
            Vector3 mousePos = Input.mousePosition;
            if (mousePos == oldMousePos) // �� ������ ������ ��������, ���� ������ �� ����������
                return;

            Vector3 wantedPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10));

            wantedPos.x = Mathf.Clamp(wantedPos.x, borderX1, borderX2);
            wantedPos.y = Mathf.Clamp(wantedPos.y, -borderY, borderY);

            rb.MovePosition(wantedPos);

            oldMousePos = mousePos;
        }
    }
    public void JoinGame(string inputID)
    {
        CmdJoinGame(inputID);
    }

    [Command]
    public void CmdJoinGame(string ID)
    {
        player1Behaviour.matchID = ID;
        if (MainMenu.instance.JoinGame(ID, gameObject))
        {
            Debug.Log("�������� ����������� � �����");
            networkMatch.matchId = ID.ToGuid();
            TargetJoinGame(true, ID);
        }
        else
        {
            Debug.Log("�� ������� ������������");
            TargetJoinGame(false, ID);
        }
    }

    [TargetRpc]
    void TargetJoinGame(bool success, string ID)
    {
        player1Behaviour.matchID = ID;
        Debug.Log($"ID {player1Behaviour.matchID} == {ID}");
        MainMenu.instance.JoinSuccess(success, ID);
    }

    public void HostGame()
    {
        string ID = MainMenu.GetRandomID();
        CmdHostGame(ID);
    }

    [Command]
    public void CmdHostGame(string ID)
    {
        player1Behaviour.matchID = ID;
        if (MainMenu.instance.HostGame(ID, gameObject))
        {
            Debug.Log("����� ���� ������� �������");
            networkMatch.matchId = ID.ToGuid();
            TargetHostGame(true, ID);
        }
        else
        {
            Debug.Log("������ � �������� �����");
            TargetHostGame(false, ID);
        }
    }
    [TargetRpc]
    void TargetHostGame(bool success, string ID)
    {
        player1Behaviour.matchID = ID;
        Debug.Log($"ID {player1Behaviour.matchID} == {ID}");
        MainMenu.instance.HostSuccess(success, ID);
    }

    public void BeginGame()
    {
        CmdBeginGame();
    }

    [Command]
    public void CmdBeginGame()
    {
        MainMenu.instance.BeginGame(player1Behaviour.matchID);
        Debug.Log("���� ��������");
    }

    public void StartGame()
    {
        TargetBeginGame();
    }

    [TargetRpc]
    void TargetBeginGame()
    {
        Debug.Log($"ID {player1Behaviour.matchID} | ������");
        DontDestroyOnLoad(gameObject);
        MainMenu.instance.inGame = true;
        transform.localScale = new Vector3(2.0f, 2.0f, 2.0f); //������ ������ ������ (x, y, z)
        facingRight = true;
        SceneManager.LoadScene("gamegame", LoadSceneMode.Additive + 1);
    }
}