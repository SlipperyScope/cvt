using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Controls : MonoBehaviour {

	public int health = 100;
	public float acceleration = 50;
    public float steering;
	public float speedOfCar;
	public float topSpeed;
	public int numberOfInvertors;
	public bool controlsInverted;
	public bool isDead = false;
	public bool hasFinished = false;
	public int numberOfSprings;
	public int numberOfHydrogenCells;
	public int numberOfHearts;
	public int numberOfPulseCubes;
	public bool canUseNitro;
	public int numberOfNitroCharges;
	public bool slickTiresExist;
	public bool offRoadTiresExist;
	public bool combustionBlockIsInstalled;
	public bool hornIsEquiped;
    public string horizontalName;
    public string verticalName;
	public string boostName;
	public string hornButtonName;

	public AudioClip boopSound;

    private Rigidbody2D rb;
    private CarSpecs spec = GameData.Spec;
    private Vector3 startPos;
    private Quaternion startRot;
    private float weightEffect = 0;

    void Start () {
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
        startRot = transform.rotation;

        if (spec != null)
        {
            numberOfSprings            = (int)spec.springs;
            numberOfNitroCharges       = (int)spec.nitrous;
            numberOfInvertors          = (int)spec.inverters;
            numberOfPulseCubes         = (int)spec.pulseCubes;
            hornIsEquiped              = spec.horns > 0;
            slickTiresExist            = spec.slickTires > 0;
            offRoadTiresExist          = spec.treadedTires > 0;
            numberOfHearts             = (int)spec.hearts;
            numberOfHydrogenCells      = (int)spec.hydrogenCells;
            combustionBlockIsInstalled = spec.combustionBlocks > 0;
            weightEffect               = Mathf.Abs(spec.driftCoefficient) <= 9 ? spec.driftCoefficient / -10 : -0.9f;
        }
		if(numberOfSprings > 0){
			topSpeed += numberOfSprings * 10;
		}
		
		if(numberOfHydrogenCells > 0 ){
			acceleration += numberOfHydrogenCells * 10;
		}

		if( numberOfNitroCharges > 0 ){
			canUseNitro = true;
		}
    }

	private void applyBoost(){
		acceleration += 100 * numberOfNitroCharges;
		canUseNitro = false;
	}
	private void resetBoost(){
		acceleration = 50;
	}

	private bool checkIfOddNumbersOfItemsExist(int numberOfItems){
		if(numberOfItems % 2 == 1){
			return true;
		}else return false;
	}

    void FixedUpdate () {
        if (!isDeadOrFinished())
        {
            float h = -Input.GetAxis(horizontalName);
            float v = Input.GetAxis(verticalName);
            float boostKeyIsPressed = Input.GetAxis(boostName);
            float hornKeyIsPressed = Input.GetAxis(hornButtonName);

		    if(hornKeyIsPressed != 0 & !GetComponent<AudioSource>().isPlaying & hornIsEquiped){
			    GetComponent<AudioSource>().Play();
		    }

            if (boostKeyIsPressed != 0 & canUseNitro)
            {
                applyBoost();
                Invoke("resetBoost", 2);
            }

            if (slickTiresExist & offRoadTiresExist)
            {
                rb.drag = 3;
                rb.angularDrag = 2;
            }
            else if (slickTiresExist & !offRoadTiresExist)
            {
                rb.drag = 2;//normal 3
                rb.angularDrag = 1;
            }
            else if (!slickTiresExist & offRoadTiresExist)
            {
                rb.drag = 4;
                rb.angularDrag = 3;
            }
            else if (!slickTiresExist & !offRoadTiresExist)
            {
                rb.drag = 3;
                rb.angularDrag = 2;
            }

            if (combustionBlockIsInstalled)
            {
                //Detect if we contact fire, if so EXPLODE
            }

            if (checkIfOddNumbersOfItemsExist(numberOfInvertors))
            {
                h = h * -1;
                v = v * -1;
            }

            if (health <= 0 & numberOfHearts > 0)
            {
                speedOfCar = 0;
                transform.rotation = startRot;
                transform.position = startPos;
                health = 100;
                numberOfHearts--;
            }
            else if (health <= 0 & numberOfHearts <= 0)
            {
                isDead = true;
                GameData.numDead++;
            }

            if (numberOfPulseCubes > 0)
            {
                //code goes here.
            }


            //Car Control Code Starts Here.

            Vector2 speed = transform.up * (v * acceleration);

            speedOfCar = rb.velocity.magnitude;
            if (speedOfCar < topSpeed)
            {
                rb.AddForce(speed);
            }
            float direction = Vector2.Dot(rb.velocity, rb.GetRelativeVector(Vector2.up));
            if (direction > 0.0f)
            {
                rb.rotation += (weightEffect + h) * steering * (rb.velocity.magnitude / 5.0f);
                //rb.AddTorque((h * steering) * (rb.velocity.magnitude / 10.0f));
            }
            else if (direction < 0.0f)
            {
                rb.rotation -=  (-weightEffect + h) * steering * (rb.velocity.magnitude / 5.0f);
                //rb.AddTorque((-h * steering) * (rb.velocity.magnitude / 10.0f));
            }

            Vector2 forward = new Vector2(0.0f, 0.5f);
            float steeringRightAngle;
            if (rb.angularVelocity > 0)
            {
                steeringRightAngle = -90;
            }
            else
            {
                steeringRightAngle = 90;
            }

            Vector2 rightAngleFromForward = Quaternion.AngleAxis(steeringRightAngle, Vector3.forward) * forward;

            float driftForce = Vector2.Dot(rb.velocity, rb.GetRelativeVector(rightAngleFromForward.normalized));

            Vector2 relativeForce = (rightAngleFromForward.normalized * -1.0f) * (driftForce * 10.0f);

            rb.AddForce(rb.GetRelativeVector(relativeForce));
        }
    }

    public bool isDeadOrFinished()
    {
        return isDead || hasFinished;
    }
}