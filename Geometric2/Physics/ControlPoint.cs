using OpenTK;

namespace Geometric2.Physics
{
    public class ControlPoint
    {
        /// <summary>
        /// Point data in the last time frame
        /// </summary>
        public PointData LastData;
        /// <summary>
        /// Point data in current time frame
        /// </summary>
        public PointData Data;

        public float Mass { get; set; }

        public ControlPoint(Vector3 position, Vector3 velocity, float mass)
        {
            LastData.Position = position;
            LastData.Velocity = velocity;
            LastData.Force = Vector3.Zero;
            Mass = mass;
        }

        public void ApplyFriction(float frictionCoefficient)
        {
            Data.Force += -frictionCoefficient * LastData.Velocity;
        }

        public void CalculateNextStep(float deltaTime)
        {
            Data.Velocity = LastData.Velocity + deltaTime * Data.Force / Mass;
            Data.Position = LastData.Position + deltaTime * Data.Velocity;

            LastData = Data;
            LastData.Force = Vector3.Zero;
            Data = PointData.Zero;
        }

        public override string ToString()
        {
            return $"LastData: {LastData.Position}, Data: {Data.Position}, Mass: {Mass}";
        }
    }
}
