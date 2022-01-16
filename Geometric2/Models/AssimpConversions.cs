using OpenTK;
using OpenTK.Graphics;

namespace Geometric2.Models
{
    /// <summary>
    ///  Assimp extensions.
    /// </summary>
    public static class AssimpConversions
    {
        //from: http://www.richardssoftware.net/2013/10/loading-3d-models-using-assimpnet-and.html
        public static Vector3 ToVector3(this Vector4 v)
        {
            return new Vector3(v.X, v.Y, v.Z);
        }

        public static Vector3 ToVector3(this Assimp.Vector3D v)
        {
            return new Vector3(v.X, v.Y, v.Z);
        }

        public static Vector2 ToVector2(this Assimp.Vector3D v)
        {
            return new Vector2(v.X, v.Y);
        }

        public static Material ToMaterial(this Assimp.Material m)
        {
            var ret = new Material
            {
                Ambient = new Color4(m.ColorAmbient.R, m.ColorAmbient.G, m.ColorAmbient.B, m.ColorAmbient.A),
                Diffuse = new Color4(m.ColorDiffuse.R, m.ColorDiffuse.G, m.ColorDiffuse.B, m.ColorDiffuse.A),
                Specular = new Color4(m.ColorSpecular.R, m.ColorSpecular.G, m.ColorSpecular.B, m.Shininess),
                Reflect = new Color4(m.ColorReflective.R, m.ColorReflective.G, m.ColorReflective.B, m.ColorReflective.A)
            };

            if (ret.Ambient == new Color4(0, 0, 0, 0))
            {
                ret.Ambient = Color4.Gray;
            }

            if (ret.Diffuse == new Color4(0, 0, 0, 0) || ret.Diffuse == Color4.Black)
            {
                ret.Diffuse = Color4.White;
            }

            if (m.ColorSpecular == new Assimp.Color4D(0, 0, 0, 0) || m.ColorSpecular == new Assimp.Color4D(0, 0, 0))
            {
                ret.Specular = new Color4(0.5f, 0.5f, 0.5f, ret.Specular.A);
            }

            ret.Power = m.Shininess;

            return ret;
        }

        public static Matrix4 ToMatrix(this Assimp.Matrix4x4 m)
        {
            var ret = Matrix4.Identity;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    ret[i, j] = m[i + 1, j + 1];
                }
            }
            return ret;
        }

        public static Quaternion ToQuaternion(this Assimp.Quaternion q)
        {
            return new Quaternion(q.X, q.Y, q.Z, q.W);
        }
    }
}
