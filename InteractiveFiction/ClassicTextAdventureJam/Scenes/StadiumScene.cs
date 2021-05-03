using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EsotericFiction;
using ClassicTextAdventureJam.Episodes;

namespace ClassicTextAdventureJam.Scenes {
    public class StadiumScene : Scene {
        public override string Title => "Stadium";
        public override string Description { get; }
        public StadiumScene(string name) {
            Description = $@"A lectern with a screen sits in front of you. It displays ""{name}"" with a score of ""0,"" and ""{StaticData.OpponentName}"" with a score of ""0.""";
            AddEpisode(new ContestEpisode());
        }
    }
}
