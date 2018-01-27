using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowCar : MonoBehaviour {

    public Transform carLocation;
    private float distanceFromCar = 10.0f;
    private float distanceDamp = .15f;
    private Vector3 velocity = Vector3.one;

    private void Start()
    {
        transform.position = carLocation.position + new Vector3(0, 0, -distanceFromCar);
        transform.rotation = carLocation.rotation;
    }

    void LateUpdate () {
        Vector3 toPos = carLocation.position + new Vector3(0, 0, -distanceFromCar);
        transform.position = Vector3.SmoothDamp(transform.position, toPos, ref velocity, distanceDamp);

        transform.rotation = Quaternion.Lerp(transform.rotation, carLocation.rotation, Time.deltaTime * 5);
	}
}
