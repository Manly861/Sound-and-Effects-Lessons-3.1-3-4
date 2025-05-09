using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private int score;
    public TextMeshProUGUI scoreText;
    private bool hasScored = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateScoreText(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateScoreText(int AddScore)
    {
        score += AddScore;
        scoreText.text = "Score: " + score;
    }
}