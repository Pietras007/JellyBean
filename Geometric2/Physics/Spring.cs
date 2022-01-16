using OpenTK;

namespace Geometric2.Physics
{
    public class Spring
    {
        public ControlPoint P0 { get; private set; }
        public ControlPoint P1 { get; private set; }
        public float InitialLength { get; private set; }


        public readonly float Eps = 1e-4f;

        public Spring(ControlPoint p0, ControlPoint p1, float initialLength)
        {
            P0 = p0;
            P1 = p1;
            InitialLength = initialLength;
        }

        public void CalculateNextForce(float stiffness)
        {
            var deltaLength = Vector3.Distance(P0.LastData.Position, P1.LastData.Position) - InitialLength;
            if (deltaLength < Eps) //TODO: check what happens without this if
                return;

            var force = stiffness * deltaLength;
            var vecP0P1 = (P1.LastData.Position - P0.LastData.Position).Normalized();

            var hookeForce = force * vecP0P1;

            P0.Data.Force += hookeForce;//TODO: should be OK but check if sign is correct
            P1.Data.Force += -hookeForce;
        }

        public override string ToString()
        {
            return $"P0: {P0.LastData.Position}, P1: {P1.LastData.Position}, L: {InitialLength}";
        }
    }
}
