using TMPro;
using UnityEngine;

public class ScoreUpdate : MonoBehaviour
{
    public TextMeshProUGUI myScoreText, EnemyScoreText;
    public static ScoreUpdate Instance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
