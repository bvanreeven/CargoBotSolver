using System;
using System.Collections.Generic;
using System.Linq;

namespace CargoBotSolver
{
    public class BruteForceAlgorithm : IAlgorithm
    {
        List<Instruction> toolbox = new List<Instruction>();
        List<int> indices = new List<int>();
        
        public BruteForceAlgorithm(bool includeConditions)
        {
            foreach (InstructionType type in Enum.GetValues(typeof(InstructionType)))
            {                
                toolbox.Add(new Instruction(type, null));
                if (includeConditions)
                {
                    foreach (Condition condition in  Enum.GetValues(typeof(Condition)))
                        toolbox.Add(new Instruction(type, condition));
                }
            }
        }
        
        public Solution GetNextSolution()
        {
            Increment();
            
            var solution = new Solution();
            
            foreach (var index in indices)
            {
                var instr = toolbox[index];
                solution.F1.AddInstruction(instr.Type, instr.Condition);
            }
            
            return solution;
        }
        
        void Increment()
        {
            Increment(indices.Count - 1);
            if (indices.All(i => i == toolbox.Count - 1))
            {
                for (int i = 0; i < indices.Count; i++)
                    indices[i] = 0;
                indices.Add(0);
            }
        }
        
        private void Increment(int index)
        {
            if (index < 0)
                return;
            indices[index]++;
            if (indices[index] == toolbox.Count)
            {
                indices[index] = 0;
                Increment(index - 1);
            }
        }
    }
}
