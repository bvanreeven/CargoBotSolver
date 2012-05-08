using System;

namespace CargoBotSolver
{
    public enum SimulationResult
    {
        Solved,
        Finished,
        CrashedIntoLeftWall,
        CrashedIntoRightWall,
        CrashedIntoBoxes,
        InfiniteLoop
    }
}
