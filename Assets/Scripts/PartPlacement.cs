using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartPlacement {
  public uint x = 0;
  public uint y = 0;
  public CarPart part;
  public GameObject sprite;

  public PartPlacement(CarPart part, uint x, uint y) {
    this.part = part;
    this.x = x;
    this.y = y;
  }
}