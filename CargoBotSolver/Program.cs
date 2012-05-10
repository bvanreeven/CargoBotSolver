using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CargoBotSolver
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            
            var puzzle = new Puzzle(File.ReadAllText("../../Puzzles/Re-Curses.json"));
            var algorithm = new BruteForceAlgorithm(false);
            var solver = new Solver(puzzle, algorithm);
            solver.Solve();
            Console.WriteLine("Done!");
        }
    }
}
