using Mirror;
using UnityEngine.SceneManagement;
using UnityEngine;

public class player1behaviour : NetworkBehaviour // Сетевой объект
{
    private float borderY = 3.9f; private float borderX1 = -8.1f; private float borderX2 = -0.8f;
    Rigidbody2D rb;
    CircleCollider2D cc;
    [SyncVar] public string matchID;
    bool facingRight = true;
    Vector3 oldMousePos; // оптимизацияs

    private NetworkMatch networkMatch;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CircleCollider2D>();
        networkMatch = GetComponent<NetworkMatch>();
    }
   
    void FixedUpdate()
    {
        if (hasAuthority && isLocalPlayer && Input.GetMouseButton(0)) // Есть ли права изменять объект
        {
            Vector3 mousePos = Input.mousePosition;
            if (mousePos == oldMousePos) // не делать лишних движений, если ничего не поменялось
                return;

            Vector3 wantedPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10));

            wantedPos.x = Mathf.Clamp(wantedPos.x, borderX1, borderX2);
            wantedPos.y = Mathf.Clamp(wantedPos.y, -borderY, borderY);

            //Debug.Log(mousePos);
            //Debug.Log(cc.transform.position);

            //if (cc.OverlapPoint(mousePos))
           // if (MainMenu.instance.inGame)
            //{
                rb.MovePosition(wantedPos);

                oldMousePos = mousePos;
            //}
        }
    }
    public void HostGame()
    {
        string ID = MainMenu.GetRandomID();
        CmdHostGame(ID);
    }

    [Command]
    public void CmdHostGame(string ID)
    {
        matchID = ID;
        if (MainMenu.instance.HostGame(ID, gameObject))
        {
            Debug.Log("Лобби было создано успешно");
            networkMatch.matchId = ID.ToGuid();
            TargetHostGame(true, ID);
        }
        else
        {
            Debug.Log("Ошибка в создании лобби");
            TargetHostGame(false, ID);
        }
    }
    [TargetRpc]
    void TargetHostGame(bool success, string ID)
    {
        matchID = ID;
        Debug.Log($"ID {matchID} == {ID}");
        MainMenu.instance.HostSuccess(success, ID);
    }

    public void BeginGame()
    {
        CmdBeginGame();
    }

    [Command]
    public void CmdBeginGame()
    {
        MainMenu.instance.BeginGame(matchID);
        Debug.Log("Игра начилась");
    }

    public void StartGame()
    {
        TargetBeginGame();
    }

    [TargetRpc]
    void TargetBeginGame()
    {
        Debug.Log($"ID {matchID} | начало");
        DontDestroyOnLoad(gameObject);
        transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);
        MainMenu.instance.inGame = true;
        facingRight = true;
        SceneManager.LoadScene("gamegame", LoadSceneMode.Additive + 1);
    }
}
