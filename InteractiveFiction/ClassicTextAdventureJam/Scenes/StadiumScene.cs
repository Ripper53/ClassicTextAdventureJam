using EsotericFiction;
using ClassicTextAdventureJam.Episodes;

namespace ClassicTextAdventureJam.Scenes {
    public class StadiumScene : Scene {
        public override string Title => "Stadium";
        public override string Description { get; }
        public StadiumScene(GameManager gameManager) {
            Description = $@"""And welcome to the annual transformation contest!"" yells a voice from above. You look around to find yourself at the center of a stadium, with dark figures bobbing side to side, and pumping their limbs up and down, at the perimeter where the shadows take hold. ""On the right,"" the voice says, and you catch the white figure floating in the air pointing towards you. ""We have the captain of the {StaticData.PlayerShipName}: {StaticData.PlayerName}!"" And the crowd cheers! ""And on the left, we have the captain of the {StaticData.OpponentShipName} space crew: {StaticData.OpponentName}!"" And the crowd hurrahs! You, {StaticData.OpponentName}, and the glowing figure bask in the radiance of a spotlight. ""They have been at war for over a millennial, and we are here to decide their fate!""

A lectern with a screen sits in front of you. It displays ""{StaticData.PlayerName}"" with a score of ""0,"" and ""{StaticData.OpponentName}"" with a score of ""0.""";
            AddEpisode(new ContestEpisode());
        }
    }
}
