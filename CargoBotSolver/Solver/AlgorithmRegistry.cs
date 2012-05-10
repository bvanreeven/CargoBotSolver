using System;
using System.Collections.Generic;
using System.Linq;

namespace CargoBotSolver
{
    public static class AlgorithmRegistry
    {
        static Dictionary<string, Func<Puzzle, IAlgorithm>> algorithms = new Dictionary<string, Func<Puzzle, IAlgorithm>>();
        
        public static void RegisterAlgorithm(string name, Func<Puzzle, IAlgorithm> createAlgorithm)
        {
            if (algorithms.ContainsKey(name))
                throw new ArgumentException("name already in use");
            
            algorithms.Add(name, createAlgorithm);
        }
        
        public static IList<string> GetAlgorithmNames()
        {
            return algorithms.Keys.OrderBy(n => n).ToList();
        }
        
        public static IAlgorithm CreateAlgorithm(string name, Puzzle puzzle)
        {
            if (!algorithms.ContainsKey(name))
                throw new ArgumentException("unknown algorithm name");
            
            return algorithms[name](puzzle);
        }
    }
}
