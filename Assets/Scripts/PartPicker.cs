using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartPicker : MonoBehaviour {

	public GameObject[] partTypes;

	// Use this for initialization
	void Start () {
		var type = partTypes[Random.Range(0, partTypes.Length)];
		var icon = Instantiate(type);
		icon.transform.SetParent(this.transform, false);
	}

	// Update is called once per frame
	void Update () {

	}
}
