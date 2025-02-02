using UnityEngine;
using Unity.Netcode;
using TMPro;

public class PlayerCountScript : MonoBehaviour
{
    TextMeshProUGUI Text;
    string InitialText = "Players ";
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Text = gameObject.GetComponent<TextMeshProUGUI>();
        GameManager.Singleton.playerCounts.OnValueChanged+= PlayerCounts_OnValueChanged;
    }

    void PlayerCounts_OnValueChanged(int oldVal,int newVal)
    {
        Text.text = InitialText + newVal.ToString();
    }

    


    
}
