using System;
using System.Collections.Generic;
using EsotericFiction;
using ClassicTextAdventureJam.Scenes;
using ClassicTextAdventureJam.Items;

namespace ClassicTextAdventureJam {
    class Program {
        static void Main() {
            Act.ForegroundColor = ConsoleColor.White;
            Act.BackgroundColor = ConsoleColor.Black;

            Act.Write("The eye in space watches over a spaceship. Constraining its freedom, disallowing any action it deems unworthy of being committed. Within the ship, there is you, with the name: ");
            string name = Act.ReadLine();
            Act.WriteLine($@"""{name},"" the radio at the front of the ship broadcasts. ""I'm not sure what's happened with our captain. He's gone mad!""

A gunshot pierces your ears and echoes throughout the ship broadcasted from the radio, then the sound of struggling people, and finally a thump to end the hustling.

[Type ""HELP"" for command list]
[Command always follows: ""(Verb) (Noun)""]

Your inventory contains a map of the ship.");

            Grid grid = new Grid(new Vector2(41, 13));

            // Control Room
            grid.GenerateRoom(new Vector2(7, 7), new Vector2(7, 6));
            // Lab
            grid.GenerateRoom(new Vector2(7, 0), new Vector2(7, 6));
            // Control Room to Lab
            grid.Tiles[10, 7] = Grid.Tile.Ground;
            grid.Tiles[10, 5] = Grid.Tile.Ground;
            grid.Tiles[9, 6] = Grid.Tile.Wall;
            grid.Tiles[11, 6] = Grid.Tile.Wall;
            {
                const int y = 0;
                // A
                grid.GenerateRoom(new Vector2(0, y), new Vector2(6, 4));
                // A to Lab
                grid.Tiles[5, y + 1] = Grid.Tile.Ground;
                grid.Tiles[7, y + 1] = Grid.Tile.Ground;
                grid.Tiles[6, y + 2] = Grid.Tile.Wall;
                grid.Tiles[6, y] = Grid.Tile.Wall;
            }
            {
                // C
                const int y = 2;
                grid.GenerateRoom(new Vector2(15, y), new Vector2(5, 8));
                // C to Lab
                grid.Tiles[15, y + 1] = Grid.Tile.Ground;
                grid.Tiles[13, y + 1] = Grid.Tile.Ground;
                grid.Tiles[14, y + 2] = Grid.Tile.Wall;
                grid.Tiles[14, y] = Grid.Tile.Wall;
            }
            {
                // B
                const int x = 21, y = 2;
                grid.GenerateRoom(new Vector2(x, y), new Vector2(20, 8));
                // B to C
                grid.Tiles[x, y + 4] = Grid.Tile.Ground;
                grid.Tiles[x - 2, y + 4] = Grid.Tile.Ground;
                grid.Tiles[x - 1, y + 5] = Grid.Tile.Wall;
                grid.Tiles[x - 1, y + 3] = Grid.Tile.Wall;
            }

            grid.GenerateDisplay();
            Map map = new Map(grid) {
                Name = "Map",
                Description = "A map of the spaceship."
            };
            Act.Display(map.Grid);

            Act.WriteLine("You are within the:");

            GameManager gameManager = new GameManager(new Entity()) {
                ErrorMessage = "A shiver strikes your body, the eye disallows your attempt."
            };
            
            gameManager.PlayerEntity.Inventory.AddItem(map);
            gameManager.PlayerEntity.Inventory.GenerateDisplay();
            gameManager.SetActiveScene(BuildScenes(name));
            gameManager.Execute();
        }

        static Scene BuildScenes(string name) {
            Scene controlRoom = new StaticScene("Control Room", "The radio sits silently at the front. The grand window stares out into the darkness of space. To the south is the Lab.");
            LabScene lab = new LabScene("Lab", "Four capsules made of glass sit at the side.", "A lectern with buttons stands at the center. To the north is the Control Room.");
            Scene hall = new StaticScene("Hall", "");
            Scene storage = new StaticScene("Storage", "");
            ConcourseScene concourse = new ConcourseScene();
            StadiumScene stadium = new StadiumScene(name);

            concourse.SetDescription("A large room with multiple tables like a banquet hall. One wall scorched by the bright lights displays space across its massive window.");

            #region Episodes

            #endregion

            #region Directions
            controlRoom.AddWork(new RoamWork(new string[] { RoamWork.South, "LAB" }, lab));
            lab.AddWork(new RoamWork(new string[] { RoamWork.North, "CONTROL" }, controlRoom));
            lab.AddWork(new RoamWork(new string[] { RoamWork.East, "HALL" }, hall));
            lab.AddWork(new RoamWork(new string[] { RoamWork.West, "STORAGE" }, storage));
            storage.AddWork(new RoamWork(new string[] { RoamWork.East, "LAB" }, lab));
            hall.AddWork(new RoamWork(new string[] { RoamWork.West, "LAB" }, lab));
            hall.AddWork(new RoamWork(new string[] { RoamWork.East, "CONCOURSE" }, concourse));
            concourse.AddWork(new RoamWork(new string[] { RoamWork.West, "HALL" }, hall));

            // TEST
            controlRoom.AddWork(new RoamWork(new string[] { "TEST" }, stadium));
            #endregion

            #region Items
            controlRoom.AddItem(new RadioItem());
            controlRoom.AddItem(new WindowItem {
                Description = "The window stares out into the vastness of space. The stars glitter in their reds, blues, and whites."
            });

            {
                CapsuleItem capsuleItem = new CapsuleItem {
                    Description = "Four empty capsules sit against the wall."
                };
                WindowItem windowItem = new WindowItem {
                    Description = "The stars from here are barely visible to the naked eye because of the sharp lights within the Concourse."
                };
                lab.AddItem(capsuleItem);
                lab.AddItem(new LecternItem(lab, concourse, capsuleItem, windowItem));
                concourse.AddItem(windowItem);
            }
            #endregion

            return controlRoom;
        }

    }
}
