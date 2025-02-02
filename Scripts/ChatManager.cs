using UnityEngine;
using TMPro;
using Unity.Netcode;

public class ChatManager : NetworkBehaviour
{
    public TextMeshProUGUI SenderMessage, RecieverMessage;
    public TMP_InputField inputField;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnClickSendMsg()
    {
        if(inputField.text != "")
        {
            SendMessageToServerRpc(inputField.text, NetworkManager.LocalClientId);
        }
    }

    [ServerRpc(RequireOwnership =false)]
    void SendMessageToServerRpc(string message, ulong SenderID)
    {
        SendMessageToClientRpc(message, SenderID);
    }

    [ClientRpc]
    void SendMessageToClientRpc(string message, ulong SenderID)
    {
        if (NetworkManager.LocalClientId != SenderID)
        {
            ShowMessageOnReciever(message);
        }
        else ShowMessageOnSender(message);
    }

    void ShowMessageOnSender(string msg)
    {
        SenderMessage.text = msg;
    }

    void ShowMessageOnReciever(string msg)
    {
        RecieverMessage.text = msg;
    }
    
}
