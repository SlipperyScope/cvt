using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCrashes : MonoBehaviour {

    private bool tempFinished = false;
    private void OnTriggerStay2D(Collider2D col)
    {

        if (col.gameObject.CompareTag("track"))
        {
            Car_Controls controls = GetComponent<Car_Controls>();
            controls.health -= 100;
        } 
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
       if (col.gameObject.CompareTag("Finish") && !tempFinished)
        {
            Car_Controls controls = GetComponent<Car_Controls>();
            tempFinished = true;
            Debug.Log("WINNER WINNER TURKEY DINNER");
            controls.health = 0; //remove this idioth
            //controls.finished = true;
        } 
    }
}
