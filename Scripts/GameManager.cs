using UnityEngine;
using Unity.Netcode;
using System.Collections;

public class GameManager : NetworkBehaviour
{
    public NetworkVariable<int> playerCounts = new NetworkVariable<int>(0,NetworkVariableReadPermission.Everyone,NetworkVariableWritePermission.Server);
    [SerializeField] int playerNeeded = 2;

    public GameObject playStateManagerPrefab;

    public GameObject MainMenuState, LobbyState, PlayState;

    //public MyNetworkManager m_NetworkManager;

   
    public static GameManager Singleton;

    public GameObject Countdown;


    private void Awake()
    {
        Countdown.SetActive(false);
        Singleton = this;
    }

    public override void OnNetworkSpawn()
    {
        if (IsHost)
        {
            NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
            playerCounts.Value = 1;
        }

        

    }

    void OnClientConnected(ulong clientId)
    {

        if (EnoughPlayers())
        {
            Debug.Log("Start Timer");
            CountdownStartOnClientRpc();
        }
    }

    bool EnoughPlayers()
    {
        playerCounts.Value = NetworkManager.Singleton.ConnectedClientsList.Count;
        return playerCounts.Value == playerNeeded;
    }

    
    

    [ClientRpc]
    private void CountdownStartOnClientRpc()
    {
        Countdown.SetActive(true);
    }


    [ClientRpc]
    public void StartGameClientRpc()
    {

        StateManager.Instance.SwitchStateOnServerRpc(LobbyState, PlayState);
        if (IsHost)
        {
            if (PlayStateManager.Instance == null)
            {

                GameObject manager = Instantiate(playStateManagerPrefab);
                manager.GetComponent<NetworkObject>().Spawn();

            }
        }
        
    }

}
