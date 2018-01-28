using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarBuilder : MonoBehaviour {

	public uint playerCount = 2;
	public PartPicker PartPicker;
	public GameObject partPickerContainer;
	public PartPicker wrench;

	public PartPlacementTile PartPlacementTile;
	public GameObject partPlacementContainer;
	private bool[,] grid;
	private PartPlacementTile[,] tileGrid;
	public List<PartPlacement> parts = new List<PartPlacement>();

	public GameObject PartSheet;
	public HandCursor[] cursors;

	// Use this for initialization
	void Start () {
		// Create a part for each player + 2 for some options
		var pickers = new List<PartPicker>();
		var height = this.GetComponent<RectTransform>().rect.height;
		grid = new bool[playerCount + 2, playerCount + 2];
		tileGrid = new PartPlacementTile[playerCount + 2, playerCount + 2];

		for (var i = 0; i < playerCount + 2; i++) {
			var partPicker = Instantiate(PartPicker);
			var partPickerSize = partPicker.GetComponent<RectTransform>().rect.height + 5;
			partPicker.transform.SetParent(this.partPickerContainer.transform, false);
			partPicker.transform.position += new Vector3(
				(i % 2 * -partPickerSize) + partPickerSize*0.75f,
				-partPickerSize*(i / 2),
				0
			);
			pickers.Add(partPicker);
		}

		// Add the wrench as well, which is not in the part picker list
		pickers.Add(wrench);

		// Create a grid (scaled by the number of players)
		// 4 x 4
		// 5 x 5
		// 6 x 6
		float size = 0;
		var tiles = new List<PartPlacementTile>();
		for (var i = 0; i < playerCount + 2; i++) {
			for (var j = 0; j < playerCount + 2; j++) {
				var tile = Instantiate(PartPlacementTile);
				tile.x = (uint)i;
				tile.y = (uint)(playerCount + 1 - j);
				tile.transform.SetParent(this.partPlacementContainer.transform, false);

				var rect = tile.GetComponent<RectTransform>().rect;
				size = rect.height;
				tile.transform.position += new Vector3(
					size * i,
					size * j,
					0
				);
				tileGrid[i, playerCount + 1 - j] = tile;
				tiles.Add(tile);
			}
		}

		foreach (var cursor in cursors)
		{
			cursor.partOptions = pickers.ToArray();
			cursor.tileOptions = tiles.ToArray();
		}

		// Position the entire grid so it is roughly centered in the screen aside from the tray
		// Due to relative resizing of elements, it isn't quite centered. Oh well.
		var sizeX = size * (playerCount / 2 + 0.5f);
		var containerRect = this.partPlacementContainer.GetComponent<RectTransform>().rect;
		this.partPlacementContainer.transform.position -= new Vector3(sizeX, sizeX, 0);

		// Populate the sprite map
	}

	// Update is called once per frame
	void Update () {
		foreach (var tile in tileGrid) {
			tile.GetComponent<Image>().color = Color.white;
		}

		var allDone = true;
		foreach (var cursor in cursors) {
			if (!cursor.finished) {
				allDone = false;
				break;
			}
		}
		if (allDone) {
			Debug.Log("Finished!");
		}
	}

	bool PlacementIsValid(CarPart part, uint x, uint y) {
		// Part needs to fit in the grid
		bool isWrench = part.partName == "Wrench";

		if (y + part.height > grid.GetLength(1) || y + part.height < 0) {
			return false;
		}
		// Part need to fit in the grid
		if (x + part.width > grid.GetLength(0) || x + part.width < 0) {
			return false;
		}

		// Part can't be placed on top of an existing part
		for (var yCell = y; yCell < y + part.height; yCell++) {
			for (var xCell = x; xCell < x + part.width; xCell++) {
				if (grid[xCell, yCell]) return isWrench;
			}
		}

		return true;
	}

	public void IntentToPlacePart(CarPart part, uint x, uint y) {
		var isValid = PlacementIsValid(part, x, y);
		for (var yCell = y; yCell < y + part.height; yCell++) {
			for (var xCell = x; xCell < x + part.width; xCell++) {
				if (xCell < tileGrid.GetLength(0) && yCell < tileGrid.GetLength(1)) {
					tileGrid[xCell,yCell].GetComponent<Image>().color = isValid ? Color.yellow : Color.red;
				}
			}
		}
	}

	public bool PlacePart(CarPart part, uint x, uint y) {
		if (PlacementIsValid(part, x, y)) {
			if (part.partName != "Wrench") {
				// Add a part with coordinates to the list of parts
				var placement = new PartPlacement(part, x, y);
				parts.Add(placement);

				var sprite = Instantiate(part.sprite);
				sprite.transform.SetParent(this.partPlacementContainer.transform, false);
				placement.sprite = sprite;

				var rect = tileGrid[0,0].GetComponent<RectTransform>().rect;
				var size = rect.height;

				// Correct for position
				sprite.transform.position += new Vector3(
					size * x,
					size * (playerCount + 1 - y),
					0
				);

				// Correct for tile size
				sprite.transform.position -= new Vector3(
					(part.width - 1) * (size / 2) * -1,
					(part.height - 1) * (size / 2),
					0
				);

				// Update the grid so parts can't be placed on top of each other
				for (var yCell = 0; yCell < part.height; yCell++) {
					for (var xCell = 0; xCell < part.width; xCell++) {
						grid[xCell + x, yCell + y] = true;
					}
				}
			} else {
				// Remove the part at the coordinates
				var partToRemove = GetPartAt((int)x, (int)y);
				if (partToRemove != null) {
					parts.Remove(partToRemove);
					Destroy(partToRemove.sprite);

					// Update the grid so parts can be placed here again
					for (var yCell = 0; yCell < partToRemove.part.height; yCell++) {
						for (var xCell = 0; xCell < partToRemove.part.width; xCell++) {
							grid[xCell + partToRemove.x, yCell + partToRemove.y] = false;
						}
					}
				}
			}

			ResolveGrid();
			return true;
		}

		return false;
	}

	public CarSpecs ResolveGrid() {
		List<PartPlacement> markedParts = new List<PartPlacement>();
		var specs = new CarSpecs();

		// Sweep list for combinations, marking all parts used in them
		foreach (var part in parts) {
			var neighbors = GetPartNeighbors(part);
			foreach(var neighbor in neighbors) {
				if (!markedParts.Contains(neighbor)) {
					// Check if neighbor + part is a synergy
					// TODO: Make synergies
					// Debug.Log("Part: " + part.part.partName + " X: " + neighbor.x + " Y: " + neighbor.y + " Neighbor: " + neighbor.part.partName);
				}
			}
		}

		// Sweep the remainder for counts
		foreach (var part in parts) {
			markedParts.Add(part);
			var name = part.part.partName;
			switch (name) {
				case "Spring":
					specs.springs++;
				break;
				case "Nitrous":
					specs.nitrous++;
				break;
				case "Lead":
					specs.weights++;
				break;
				case "MagnetPos":
					specs.magnetsPositive++;
				break;
				case "MagnetNeg":
					specs.magnetsNegative++;
				break;
				case "Inverter":
					specs.inverters++;
				break;
				case "PulseCube":
					specs.pulseCubes++;
				break;
				case "Horn":
					specs.horns++;
				break;
				case "SlickTires":
					specs.slickTires++;
				break;
				case "TreadedTires":
					specs.treadedTires++;
				break;
				case "Spikes":
					specs.spikes++;
				break;
				case "HydrogenCell":
					specs.hydrogenCells++;
				break;
				case "CombustionBlock":
					specs.combustionBlocks++;
				break;
				case "Heart":
					specs.hearts++;
				break;
				case "TrailerHitch":
					specs.trailerHitches++;
				break;
			}
		}

		// Compute drift
		var allWeights = parts.FindAll(p => p.part.partName == "Lead");
		float drift = 0;
		float center = ((float)grid.GetLength(0) - 1) / 2;

		// Add/Subtract based on distance from center, e.g., [ -2, -1, 0, 1, 2 ] [ -3, -2, -1, 1, 2, 3 ]
		foreach (var weight in allWeights) {
			float offset = (int)weight.x - center;
			if (offset < 0) offset = Mathf.Floor(offset);
			if (offset > 0) offset = Mathf.Ceil(offset);
			drift += offset;
		}

		specs.driftCoefficient = drift;

		return specs;
	}

	private PartPlacement GetPartAt(int x, int y) {
		return parts.Find(n => x >= n.x && x < n.x + n.part.width && y >= n.y && y < n.y + n.part.height);
	}

	private HashSet<PartPlacement> GetPartNeighbors(PartPlacement part) {
		var neighbors = new HashSet<PartPlacement>();

		// Throw this trash away in the morning. Instead do this:
		// 1. Collect all neighbors (tiles bordering part, respecting width and height; 8, 10, or 12)
		// 2. Get the part in that tile, if there is one
		// 3. If there's a part there, add it to the hashmap

		var neighborCoordinates = new List<uint[]>();
		for (int y = (int)part.y - 1; y <= part.y + part.part.height; y++) {
			for (int x = (int)part.x - 1; x <= part.x + part.part.width; x++) {
				if (x >= 0 && x < grid.GetLength(0) && y >= 0 && y < grid.GetLength(1)) {
					var neighbor = GetPartAt(x, y);
					if (neighbor != part && neighbor != null) {
						neighbors.Add(neighbor);
					}
				}
			}
		}

		return neighbors;
	}
}
