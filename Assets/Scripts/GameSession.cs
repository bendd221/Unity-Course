using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using TMPro;

public class GameSession : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] int playerLives = 3;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;
    int currentPlayerLives;
    
    private string level;

    void Awake() {
        currentPlayerLives = playerLives;
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if(numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        livesText.text = currentPlayerLives.ToString();
        scoreText.text = "0";
    }

    public void ProcessPlayerDeath()
    {
        if(currentPlayerLives > 1)
        {
            TakeLife();
        }
        else
        {
            Debug.Log("hello");
            ResetGameSession();
        }
    }

    void TakeLife()
    {
        
        currentPlayerLives -= 1;
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
        livesText.text = currentPlayerLives.ToString();
    }

    public void increaseScore(int points)
    {
        scoreText.text = (Int32.Parse(scoreText.text) + points).ToString();
    }

    void ResetGameSession()
    {
        FindObjectOfType<ScenePersist>().ResetScenePersists();
        currentPlayerLives = playerLives;
        scoreText.text = "0";
        FadeToLevel("Level 1");
        // Destroy(gameObject);
    }

    public void FadeToLevel(string levelName)
    {
        level = levelName;
        animator.SetTrigger(("FadeOut"));
    }

    public void OnFadeComplete()
    {
        FindObjectOfType<ScenePersist>().ResetScenePersists();
        SceneManager.LoadScene(level);
        animator.SetTrigger(("FadeIn"));
    }
}
