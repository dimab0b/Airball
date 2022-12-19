using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;


public class Connection : MonoBehaviour
{
    public NetworkManager networkManager;
    private void Start()
    {
        networkManager = NetworkManager.FindObjectOfType<NetworkManager>();
    }
    public void StartHosts()
    {
        networkManager.StartHost();
    }
    public void JoinClient()
    {
        networkManager.networkAddress = "localhost";
        networkManager.StartClient();
    }
    public void StopButton()
    {
        // stop host if host mode
        if (NetworkServer.active && NetworkClient.isConnected)
        {
            networkManager.StopHost();
            
        }
        // stop client if client-only
        else if (NetworkClient.isConnected)
        {
            networkManager.StopClient();
        }
        
        SceneManager.LoadScene("Menu", LoadSceneMode.Additive - 1);
    }
}
