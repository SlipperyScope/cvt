using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandCursor : MonoBehaviour {

	private Color _color = Color.white;
	public Color color {
		get {
			return _color;
		}
		set {
			_color = value;
			closedHandSprite.GetComponent<Image>().color = value;
			openHandSprite.GetComponent<Image>().color = value;
		}
	}
	public bool isOpen = true;
	public CarPart part;
	private GameObject partSprite;
	public CarBuilder carBuilder;

	public GameObject openHandSprite;
	public GameObject closedHandSprite;
	public PartPicker[] partOptions = new PartPicker[0];
	public PartPlacementTile[] tileOptions = new PartPlacementTile[0];
	private bool isDone = false;
	public bool finished {
		get {
			return isDone;
		}
	}

	public string inputX;
	public string inputY;
	public string inputAction;

	private bool grabbing = false;

	public HandCursor(Color color, uint playerNumber) {
		this.color = color;
		this.inputX = "MenuHP" + playerNumber;
		this.inputY = "MenuVP" + playerNumber;
		this.inputAction = "MenuActionP" + playerNumber;
	}

	// Use this for initialization
	void Start () {
		closedHandSprite.SetActive(false);
		closedHandSprite.GetComponent<Image>().color = color;
		openHandSprite.GetComponent<Image>().color = color;
		RandomizeColor();
	}

	void RandomizeColor() {
		color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1);
	}

	// Update is called once per frame
	void Update () {
		if (!isDone) {
			if (Input.GetAxis(inputAction) > 0 && !grabbing && !part) {
				grabbing = true;
				Grab();
			}

			if (Input.GetAxis(inputAction) == 0 && grabbing) {
				grabbing = false;
			}

			MoveCursor();
		}
	}

	void LateUpdate() {
		if (!isDone) {
			if (part) {
				Placement(Input.GetAxis(inputAction) > 0);
			}

			if (!part) {
				CheckPartOptions();
			}
		}
	}

	void Placement(bool shouldPlace = false) {
		// Top-left-most colliding tile is the origin tile used to validate placement
		PartPlacementTile origin = null;
		var worldRect = partSprite.GetComponent<RectTransform>().rect;
		foreach (var item in tileOptions) {
			var localRect = new Rect(
				worldRect.position + (Vector2)this.transform.position - (Vector2)item.transform.position,
				worldRect.size
			);
			if (item.GetComponent<RectTransform>().rect.Overlaps(localRect)) {
				if (!origin) {
					origin = item;
				} else {
					var curPos = origin.transform.position;
					var newPos = item.transform.position;
					if (newPos.y > curPos.y || (newPos.y == curPos.y && newPos.x < curPos.x)) {
						origin = item;
					}
				}
			}
		}

		if (origin) {
			// Validate placement
			carBuilder.IntentToPlacePart(part, origin.x, origin.y);
			if (shouldPlace) {
				var success = carBuilder.PlacePart(part, origin.x, origin.y);
				if (success) {
					// Add part to grid
					part = null;
					// isDone = true;
					closedHandSprite.SetActive(false);
					openHandSprite.SetActive(false);
					Destroy(partSprite);
				}
			}
		}
	}

	void CheckPartOptions() {
		foreach (var picker in partOptions) {
			picker.checkCollision((Vector2)this.transform.position);
		}
	}

	void MoveCursor() {
		float speed = 15;
		this.transform.position += new Vector3(Input.GetAxis(inputX) * speed, Input.GetAxis(inputY) * speed, 0);
	}

	void Grab() {
		CloseHand();

		// Check if successfully grabbed something
		foreach (var picker in partOptions)
		{
			picker.checkCollision((Vector2)this.transform.position);
			var part = picker.PickUp((Vector2)this.transform.position);
			if (part) {
				this.part = part;
				partSprite = Instantiate(part.sprite);
				partSprite.transform.SetParent(this.transform, false);
				partSprite.transform.SetAsFirstSibling();
				return;
			}
		}

		// Otherwise
		Invoke("OpenHand", 0.25f);
	}

	void OpenHand() {
		isOpen = true;
		closedHandSprite.SetActive(false);
		openHandSprite.SetActive(true);
	}

	void CloseHand() {
		isOpen = false;

		closedHandSprite.SetActive(true);
		openHandSprite.SetActive(false);
	}
}