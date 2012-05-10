namespace CargoBotSolver
{
    public class Instruction
    {
        private readonly Condition? condition;
        private readonly InstructionType type;

        public Instruction(InstructionType type, Condition? condition)
        {
            this.type = type;
            this.condition = condition;
        }

        public InstructionType Type
        {
            get { return type; }
        }

        public Condition? Condition
        {
            get { return condition; }
        }
        
        public override string ToString()
        {
            return string.Format("{0}{1}", Type, Condition == null ? "" : string.Format(" ({0})", Condition));
        }
    }
}