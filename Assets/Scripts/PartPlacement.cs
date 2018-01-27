using System.Collections;
using System.Collections.Generic;

public class PartPlacement {
  public uint x = 0;
  public uint y = 0;
  public CarPart part;

  public PartPlacement(CarPart part, uint x, uint y) {
    this.part = part;
    this.x = x;
    this.y = y;
  }
}