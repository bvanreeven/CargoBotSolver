using System;
using System.Collections.Generic;
using System.Linq;

namespace CargoBotSolver
{
    public class Stage
    {
        private const int MAX_STACK_SIZE = 7;
        private readonly int grapplerPosition;
        private readonly Box? boxInGrappler;
        private readonly List<Stack<Box>> stacks = new List<Stack<Box>>();
        
        public static Stage CreateStage(int grapplerPosition, List<string> stackTexts)
        {
            var stacks = new List<Stack<Box>>();
            foreach (var stackText in stackTexts)
            {
                var stack = new Stack<Box>();
                stacks.Add(stack);
                foreach (var c in stackText)   
                {
                    switch(c)
                    {
                        case 'r':
                            stack.Push(Box.Red);
                            break;
                        case 'g':
                            stack.Push(Box.Green);
                            break;
                        case 'b':
                            stack.Push(Box.Blue);
                            break;
                        case 'y':
                            stack.Push(Box.Yellow);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }
            return new Stage(grapplerPosition, null, stacks);
        }
        
        private Stage(int _grapplerPosition, Box? _boxInGrappler, List<Stack<Box>> _stacks)
        {
            grapplerPosition = _grapplerPosition;
            boxInGrappler = _boxInGrappler;
            stacks = _stacks;
        }
        
        private List<Stack<Box>> CloneStacks()
        {
            var clonedStacks = new List<Stack<Box>>();
            foreach (var stack in stacks)
                clonedStacks.Add(new Stack<Box>(stack));
            return clonedStacks;
        }

        private void CheckStackSizeBeforeMove()
        {
            if (stacks[grapplerPosition].Count >= MAX_STACK_SIZE)
                throw new SimulationException(SimulationResult.CrashedIntoBoxes, "Crashed into stack " + grapplerPosition);
        }

        public Stage Left()
        {
            CheckStackSizeBeforeMove();
            if (grapplerPosition == 0)
                throw new SimulationException(SimulationResult.CrashedIntoLeftWall, "Crashed into left wall");
            return new Stage(grapplerPosition - 1, boxInGrappler, CloneStacks());
        }

        public Stage Right()
        {
            CheckStackSizeBeforeMove();
            if (grapplerPosition == stacks.Count - 1)
                throw new SimulationException(SimulationResult.CrashedIntoRightWall, "Crashed into right wall");
            return new Stage(grapplerPosition + 1, boxInGrappler, CloneStacks());
        }

        public Stage Down()
        {
            var newStacks = CloneStacks();
            var newBoxInGrappler = boxInGrappler;
            
            if (boxInGrappler.HasValue) {
                newStacks[grapplerPosition].Push(boxInGrappler.Value);
                newBoxInGrappler = null;
            }
            else if (stacks[grapplerPosition].Count > 0)
                newBoxInGrappler = newStacks[grapplerPosition].Pop();
            
            return new Stage(grapplerPosition, newBoxInGrappler, newStacks);
        }
        
        public Stage Clone()
        {
            return new Stage(grapplerPosition, boxInGrappler, CloneStacks());
        }

        public bool Matches(Condition? condition)
        {
            if (condition == null)
                return true;
            switch (condition) {
                case Condition.None:
                    return !boxInGrappler.HasValue;
                case Condition.Any:
                    return boxInGrappler.HasValue;
                case Condition.Red:
                    return boxInGrappler == Box.Red;
                case Condition.Green:
                    return boxInGrappler == Box.Green;
                case Condition.Blue:
                    return boxInGrappler == Box.Blue;
                case Condition.Yellow:
                    return boxInGrappler == Box.Yellow;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != typeof(Stage))
                return false;
            if (obj == this)
                return true;
            var that = (Stage)obj;
            return this.grapplerPosition == that.grapplerPosition
                && this.boxInGrappler == that.boxInGrappler
                && StacksEqual(that);
        }
        
        public bool StacksEqual(Stage that)
        {
            if (this.stacks.Count != that.stacks.Count)
                return false;
            
            for (int i = 0; i < stacks.Count; i++)
                if (!stacks[i].SequenceEqual(that.stacks[i]))
                    return false;
            
            return true;
        }
        
        public override int GetHashCode()
        {
            var hashCode = grapplerPosition ^ stacks.Count;
            if (boxInGrappler.HasValue)
                hashCode ^= (int)boxInGrappler;
            foreach (var box in stacks.SelectMany(stack => stack))
                hashCode ^= (int)box;
            return hashCode;
        }
    }
}