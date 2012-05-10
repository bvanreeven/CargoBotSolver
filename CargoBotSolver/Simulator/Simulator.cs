using System;
using System.Collections.Generic;
using System.Linq;

namespace CargoBotSolver
{
    public class Simulator
    {
        const int MAX_STACK_SIZE = 10000;
        readonly Puzzle puzzle;
        readonly Solution solution;
        Stage currentStage;
        readonly Stack<ProgramCounter> stack = new Stack<ProgramCounter>();
        //readonly HashSet<Snapshot> snapshots = new HashSet<Snapshot>();

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
            
            //snapshots.Clear();
            //snapshots.Add(new Snapshot(currentStage, stack));
   
            try
            {
                while (stack.Count > 0)
                {
                    Step();
                    // XXX: Stack simplification.
                    
                    if (puzzle.GoalStage.StacksEqual(currentStage))
                        return SimulationResult.Solved;
                    
                    //if (!snapshots.Add(new Snapshot(currentStage, stack)))
                    //    return SimulationResult.InfiniteLoop;
                    
                    if (stack.Count > MAX_STACK_SIZE)
                        return SimulationResult.InfiniteLoop;
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
            var pc = stack.Pop();
            var instruction = solution.Functions[pc.FunctionIndex].InstructionAt(pc.InstructionIndex);
            if (instruction == null)
                return;
            stack.Push(pc.Increment());

            if (currentStage.Matches(instruction.Condition))
            {
                switch (instruction.Type)
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
        }

        private class ProgramCounter
        {
            public ProgramCounter(int functionIndex, int instructionIndex)
            {
                FunctionIndex = functionIndex;
                InstructionIndex = instructionIndex;
            }
    
            public int FunctionIndex { get; private set; }
    
            public int InstructionIndex { get; private set; }
            
            public ProgramCounter Increment()
            {
                return new ProgramCounter(FunctionIndex, InstructionIndex + 1);
            }
            
            public override bool Equals(object obj)
            {
                if (obj == null || obj.GetType() != typeof(ProgramCounter))
                    return false;
                if (obj == this)
                    return true;
                var that = (ProgramCounter)obj;
                return this.FunctionIndex == that.FunctionIndex && this.InstructionIndex == that.InstructionIndex;
            }
            
            public override int GetHashCode()
            {
                return FunctionIndex ^ InstructionIndex;
            }
        }
        
        private class Snapshot
        {
            Stage stage;
            Stack<ProgramCounter> stack;
            
            public Snapshot(Stage stage, Stack<ProgramCounter> stack)
            {
                this.stage = stage;
                this.stack = new Stack<ProgramCounter>(stack);
            }
            
            public override bool Equals(object obj)
            {
                if (obj == null || obj.GetType() != typeof(Snapshot))
                    return false;
                if (obj == this)
                    return true;
                var that = (Snapshot)obj;
                return Equals(this.stage, that.stage) && this.stack.SequenceEqual(that.stack);
            }
            
            public override int GetHashCode ()
            {
                var hashCode = stage.GetHashCode();
                foreach (var pc in stack)
                    hashCode ^= pc.GetHashCode();
                return hashCode;
            }
        }
    }
}
