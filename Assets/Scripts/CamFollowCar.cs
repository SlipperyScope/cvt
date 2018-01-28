using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowCar : MonoBehaviour {

    public Transform carLocation;
    private float distanceFromCar = 10.0f;

    private void Start()
    {
        transform.position = carLocation.position + new Vector3(0, 0, -distanceFromCar);
        transform.rotation = carLocation.rotation;
    }

    void LateUpdate () {
        Vector3 toPos = carLocation.position + new Vector3(0, 0, -distanceFromCar);
        transform.position = toPos;

        transform.rotation = Quaternion.Lerp(transform.rotation, carLocation.rotation, Time.deltaTime * 5);
	}
}
