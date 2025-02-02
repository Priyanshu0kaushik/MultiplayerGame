using UnityEngine;
using Unity.Netcode;
using System;

public class StateManager : MonoBehaviour
{
    public static StateManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    [ServerRpc]
    public void SwitchStateOnServerRpc(GameObject oldState, GameObject newState)
    {
        oldState.gameObject.SetActive(false);
        newState.gameObject.SetActive(true);

        SwitchStateOnClientRpc(oldState, newState);

        

    }

    [ClientRpc]
    public void SwitchStateOnClientRpc(GameObject oldState, GameObject newState)
    {
        Debug.Log("Running");
        oldState.gameObject.SetActive(false);
        newState.gameObject.SetActive(true);
    }
}
