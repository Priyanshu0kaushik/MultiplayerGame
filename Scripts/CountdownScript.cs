using UnityEngine;
using System.Collections;
using TMPro;

public class CountdownScript : MonoBehaviour
{
    [SerializeField] int time = 10;
    TextMeshProUGUI textGO;
    [SerializeField] string InitialText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textGO = gameObject.GetComponent<TextMeshProUGUI>();
        StartCoroutine(CountdownCoroutine(time));
    }

    private IEnumerator CountdownCoroutine(int Time)
    {
        for(int i = Time; i >= 0; i--)
        {
            textGO.text = InitialText + i.ToString();
            yield return new WaitForSeconds(1);
        }
        CallStartGameOnServer();
    }

    void CallStartGameOnServer()
    {
        GameManager.Singleton.StartGameClientRpc();
    }
}
