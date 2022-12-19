using Mirror;
using UnityEngine;

public class NetMan : NetworkManager
{
    GameObject ball;

    public struct OnlinePlayers : NetworkMessage
    {
        public int n;
    }
    public GameObject PlayerPrefab1, PlayerPrefab2, ballPrefab;
    
    public override void OnStartServer()
    {
        base.OnStartServer();

        NetworkServer.RegisterHandler<OnlinePlayers>(OnPlayerJoin);
    }
    public override void OnClientConnect()
    {
        base.OnClientConnect();
        OnlinePlayers plus = new OnlinePlayers { n = numPlayers};

        NetworkClient.Send(plus);
    }
     void OnPlayerJoin(NetworkConnectionToClient conn, OnlinePlayers msg)
    {
        if (numPlayers % 2 == 0)
        {
            GameObject gameObject = Instantiate(PlayerPrefab1);
            NetworkServer.AddPlayerForConnection(conn, gameObject);

            return;
        }

        if (numPlayers % 2 == 1)
        {
            GameObject gameObject = Instantiate(PlayerPrefab2);
            NetworkServer.AddPlayerForConnection(conn, gameObject);
            GameObject ball = Instantiate(ballPrefab);
            NetworkServer.Spawn(ball, conn);

            return;
        }
    }
    public void OnServerDisconnect(NetworkConnection conn)
    {
        if (ball != null)
        {
            NetworkServer.Destroy(ball);
        }
    }
}
