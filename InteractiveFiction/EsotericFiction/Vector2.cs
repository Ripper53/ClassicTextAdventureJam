namespace EsotericFiction {
    public struct Vector2 {
        public int x, y;
        public Vector2(int x, int y) {
            this.x = x;
            this.y = y;
        }

        public static Vector2 operator +(Vector2 vec1, Vector2 vec2) {
            return new Vector2(vec1.x + vec2.x, vec1.y + vec2.y);
        }

    }
}
