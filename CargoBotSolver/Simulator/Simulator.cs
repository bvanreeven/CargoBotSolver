using System;
using System.Collections.Generic;

namespace CargoBotSolver
{
    public class Simulator
    {
        readonly Puzzle puzzle;
        readonly Solution solution;
        Stage currentStage;
        readonly Stack<ProgramCounter> stack = new Stack<ProgramCounter>();

        public Simulator(Puzzle puzzle, Solution solution)
        {
            this.puzzle = puzzle;
            this.solution = solution;
            this.currentStage = puzzle.InitialStage;
        }

        public SimulationResult Run()
        {
            stack.Clear();
            stack.Push(new ProgramCounter(0, 0));
   
            try
            {
                while (stack.Count > 0)
                {
                    Step();
                    // XXX: Stack simplification.
                    // XXX: Infinited loop detection.
                    
                    if (puzzle.GoalStage.StacksEqual(currentStage))
                        return SimulationResult.Solved;
                }
                return SimulationResult.Finished;
            }
            catch (SimulationException se)
            {
                return se.Result;
            }
        }

        private void Step()
        {
            ProgramCounter pc = stack.Peek();
            Instruction entry = solution.Functions[pc.FunctionIndex].InstructionAt(pc.InstructionIndex);
            if (entry == null)
            {
                stack.Pop();
                return;
            }

            if (currentStage.Matches(entry.Condition))
            {
                switch (entry.Type)
                {
                    case InstructionType.Right:
                        currentStage = currentStage.Right();
                        break;
                    case InstructionType.Down:
                        currentStage = currentStage.Down();
                        break;
                    case InstructionType.Left:
                        currentStage = currentStage.Left();
                        break;
                    case InstructionType.F1:
                        stack.Push(new ProgramCounter(0, 0));
                        break;
                    case InstructionType.F2:
                        stack.Push(new ProgramCounter(1, 0));
                        break;
                    case InstructionType.F3:
                        stack.Push(new ProgramCounter(2, 0));
                        break;
                    case InstructionType.F4:
                        stack.Push(new ProgramCounter(3, 0));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            pc.InstructionIndex++;
        }

        private class ProgramCounter
        {
            public ProgramCounter(int functionIndex, int entryIndex)
            {
                FunctionIndex = functionIndex;
                InstructionIndex = entryIndex;
            }
    
            public int FunctionIndex { get; set; }
    
            public int InstructionIndex { get; set; }
        }
    }
}
