using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour {

    private bool hasPlayer1 = false;
    private bool hasPlayer2 = false;
    private bool hasPlayer3 = false;
    private bool hasPlayer4 = false;
    private bool axis1Free = true;
    private bool axis2Free = true;
    private bool axis3Free = true;
    private bool axis4Free = true;
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
        if (axis1Free && Input.GetAxis("HorizontalP1") < 0)
        {
            axis1Free = false;
            hasPlayer1 = !hasPlayer1;
            square1.SetActive(hasPlayer1);
        }
        else if (!axis1Free && Input.GetAxis("HorizontalP1") >= 0)
        {
            axis1Free = true;
        }

        if (axis2Free && Input.GetAxis("HorizontalP2") < 0)
        {
            axis2Free = false;
            hasPlayer2 = !hasPlayer2;
            square2.SetActive(hasPlayer2);
        }
        else if (!axis2Free && Input.GetAxis("HorizontalP2") >= 0)
        {
            axis2Free = true;
        }

        if (axis3Free && Input.GetAxis("HorizontalP3") < 0)
        {
            axis3Free = false;
            hasPlayer3 = !hasPlayer3;
            square3.SetActive(hasPlayer3);
        }
        else if (!axis3Free && Input.GetAxis("HorizontalP3") >= 0)
        {
            axis3Free = true;
        }

        if (axis4Free && Input.GetAxis("HorizontalP4") < 0)
        {
            axis4Free = false;
            hasPlayer4 = !hasPlayer4;
            square4.SetActive(hasPlayer4);
        }
        else if (!axis4Free && Input.GetAxis("HorizontalP4") >= 0)
        {
            axis4Free = true;
        }

        if ((hasPlayer1 && Input.GetAxisRaw("ForwardP1") == 1)
                || (hasPlayer2 && Input.GetAxisRaw("ForwardP2") == 1)
                || (hasPlayer3 && Input.GetAxisRaw("ForwardP3") == 1)
                || (hasPlayer4 && Input.GetAxisRaw("ForwardP4") == 1))
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
