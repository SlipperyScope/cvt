using System.Collections;
using System.Collections.Generic;

public class CarSpecs {
  // Modifier Counts
  public uint springs = 0;
  public uint nitrous = 0;
  public uint weights = 0;
  public int magnetsPositive = 0;
  public int magnetsNegative = 0;
  public uint inverters = 0;
  public uint pulseCubes = 0;
  public uint horns = 0;
  public uint slickTires = 0;
  public uint treadedTires = 0;
  public uint spikes = 0;
  public uint hydrogenCells = 0;
  public uint combustionBlocks = 0;
  public uint hearts = 0;
  public uint trailerHitches = 0;

  // Composite Counts
  // TBD!!!

  // Computed Effects
  public float driftCoefficient = 0f;
  public int polarity {
    get {
      return this.magnetsPositive - this.magnetsNegative;
    }
  }
}