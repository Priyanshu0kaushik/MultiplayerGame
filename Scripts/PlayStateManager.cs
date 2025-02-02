using UnityEngine;
using TMPro;
using Unity.Netcode;
using System.Collections.Generic;

public class PlayStateManager : NetworkBehaviour
{
    public static PlayStateManager Instance;
    public List<Transform> spawnPositions = new List<Transform>();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            NetworkObject.Despawn(true); 
            return;
        }

        Instance = this;

        if (IsServer || IsHost) 
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        GetSpawnPointsClientRpc();
        AssignSpawnPointsServerRpc();
    }

    [ClientRpc]
    void GetSpawnPointsClientRpc()
    {
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");

        Debug.Log($"Spawnpoints : {spawnPoints.Length}");
        if (spawnPoints.Length == 2)
        {
            spawnPositions[0] = GameObject.FindGameObjectsWithTag("SpawnPoint")[0].GetComponent<Transform>();
            spawnPositions[1] = GameObject.FindGameObjectsWithTag("SpawnPoint")[1].GetComponent<Transform>();
        }
    }

    [ServerRpc(RequireOwnership =false)]
    void AssignSpawnPointsServerRpc()
    {
        NetworkObject playerObject;
        for (int j = 0; j < NetworkManager.Singleton.ConnectedClientsIds.Count; j++)
        {
            ulong id = NetworkManager.Singleton.ConnectedClientsIds[j];
            
            if (NetworkManager.Singleton.ConnectedClients.TryGetValue(id, out var EnemyClient))
            {
                playerObject = EnemyClient.PlayerObject;
                playerObject.GetComponent<PlayerNetworkScript>().SpawnPlayerOnSpawnPointServerRpc(spawnPositions[j].position);
                
            }
        }
    }




    [ClientRpc]
    public void UpdateUiClientRpc()
    {
        Debug.Log("Death");

        ulong[] clientIds = new ulong[2];
        int[] deaths = new int[2];

        for (int i = 0; i < NetworkManager.Singleton.ConnectedClientsIds.Count; i++)
        {
            if (NetworkManager.Singleton.ConnectedClients.TryGetValue(NetworkManager.Singleton.ConnectedClientsIds[i], out var client))
            {
                var playerObject = client.PlayerObject;
                var playerScript = playerObject.GetComponent<PlayerNetworkScript>();
                clientIds[i] = NetworkManager.Singleton.ConnectedClientsIds[i];
                deaths[i] = playerScript.deaths.Value;
            }
        }

        // Send the score data to all clients.
        UpdateUIClientRpc(clientIds, deaths);
    }


    [ClientRpc]
    void UpdateUIClientRpc(ulong[] ids, int[] deaths)
    {
        int myScore = 0;
        int enemyScore = 0;
        ulong localClientId = NetworkManager.Singleton.LocalClientId;

        for(int i = 0;i<ids.Length;i++)
        {
            if (ids[i] == localClientId) enemyScore = deaths[i];
            else myScore = deaths[i];
        }

        ScoreUpdate.Instance.myScoreText.text =  myScore.ToString();
        ScoreUpdate.Instance.EnemyScoreText.text = enemyScore.ToString();
    }

}

