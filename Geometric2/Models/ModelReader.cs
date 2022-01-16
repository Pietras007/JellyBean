using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Geometric2.Models
{
    public static class ModelReader
    {
        /// <summary>
        /// Reads obj model from file.
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        /// <param name="path"></param>
        /// <returns></returns>
        public static MeshVertices ReadObjModelFromFile(string path)
        {
            if (Path.GetExtension(path) != ".obj")
            {
                throw new ArgumentException($"File {path} is not an obj file");
            }


            Assimp.Scene scene;
            using (var importer = new Assimp.AssimpContext())
            {
                scene = importer.ImportFile(path,
                    Assimp.PostProcessSteps.Triangulate | Assimp.PostProcessSteps.FlipUVs | Assimp.PostProcessSteps.GenerateNormals);
            }

            var mesh = scene.Meshes.FirstOrDefault();

            return new MeshVertices(scene, mesh);
        }
    }
}
