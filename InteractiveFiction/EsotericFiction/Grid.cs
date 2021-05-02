using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EsotericFiction {
    public class Grid : IDescription {
        public const char
            Ground = ' ',
            Horizontal = '═',
            Vertical = '║',
            TopLeft = '╔',
            TopRight = '╗',
            BottomRight = '╝',
            BottomLeft = '╚',
            Center = '╬',
            TopTriad = '╦',
            RightTriad = '╣',
            BottomTriad = '╩',
            LeftTriad = '╠',
            Standalone = 'O';

        public enum Tile {
            Ground, Wall
        };
        public string Description { get; private set; }
        public Tile[,] Tiles { get; }
        public Vector2 Size { get; }

        public Grid(Vector2 size) {
            Tiles = new Tile[size.x, size.y];
            Size = size;
        }

        public void GenerateRoom(Vector2 position, Vector2 size) {
            // Horizontal
            for (int x = 0; x < size.x; x++) {
                for (int y = 0; y < 2; y++) {
                    Vector2 pos = position + new Vector2(x, y * (size.y - 1));
                    Tiles[pos.x, pos.y] = Tile.Wall;
                }
            }
            // Vertical
            for (int y = 0; y < size.y; y++) {
                for (int x = 0; x < 2; x++) {
                    Vector2 pos = position + new Vector2(x * (size.x - 1), y);
                    Tiles[pos.x, pos.y] = Tile.Wall;
                }
            }
        }

        public void GenerateDisplay() {
            StringBuilder stringBuilder = new StringBuilder(Size.x * Size.y);
            for (int y = Size.y; y > -1; y--) {
                for (int x = 0; x < Size.x; x++) {
                    Vector2 pos = new Vector2(x, y);
                    if (!CheckTile(pos, Tile.Wall)) {
                        stringBuilder.Append(Ground);
                        continue;
                    }
                    Vector2 top = new Vector2(pos.x, pos.y + 1);
                    Vector2 right = new Vector2(pos.x + 1, pos.y);
                    Vector2 bottom = new Vector2(pos.x, pos.y - 1);
                    Vector2 left = new Vector2(pos.x - 1, pos.y);

                    if (CheckTile(top, Tile.Wall)) {
                        // TOP
                        if (CheckTile(right, Tile.Wall)) {
                            // TOP, RIGHT
                            if (CheckTile(bottom, Tile.Wall)) {
                                // TOP, RIGHT, BOTTOM
                                if (CheckTile(left, Tile.Wall)) {
                                    // ALL
                                    stringBuilder.Append(Center);
                                } else {
                                    // TOP, RIGHT, BOTTOM
                                    stringBuilder.Append(LeftTriad);
                                }
                            } else if (CheckTile(left, Tile.Wall)) {
                                // TOP, RIGHT, LEFT
                                stringBuilder.Append(BottomTriad);
                            } else {
                                // TOP, RIGHT
                                stringBuilder.Append(BottomLeft);
                            }
                        } else if (CheckTile(bottom, Tile.Wall)) {
                            // TOP, BOTTOM
                            if (CheckTile(left, Tile.Wall)) {
                                // TOP, BOTTOM, LEFT
                                stringBuilder.Append(RightTriad);
                            } else {
                                // TOP, BOTTOM
                                stringBuilder.Append(Vertical);
                            }
                        } else if (CheckTile(left, Tile.Wall)) {
                            // TOP, LEFT
                            stringBuilder.Append(BottomRight);
                        } else {
                            // TOP
                            stringBuilder.Append(Vertical);
                        }
                    } else if (CheckTile(right, Tile.Wall)) {
                        // RIGHT
                        if (CheckTile(bottom, Tile.Wall)) {
                            // RIGHT, BOTTOM
                            if (CheckTile(left, Tile.Wall)) {
                                // RIGHT, BOTTOM, LEFT
                                stringBuilder.Append(TopTriad);
                            } else {
                                // RIGHT, BOTTOM
                                stringBuilder.Append(TopLeft);
                            }
                        } else if (CheckTile(left, Tile.Wall)) {
                            // RIGHT, LEFT
                            stringBuilder.Append(Horizontal);
                        } else {
                            // RIGHT
                            stringBuilder.Append(Horizontal);
                        }
                    } else if (CheckTile(bottom, Tile.Wall)) {
                        // BOTTOM
                        if (CheckTile(left, Tile.Wall)) {
                            // BOTTOM, LEFT
                            stringBuilder.Append(TopRight);
                        } else {
                            // BOTTOM
                            stringBuilder.Append(Vertical);
                        }
                    } else if (CheckTile(left, Tile.Wall)) {
                        // LEFT
                        stringBuilder.Append(Horizontal);
                    } else {
                        // NONE
                        stringBuilder.Append(Standalone);
                    }
                }
                stringBuilder.Append(Environment.NewLine);
            }
            Description = stringBuilder.ToString();
        }

        public bool GetTile(Vector2 position, out Tile tile) {
            if (position.x > -1 && position.x < Size.x && position.y > -1 && position.y < Size.y) {
                tile = Tiles[position.x, position.y];
                return true;
            }
            tile = default;
            return false;
        }

        public bool CheckTile(Vector2 position, Tile tile) {
            if (GetTile(position, out Tile t)) {
                return tile == t;
            }
            return false;
        }

    }
}
