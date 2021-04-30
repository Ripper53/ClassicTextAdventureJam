using System;
using EsotericFiction;

namespace ClassicTextAdventureJam {
    class Program {
        static void Main(string[] args) {
            Act.Write("The eye in space watches over the three in their spaceship. Constraining their freedom, disallowing any action it deems unworthy of being committed. Among the three, there is you, with the name: ");
            string name = Act.ReadLine();
            Act.WriteLine($@"""{name},"" the radio at the front of the ship broadcasts. ""I’m not sure what’s happened with our captain. He’s gone mad!""

A gunshot echoes throughout your ship from the radio, the sound of struggling people, and finally a thump to end the hustle.");

            GameManager gameManager = new GameManager();
            gameManager.SetActiveScene(new Scene("Control Room", ""));
            gameManager.Execute();
        }
    }
}
