using System;

namespace CargoBotSolver
{
    public class Solver
    {
        Puzzle puzzle;
        IAlgorithm algorithm;
        
        public Solver(Puzzle puzzle, IAlgorithm algorithm)
        {
            this.puzzle = puzzle;
            this.algorithm = algorithm;
        }
        
        public void Solve()
        {
            int counter = 1;
            while(true)
            {
                var solution = algorithm.GetNextSolution();
                if (solution == null)
                {
                    Console.WriteLine("No more solutions.");
                    break;
                }
                
                Console.WriteLine(string.Format("Trying solution {0}...", counter++));
                
                var simulator = new Simulator(puzzle, solution);
                var result = simulator.Run();
                Console.WriteLine(result);
                Console.WriteLine();
                
                if (result == SimulationResult.Solved)
                {
                    Console.WriteLine("Solution:");
                    Console.WriteLine(solution.ToString());
                    break;
                }
            }
        }
    }
}
