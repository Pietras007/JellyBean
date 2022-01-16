using OpenTK;

namespace Geometric2.Physics
{
    public struct PointData
    {
        public Vector3 Position;
        public Vector3 Velocity;
        public Vector3 Force;

        public static PointData Zero => new PointData()
        {
            Position = Vector3.Zero,
            Velocity = Vector3.Zero,
            Force = Vector3.Zero
        };
    }
}
