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

        if (GameData.hasPlayer1)
        {
            playerScore1.text = "" + GameData.playerScore1;
        }
        else
        {
            playerText1.enabled = false;
            playerScore1.enabled = false;
            GameObject.FindGameObjectWithTag("square1").SetActive(false);
        }
        if (GameData.hasPlayer2)
        {
            playerScore2.text = "" + GameData.playerScore2;
        }
        else
        {
            playerText2.enabled = false;
            playerScore2.enabled = false;
            GameObject.FindGameObjectWithTag("square2").SetActive(false);
        }
        if (GameData.hasPlayer3)
        {
            playerScore3.text = "" + GameData.playerScore3;
        }
        else
        {
            playerText3.enabled = false;
            playerScore3.enabled = false;
            GameObject.FindGameObjectWithTag("square3").SetActive(false);
        }
        if (GameData.hasPlayer4)
        {
            playerScore4.text = "" + GameData.playerScore4;
        }
        else
        {
            playerText4.enabled = false;
            playerScore4.enabled = false;
            GameObject.FindGameObjectWithTag("square4").SetActive(false);
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