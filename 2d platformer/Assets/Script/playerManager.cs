using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class playerManager : MonoBehaviour
{
    // set a max health
    public int maxHealth;
    public int currentHealth;
    playerMovement playerMovement;
    private int coinCount;
    BossBehavior bossbehavior;
    public TextMeshProUGUI scoreText;

    private void Start()
    {

        playerMovement = GetComponent<playerMovement>();
        UpdateScore(0);
        coinCount = 0;
    }
    public void UpdateScore(int scoreToAdd)
    {
        coinCount += scoreToAdd;
        scoreText.text = "Score: " + coinCount;
    }
    private void Update()
    {
        if (currentHealth <= 0)
        {
            PauseGame();
        }
    }
    public bool Pickupitem(GameObject obj)
    {
        switch (obj.tag)
        {
            case "Currency":
                coinCount++;
                UpdateScore(1);
                Debug.Log("You grabbed a coin");
                return true;
            case "Speed+":
                //call function here
                playerMovement.SpeedPowerUp();
                return true;
            default:
                return false;
        }
    }
    public void takeDamage()
    {
        currentHealth -= 1;
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;

    }
    
    
}
   
