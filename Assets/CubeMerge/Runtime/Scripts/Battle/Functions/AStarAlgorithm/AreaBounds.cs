namespace CubeMerge.Runtime.Scripts.Battle
{
    public struct AreaBounds
    {
        public Position Center;
        public float Width;
        public float Heigth;

        public AreaBounds(Position center, float width, float heigth)
        {
            Center = center;
            Width = width;
            Heigth = heigth;
        }

        public AreaBounds(float width, float heigth)
        {
            Center = new Position(0, 0);
            Width = width;
            Heigth = heigth;
        }

        public bool IsInBounds(Position p)
        {
            return p.X <= Center.X + Width / 2 && p.X >= Center.X - Width / 2 && 
                   p.Y <= Center.Y + Heigth / 2 && p.Y >= Center.Y - Heigth / 2;
        }
    }
}