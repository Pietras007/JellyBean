using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometric2.Helpers
{
    public static class HelpFunctions
    {
        public static float DeKastilio(float[] vert, float t, int degree)
        {
            for (int i = 0; i < degree; i++)
            {
                for (int j = 0; j < degree - i - 1; j++)
                {
                    vert[j] = (1 - t) * vert[j] + t * vert[j + 1];
                }
            }

            return vert[0];
        }
    }
}
