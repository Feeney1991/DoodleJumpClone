using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI theFinalScreenScore; // Text which shows final score
    public int playerScore; //The score of the player (passed from GM)
    public GameManager theGM; // The GameManager script that holds the score




    // Update is called once per frame
    void Update()
    {
        playerScore = theGM.heldScore;
        theFinalScreenScore.text = "Final Score: " + playerScore;
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(0);
    }
}
