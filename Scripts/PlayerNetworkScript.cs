using UnityEngine;
using Unity.Netcode;
using System.Collections.Generic;

public class PlayerNetworkScript : NetworkBehaviour
{
    [SerializeField] NetworkVariable<int> health = new NetworkVariable<int>(100);
    public NetworkVariable<int> deaths = new NetworkVariable<int>(0);
    [SerializeField] Vector3 mySpawnPos;


    [ServerRpc(RequireOwnership =false)]
    public void SpawnPlayerOnSpawnPointServerRpc(Vector3 pos)
    {
        SpawnPlayerOnSpawnPointClientRpc(pos);
    }

    [ClientRpc]
    public void SpawnPlayerOnSpawnPointClientRpc(Vector3 pos)
    {
        //Debug.Log(IsOwner);
        Debug.Log($"[ClientRpc] Respawning Player {OwnerClientId} at {pos}");
        transform.position = pos;
        mySpawnPos = pos;
        
    }

    void ScoreUpdate()
    {
        
        PlayStateManager.Instance.UpdateUiClientRpc();
    }

    [ServerRpc]
    public void TakeDamageServerRpc(int damage)
    {
        health.Value -= damage;
        if (health.Value <= 0)
        {
            deaths.Value++;
            health.Value = 100;
            SpawnPlayerOnSpawnPointClientRpc(mySpawnPos);
            ScoreUpdate();
        }
    }


   
}
