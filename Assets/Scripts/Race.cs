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
        int numFinished = 0;
        cars.ForEach(delegate (GameObject c)
        {
            Car_Controls controls = c.GetComponent<Car_Controls>();
            if (controls.isDead)
            {
                numFinished++;
            }
        });

        if (numFinished == GameData.NumPlayers())
        {
            SceneManager.LoadScene("Scoreboard");
        }
    }
}
