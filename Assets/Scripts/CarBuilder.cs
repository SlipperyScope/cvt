using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBuilder : MonoBehaviour {

	public uint playerCount = 4;
	public GameObject PartPicker;
	public GameObject partPickerContainer;

	// Use this for initialization
	void Start () {
		// Create a part for each player + 2 for some options
		var startPos = new Vector3(150, 100, 0);
		var height = this.GetComponent<RectTransform>().rect.height;
		for (var i = 0; i < playerCount + 2; i++) {
			var partPicker = Instantiate(PartPicker);
			partPicker.transform.SetParent(this.partPickerContainer.transform, false);
			partPicker.transform.position += new Vector3(0, -115 * i, 0);
		}
	}

	// Update is called once per frame
	void Update () {

	}
}
