using System;
using System.Collections.Generic;
using EsotericFiction;
using ClassicTextAdventureJam.Scenes;

namespace ClassicTextAdventureJam {
    class Program {
        static void Main() {
            Act.Write("The eye in space watches over a spaceship. Constraining its freedom, disallowing any action it deems unworthy of being committed. Within the ship, there is you, with the name: ");
            string name = Act.ReadLine();
            Act.WriteLine($@"""{name},"" the radio at the front of the ship broadcasts. ""I'm not sure what's happened with our captain. He's gone mad!""

A gunshot echoes throughout your ship from the radio, the sound of struggling people, and finally a thump to end the hustle.

[Type ""HELP"" for command list]

You are within the: ");

            GameManager gameManager = new GameManager(new Entity()) {
                ErrorMessage = "A shiver strikes your body, the eye disallows your attempt."
            };
            gameManager.SetActiveScene(BuildScenes(name));
            gameManager.Execute();
        }

        static Scene BuildScenes(string name) {
            Scene
                controlRoom = new StaticScene("Control Room", "The radio sits silently at the front. The grand windows stare out into the darkness of space. To the south is the Lab."),
                lab = new LabScene("Lab", "Four capsules made of glass sit at the side.", "A lectern with buttons stands at the center. To the north is the Control Room.");

            controlRoom.AddWork(new RoamWork(new string[] { RoamWork.South, "LAB" }, lab));
            lab.AddWork(new RoamWork(new string[] { RoamWork.North, "CONTROL" }, controlRoom));

            return controlRoom;
        }

    }
}
