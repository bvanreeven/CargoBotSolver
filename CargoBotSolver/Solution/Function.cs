using System.Collections.Generic;

namespace CargoBotSolver
{
    public class Function
    {
        private readonly List<Instruction> instructions = new List<Instruction>();

        public Function()
        {
        }
        
        public void AddInstruction(InstructionType instructionType, Condition? condition = null)
        {
            instructions.Add(new Instruction(instructionType, condition));
        }
        
        public Instruction InstructionAt(int index)
        {
            return index >= instructions.Count ? null : instructions[index];
        }
        
        public override string ToString ()
        {
            return string.Join(", ", instructions);
        }
    }
}