using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCrashes : MonoBehaviour {

    private Car_Controls controls;
    private void Start()
    {
        controls = GetComponent<Car_Controls>();   
    }
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("track"))
        {
            controls.health -= 100;
        } 
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
       if (col.gameObject.CompareTag("Finish") && !controls.isDeadOrFinished())
        {
            Debug.Log("WINNER WINNER TURKEY DINNER");
            controls.hasFinished = true;
            char playerNum = controls.horizontalName[controls.horizontalName.Length - 1];
            switch (playerNum)
            {
                case '1':
                    GameData.playerScore1 += 10 / ++GameData.numFinished;
                    break;
                case '2':
                    GameData.playerScore2 += 10 / ++GameData.numFinished;
                    break;
                case '3':
                    GameData.playerScore3 += 10 / ++GameData.numFinished;
                    break;
                case '4':
                    GameData.playerScore4 += 10 / ++GameData.numFinished;
                    break;
            }

        } 
    }
}
