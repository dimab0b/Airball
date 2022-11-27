using Mirror;
using UnityEngine;
public class NetworkPlayerSpawn : NetworkManager
{
    [SerializeField] public GameObject playerPrefabA; //add prefab in inspector
    [SerializeField] public GameObject playerPrefabB; //add prefab in inspector

    //[ServerRpc(RequireOwnership = false)] //server owns this object but client can request a spawn
    /*public void SpawnPlayerServerRpc(ulong clientId, int prefabId)
    {
        GameObject newPlayer; NetworkObject netObj;
        if (prefabId == 0)
            newPlayer = Instantiate(playerPrefabA) as GameObject;
        else
            newPlayer = Instantiate(playerPrefabB) as GameObject;
        netObj = newPlayer.GetComponent<NetworkObject>();
        newPlayer.SetActive(true);
        netObj.SpawnAsPlayerObject(clientId, true);
    */
}