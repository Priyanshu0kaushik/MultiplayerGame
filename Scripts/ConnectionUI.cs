using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;

public class ConnectionUI : MonoBehaviour
{
   

    public void HostServer()
    {
        if (!NetworkManager.Singleton.IsServer && !NetworkManager.Singleton.IsClient)
        {
            NetworkManager.Singleton.StartHost();
            StateManager.Instance.SwitchStateOnServerRpc(GameManager.Singleton.MainMenuState, GameManager.Singleton.LobbyState);
        }

    }

    public void ConnectClient()
    {
        if (!NetworkManager.Singleton.IsServer && !NetworkManager.Singleton.IsClient)
        {
            NetworkManager.Singleton.StartClient();
            StateManager.Instance.SwitchStateOnServerRpc(GameManager.Singleton.MainMenuState, GameManager.Singleton.LobbyState);
        }
    }
}
