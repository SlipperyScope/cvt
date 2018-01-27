using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBuilder : MonoBehaviour {

	public uint playerCount = 4;
	public GameObject PartPicker;
	public GameObject partPickerContainer;
	public GameObject PartPlacementTile;
	public GameObject partPlacementContainer;
	private bool[,] grid;
	public List<PartPlacement> parts = new List<PartPlacement>();

	// Use this for initialization
	void Start () {
		// Create a part for each player + 2 for some options
		var startPos = new Vector3(200, 100, 0);
		var height = this.GetComponent<RectTransform>().rect.height;
		grid = new bool[playerCount + 2, playerCount + 2];

		for (var i = 0; i < playerCount + 2; i++) {
			var partPicker = Instantiate(PartPicker);
			var partPickerSize = partPicker.GetComponent<RectTransform>().rect.height + 5;
			partPicker.transform.SetParent(this.partPickerContainer.transform, false);
			partPicker.transform.position += new Vector3(
				(i % 2 * -partPickerSize) + partPickerSize*0.75f,
				-partPickerSize*(i / 2),
				0
			);
		}

		// Create a grid (scaled by the number of players)
		// 4 x 4
		// 5 x 5
		// 6 x 6
		float scaleFactor = playerCount * 0.05f + 0.2f;
		float size = 0;
		for (var i = 0; i < playerCount + 2; i++) {
			for (var j = 0; j < playerCount + 2; j++) {
				var tile = Instantiate(PartPlacementTile);
				tile.transform.SetParent(this.partPlacementContainer.transform, false);
				tile.transform.localScale -= new Vector3(scaleFactor, scaleFactor, 0);

				var rect = tile.GetComponent<RectTransform>().rect;
				size = rect.height * (1 - scaleFactor/2) + 15;
				tile.transform.position += new Vector3(
					size * i,
					size * j,
					0
				);
			}
		}

		// Position the entire grid so it is roughly centered in the screen aside from the tray
		// Due to relative resizing of elements, it isn't quite centered. Oh well.
		var sizeX = size * (playerCount / 2 + 0.5f);
		var containerRect = this.partPlacementContainer.GetComponent<RectTransform>().rect;
		this.partPlacementContainer.transform.position -= new Vector3(sizeX, sizeX, 0);
	}

	// Update is called once per frame
	void Update () {

	}

	bool PlacementIsValid(CarPart part, uint x, uint y) {
		// Part needs to fit in the grid
		if (y + part.height >= grid.Length || y + part.height < 0) {
			return false;
		}
		// Part need to fit in the grid
		if (x + part.width >= grid.Length || x + part.height < 0) {
			return false;
		}

		// Part can't be placed on top of an existing part
		for (var yCell = y; yCell < y + part.height; yCell++) {
			for (var xCell = 0; xCell < part.width; xCell++) {
				if (grid[xCell, yCell]) return false;
			}
		}

		return true;
	}

	void PlacePart(CarPart part, uint x, uint y) {
		if (PlacementIsValid(part, x, y)) {
			// Add a part with coordinates to the list of parts
			parts.Add(new PartPlacement(part, x, y));

			// Update the grid so parts can't be placed on top of each other
			for (var yCell = 0; yCell < part.height; yCell++) {
				for (var xCell = 0; xCell < part.width; xCell++) {
					grid[xCell + x, yCell + y] = true;
				}
			}
		}
	}
}
