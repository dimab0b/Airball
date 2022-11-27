using UnityEngine;
using Mirror;


public class NetworkUI : NetworkManager
{
    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(200, -200, 100, 100));
        /*    if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
            {
                if (GUILayout.Button("Host")) NetworkManager.Singleton.StartHost();
                if (GUILayout.Button("Server")) NetworkManager.Singleton.StartServer();
                if (GUILayout.Button("Client")) NetworkManager.Singleton.StartClient();
            }
        */
    }

    /*[SerializeField] public GameObject playerPrefabA; //add prefab in inspector
    [SerializeField] public GameObject playerPrefabB; //add prefab in inspector

    [ServerRpc(RequireOwnership = false)] //server owns this object but client can request a spawn
    public void SpawnPlayerServerRpc(ulong clientId, int prefabId)
    {
        GameObject newPlayer; NetworkObject netObj;
        if (prefabId == 0)
            newPlayer = Instantiate(playerPrefabA) as GameObject;
        else
            newPlayer = Instantiate(playerPrefabB) as GameObject;
        netObj = newPlayer.GetComponent<NetworkObject>();
        newPlayer.SetActive(true);
        netObj.SpawnAsPlayerObject(clientId, true);
    }*/

}