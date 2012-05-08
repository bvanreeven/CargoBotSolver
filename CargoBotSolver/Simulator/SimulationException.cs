using System;

namespace CargoBotSolver
{
    internal class SimulationException : Exception
    {
        SimulationResult result;
        
        public SimulationException(SimulationResult result, string message)
            : base(message)
        {
            this.result = result;
        }
        
        public SimulationResult Result
        {
            get { return result; }
        }
    }
}