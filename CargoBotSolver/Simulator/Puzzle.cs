using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CargoBotSolver
{
    public class Puzzle
    {
        Stage initialStage;
        Stage goalStage;
        Toolbox toolbox;
        
        public Puzzle(string json)
            : this(JsonConvert.DeserializeObject<PuzzleData>(json))
        {
        }
        
        public Puzzle(PuzzleData puzzleData)
        {
            initialStage = Stage.CreateStage(puzzleData.GrapplerPosition, puzzleData.Setup);
            goalStage = Stage.CreateStage(0, puzzleData.Goal);
            toolbox = null; // XXX Toolbox.CreateToolbox(puzzleData.Toolbox);
        }
        
        public Stage InitialStage
        {
            get { return initialStage; }
        }
        
        public Stage GoalStage
        {
            get { return goalStage; }
        }
    }
            
    public class PuzzleData
    {
        [JsonProperty("grapplerPosition")]
        public int GrapplerPosition { get; set; }
        
        [JsonProperty("setup")]
        public List<string> Setup { get; set; }
        
        [JsonProperty("goal")]
        public List<string> Goal { get; set; }
        
        [JsonProperty("toolbox")]
        public string Toolbox { get; set; }
    }
}
