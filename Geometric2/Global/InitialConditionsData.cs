using OpenTK;
using System;

namespace Geometric2.Global
{
    //maybe change it to struct
    public class InitialConditionsData
    {
        public double pointMass = 1;
        public double resilience_c1 = 1;
        public double tenacityRate_k = (double)(Math.PI / 180) * 15;
        public double resilience_c2 = (double)(Math.PI / 180) * 15;
        public double integrationStep = 0.001;

        public Vector3d inertiaTensor;
        public double mass;
        public Vector3d massCentre;
        public Quaterniond massCentreQuaternion;

        public void CalculateValues()
        {
            //inertia tensor
            var inertiaTensorBaseX = 11d / 12d;
            var inertiaTensorBaseY = 1d / 6d;
            var inertiaTensorBaseZ = 11d / 12d;

            inertiaTensor = Math.Pow(pointMass, 5d) * resilience_c1 * new Vector3d(inertiaTensorBaseX, inertiaTensorBaseY, inertiaTensorBaseZ);

            //mass
            mass = Math.Pow(pointMass, 3) * resilience_c1;

            //centre of mass
            massCentre = new Vector3d(0, pointMass * Math.Sqrt(3) / 2d, 0);
            massCentreQuaternion = new Quaterniond(massCentre, 0f);
        }
    }
}
