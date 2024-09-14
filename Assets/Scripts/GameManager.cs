using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public GameObject thePlayer;
    private Player thePlayerScript;
    public int heldScore;


    // Start is called before the first frame update
    void Start()
    {
        thePlayer = GameObject.FindGameObjectWithTag("Player");
        thePlayerScript = thePlayer.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
       UpdateScore(); // Calls the function to update score
    }

    public void UpdateScore()
    {
        // Get the player's Y position
        float playerYPosition = thePlayer.transform.position.y;

        // Round the Y position to the nearest integer
        int roundedYPosition = Mathf.RoundToInt(playerYPosition);


        //Check if players position is above held score (to prevent score dropping when player descends)
        if(roundedYPosition > heldScore)
        {
            heldScore = roundedYPosition;
        }


        //Updating the Score Text
        scoreText.text = "Score: " + heldScore;
    }
}
