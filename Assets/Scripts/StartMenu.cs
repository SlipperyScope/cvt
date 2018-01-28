using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour {

    private bool hasPlayer1 = false;
    private bool hasPlayer2 = false;
    private bool hasPlayer3 = false;
    private bool hasPlayer4 = false;
    private GameObject square1;
    private GameObject square2;
    private GameObject square3;
    private GameObject square4;

	// Use this for initialization
	void Start ()
    {
        square1 = GameObject.FindGameObjectWithTag("square1");
        square2 = GameObject.FindGameObjectWithTag("square2");
        square3 = GameObject.FindGameObjectWithTag("square3");
        square4 = GameObject.FindGameObjectWithTag("square4");
        square1.SetActive(false);
        square2.SetActive(false);
        square3.SetActive(false);
        square4.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("NitroP1"))
        {
            hasPlayer1 = !hasPlayer1;
            square1.SetActive(hasPlayer1);
        }

        if (Input.GetButtonDown("NitroP2"))
        {
            hasPlayer2 = !hasPlayer2;
            square2.SetActive(hasPlayer2);
        }

        if (Input.GetButtonDown("NitroP3"))
        {
            hasPlayer3 = !hasPlayer3;
            square3.SetActive(hasPlayer3);
        }

        if (Input.GetButtonDown("NitroP4"))
        {
            hasPlayer4 = !hasPlayer4;
            square4.SetActive(hasPlayer4);
        }

        if ((hasPlayer1 && Input.GetButtonDown("HornP1"))
                || (hasPlayer2 && Input.GetButtonDown("HornP2"))
                || (hasPlayer3 && Input.GetButtonDown("HornP3"))
                || (hasPlayer4 && Input.GetButtonDown("HornP4")))
        {
            setNumPlayers();
            SceneManager.LoadScene("Racetrack");
        }
	}

    public void setNumPlayers()
    {
        GameData.hasPlayer1 = hasPlayer1;
        GameData.hasPlayer2 = hasPlayer2;
        GameData.hasPlayer3 = hasPlayer3;
        GameData.hasPlayer4 = hasPlayer4;
    }
}
