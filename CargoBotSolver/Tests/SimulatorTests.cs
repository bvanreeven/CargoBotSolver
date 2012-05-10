using System;
using System.IO;
using NUnit.Framework;

namespace CargoBotSolver
{
    [TestFixture]
    public class SimulatorTests
    {
        [Test]
        public void CrashLeft()
        {
            var puzzle = new Puzzle(File.ReadAllText("../../Puzzles/Cargo101.json"));
            var solution = new Solution();
            solution.F1.AddInstruction(InstructionType.Left);
            var simulator = new Simulator(puzzle, solution);
            Assert.AreEqual(SimulationResult.CrashedIntoLeftWall, simulator.Run());
        }
        
        [Test]
        public void CrashRight()
        {
            var puzzle = new Puzzle(File.ReadAllText("../../Puzzles/Cargo101.json"));
            var solution = new Solution();
            solution.F1.AddInstruction(InstructionType.Right);
            solution.F1.AddInstruction(InstructionType.Right);
            var simulator = new Simulator(puzzle, solution);
            Assert.AreEqual(SimulationResult.CrashedIntoRightWall, simulator.Run());
        }
        
        [Test]
        public void SolveCargo101()
        {
            var puzzle = new Puzzle(File.ReadAllText("../../Puzzles/Cargo101.json"));
            var solution = new Solution();
            solution.F1.AddInstruction(InstructionType.Down);
            solution.F1.AddInstruction(InstructionType.Right);
            solution.F1.AddInstruction(InstructionType.Down);
            var simulator = new Simulator(puzzle, solution);
            Assert.AreEqual(SimulationResult.Solved, simulator.Run());
        }
        
        [Test]
        public void EmptySolution()
        {
            var puzzle = new Puzzle(File.ReadAllText("../../Puzzles/Cargo101.json"));
            var solution = new Solution();
            var simulator = new Simulator(puzzle, solution);
            Assert.AreEqual(SimulationResult.Finished, simulator.Run());
        }
        
        [Test]
        public void Finished()
        {
            var puzzle = new Puzzle(File.ReadAllText("../../Puzzles/Cargo101.json"));
            var solution = new Solution();
            solution.F1.AddInstruction(InstructionType.Down);
            var simulator = new Simulator(puzzle, solution);
            Assert.AreEqual(SimulationResult.Finished, simulator.Run());
        }
        
        [Test]
        public void CrashIntoBoxes()
        {
            var puzzle = new Puzzle(File.ReadAllText("../../Puzzles/WalkingPiles.json"));
            var solution = new Solution();
            solution.F1.AddInstruction(InstructionType.Down);
            solution.F1.AddInstruction(InstructionType.Right);
            solution.F1.AddInstruction(InstructionType.Down);
            solution.F1.AddInstruction(InstructionType.Left);
            solution.F1.AddInstruction(InstructionType.F1);
            var simulator = new Simulator(puzzle, solution);
            Assert.AreEqual(SimulationResult.CrashedIntoBoxes, simulator.Run());
        }
        
        [Test]
        public void CallFunction()
        {
            var puzzle = new Puzzle(File.ReadAllText("../../Puzzles/Cargo101.json"));
            var solution = new Solution();
            solution.F1.AddInstruction(InstructionType.F2);
            solution.F1.AddInstruction(InstructionType.Right);
            solution.F1.AddInstruction(InstructionType.F2);
            solution.F2.AddInstruction(InstructionType.Down);
            var simulator = new Simulator(puzzle, solution);
            Assert.AreEqual(SimulationResult.Solved, simulator.Run());
        }
        
        [Test]
        public void DetectInfiniteLoop()
        {
            var puzzle = new Puzzle(File.ReadAllText("../../Puzzles/Cargo101.json"));
            var solution = new Solution();
            solution.F1.AddInstruction(InstructionType.Down);
            solution.F1.AddInstruction(InstructionType.F1);
            var simulator = new Simulator(puzzle, solution);
            Assert.AreEqual(SimulationResult.InfiniteLoop, simulator.Run());
        }
        
        [Test]
        public void DetectInfiniteLoop2()
        {
            var puzzle = new Puzzle(File.ReadAllText("../../Puzzles/Cargo101.json"));
            var solution = new Solution();
            solution.F1.AddInstruction(InstructionType.Right);
            solution.F1.AddInstruction(InstructionType.F2);
            solution.F1.AddInstruction(InstructionType.Left);
            solution.F2.AddInstruction(InstructionType.Left);
            solution.F2.AddInstruction(InstructionType.F1);
            solution.F2.AddInstruction(InstructionType.Right);
            var simulator = new Simulator(puzzle, solution);
            Assert.AreEqual(SimulationResult.InfiniteLoop, simulator.Run());
        }
    }
}
