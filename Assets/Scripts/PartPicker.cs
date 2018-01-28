using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartPicker : MonoBehaviour {

	public CarPart[] partTypes;
	public CarPart type;
	public bool alreadyPicked = false;
	public bool hover = false;
	private GameObject icon;

	// Use this for initialization
	void Start () {
		var type = partTypes[Random.Range(0, partTypes.Length)];
		icon = Instantiate(type.icon);
		icon.transform.SetParent(this.transform, false);

		this.type = type;
	}

	// Update is called once per frame
	void Update () {
		if (this.hover && !this.alreadyPicked) {
			this.transform.GetChild(0).GetComponent<Image>().color = Color.yellow;
		} else if (!this.alreadyPicked) {
			this.transform.GetChild(0).GetComponent<Image>().color = Color.white;
		}
		this.hover = false;
	}

	public CarPart PickUp(Vector2 point) {
		if (checkCollision(point)) {
			alreadyPicked = true;
			icon.GetComponent<Image>().color = new Color(1, 1, 1, 0.25f);
			this.transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 0);

			return type;
		}
		return null;
	}

	public bool checkCollision(Vector2 point) {
		var localPosition = (Vector2)this.transform.position;
		if (this.GetComponent<RectTransform>().rect.Contains(point - localPosition)) {
			this.hover = true;
			return true;
		};
		return false;
	}
}
