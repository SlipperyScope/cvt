using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCrashes : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("track"))
        {
            Car_Controls controls = GetComponent<Car_Controls>();
            controls.health -= 100;
        } 
    }
}
