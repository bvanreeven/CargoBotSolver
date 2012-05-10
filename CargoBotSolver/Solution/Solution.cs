using System;
using System.Collections.Generic;

namespace CargoBotSolver
{
    public class Solution
    {
        private readonly List<Function> functions = new List<Function>();

        public Solution()
        {
            for (int i = 0; i < 4; i++)
                functions.Add(new Function());
        }
        
        public List<Function> Functions
        {
            get { return functions; }
        }
        
        public Function F1
        {
            get { return functions[0]; }
        }
        
        public Function F2
        {
            get { return functions[1]; }
        }
        
        public Function F3
        {
            get { return functions[2]; }
        }
        
        public Function F4
        {
            get { return functions[3]; }
        }
        
        public override string ToString()
        {
            return string.Format("  F1 = {0}\n  F2 = {1}\n  F3 = {2}\n  F4 = {3}", F1, F2, F3, F4);
        }
    }
}