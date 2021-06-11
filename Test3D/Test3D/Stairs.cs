namespace Test3D
{
    using Microsoft.Xna.Framework;
    using static Test3D.Constants;
    public class Stairs
    {
        private Vector3 position;
        private Vector3 size;
        public readonly HorizontalElevationConfig HConfig;
        public readonly ElevatingDirection Direction;
        public readonly float Angle;

        public bool Intersects(MainCharacter mc)
        {
            return mc.getPosition().X >= position.X &&
                mc.getPosition().X <= position.X + size.X &&
                mc.getPosition().Y >= position.Y &&
                mc.getPosition().Y <= position.Y + size.Y &&
                mc.getPosition().Z >= position.Z &&
                mc.getPosition().Z <= position.Z + size.Z;
        }

        public Stairs(Vector3 position, Vector3 size, ElevatingDirection direction, float angle, HorizontalElevationConfig hConfig = HorizontalElevationConfig.None)
        {
            this.position = position;
            this.size = size;
            this.HConfig = hConfig;
            this.Direction = direction;
            this.Angle = angle;
        }
    }
}
