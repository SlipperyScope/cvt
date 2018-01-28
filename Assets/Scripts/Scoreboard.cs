using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scoreboard : MonoBehaviour {
    [SerializeField]
    private Text playerText1;
    [SerializeField]
    private Text playerText2;
    [SerializeField]
    private Text playerText3;
    [SerializeField]
    private Text playerText4;

    [SerializeField]
    private Text playerScore1;
    [SerializeField]
    private Text playerScore2;
    [SerializeField]
    private Text playerScore3;
    [SerializeField]
    private Text playerScore4;

    public Text GameoverText;

    private string[] texts;
    private int[] scores;

    void Start()
    {
        if (GameData.haveWinner())
        {
            GameoverText.enabled = true;
        } else
        {
            GameoverText.enabled = false;
        }
        int numPlayers = GameData.NumPlayers();
        texts  = new string[numPlayers];
        scores = new int[numPlayers];

        int i = 0;
        if (GameData.hasPlayer1)
        {
            texts[i] = "Player 1 Score:";
            scores[i] = GameData.playerScore1;
            i++;
        }
        if (GameData.hasPlayer2)
        {
            texts[i] = "Player 2 Score:";
            scores[i] = GameData.playerScore2;
            i++;
        }
        if (GameData.hasPlayer3)
        {
            texts[i] = "Player 3 Score:";
            scores[i] = GameData.playerScore3;
            i++;
        }
        if (GameData.hasPlayer4)
        {
            texts[i] = "Player 4 Score:";
            scores[i] = GameData.playerScore4;
            i++;
        }

        if (numPlayers >= 1)
        {
            playerText1.text = "" + texts[0];
            playerScore1.text = "" + scores[0];
        }
        
        if (numPlayers >= 2)
        {
            playerText2.text = "" + texts[1];
            playerScore2.text = "" + scores[1];
        }
        else
        {
            playerText2.enabled = false;
            playerText3.enabled = false;
            playerText4.enabled = false;

            playerScore2.enabled = false;
            playerScore3.enabled = false;
            playerScore4.enabled = false;
        }

        if (numPlayers >= 3)
        {
            playerText3.text = "" + texts[2];
            playerScore3.text = "" + scores[2];
        }
        else
        {
            playerText3.enabled = false;
            playerText4.enabled = false;

            playerScore3.enabled = false;
            playerScore4.enabled = false; 
        }

        if (numPlayers == 4)
        {
            playerText4.text = "" + texts[3];
            playerScore4.text = "" + scores[3];
        } else
        {
            playerText4.enabled = false;
            playerScore4.enabled = false; 
        }
    }

    void Update()
    {
        if (Input.GetKey("escape"))
            Application.Quit();

        if (Input.GetButtonUp("NitroP1") || Input.GetButtonUp("NitroP2") || Input.GetButtonUp("NitroP3") || Input.GetButtonUp("NitroP4"))
        {
            changeScene();
        }
    }

    private void changeScene()
    {
        SceneManager.LoadScene(GameData.haveWinner() ? "Menu" : "CarBuilder");
    }
}