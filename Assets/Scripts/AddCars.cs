using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCars : MonoBehaviour {
    private int numPlayers = 1;
    
	// Use this for initialization
	void Start () {
        numPlayers = GameData.NumPlayers();
        Rect[] cameraRects = new Rect[numPlayers];
        if (numPlayers == 1)
        {
            cameraRects[0] = new Rect(0, 0, 1, 1);
        }
        else if (numPlayers == 2)
        {
            cameraRects[0] = new Rect(0, 0, .5f, 1);
            cameraRects[1] = new Rect(.5f, 0, .5f, 1);
        }
        else if (numPlayers == 3)
        {
            cameraRects[0] = new Rect(0, .5f, .5f, .5f);
            cameraRects[1] = new Rect(.5f, .5f, .5f, .5f);
            cameraRects[2] = new Rect(0, 0, .5f, .5f);

        }
        else
        {
            cameraRects[0] = new Rect(0, .5f, .5f, .5f);
            cameraRects[1] = new Rect(.5f, .5f, .5f, .5f);
            cameraRects[2] = new Rect(0, 0, .5f, .5f);
            cameraRects[3] = new Rect(.5f, 0, .5f, .5f);

        }

        int rectCount = 0;
        if (!GameData.hasPlayer1)
        {
            Destroy(GameObject.Find("FullCar1"));
        }
        else
        {
            Camera cam = GameObject.Find("CameraP1").GetComponent<Camera>();
            cam.rect = cameraRects[rectCount++];
        }

        if (!GameData.hasPlayer2)
        {
            Destroy(GameObject.Find("FullCar2"));
        }
        else
        {
            Camera cam = GameObject.Find("CameraP2").GetComponent<Camera>();
            cam.rect = cameraRects[rectCount++];
        }
        if (!GameData.hasPlayer3)
        {
            Destroy(GameObject.Find("FullCar3"));
        }
        else
        {
            Camera cam = GameObject.Find("CameraP3").GetComponent<Camera>();
            cam.rect = cameraRects[rectCount++];
        }
        if (!GameData.hasPlayer4)
        {
            Destroy(GameObject.Find("FullCar4"));
        }
        else
        {
            Camera cam = GameObject.Find("CameraP4").GetComponent<Camera>();
            cam.rect = cameraRects[rectCount++];
        }
    }

}
