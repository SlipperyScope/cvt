using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Race : MonoBehaviour {

    private List<GameObject> cars = new List<GameObject>();
	void Start () {
        if (GameData.hasPlayer1)
        {
            cars.Add(GameObject.Find("CarP1"));
        }
        if (GameData.hasPlayer2)
        {
            cars.Add(GameObject.Find("CarP2"));
        }
        if (GameData.hasPlayer3)
        {
            cars.Add(GameObject.Find("CarP3"));
        }
        if (GameData.hasPlayer4)
        {
            cars.Add(GameObject.Find("CarP4"));
        }
	}
	
	void Update () {
        if ((GameData.numFinished + GameData.numDead) == GameData.NumPlayers())
        {
            GameData.numFinished = 0;
            GameData.numDead = 0;
            SceneManager.LoadScene("Scoreboard");
        }
    }
}
