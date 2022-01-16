using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Geometric2.RasterizationClasses;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using PrimitiveType = OpenTK.Graphics.OpenGL4.PrimitiveType;

namespace Geometric2.Models
{
    /// <summary>
    /// Data that is stores in every mesh vertex.
    /// </summary>
    public struct Vertex
    {
        public Vector3 Position;
        public Vector3 Normal;
        public Vector2 TextureCoords;
        public Vector3 Tangent;
        public Vector3 Bitangent;
    }

    /// <summary>
    /// Stores mesh's material data.
    /// </summary>
    public struct Material
    {
        public Color4 Ambient { get; set; }
        public Color4 Diffuse { get; set; }
        public Color4 Specular { get; set; }
        public Color4 Reflect { get; set; }
        public float Power { get; set; }
    }

    /// <summary>
    ///  Stores details of mesh vertices.
    /// </summary>
    public class MeshVertices : IDisposable
    {
        public Vertex[] Vertices { get; protected set; }
        public uint[] Indices { get; protected set; }
        public Material MeshMaterial { get; protected set; }
        public Vector3 CenterOfMass { get; protected set; }

        public int VAO { get; protected set; }
        public int VBO { get; protected set; }
        public int EBO { get; protected set; }

        public bool HasVertices { get; protected set; }
        public bool HasNormals { get; protected set; }
        public bool HasTangents { get; protected set; }

        public MeshVertices(Assimp.Scene scene, Assimp.Mesh mesh)
        {
            var verticesList = new List<Vertex>();

            CenterOfMass = Vector3.Zero;
            //Load vertices
            for (var i = 0; i < mesh.VertexCount; i++)
            {
                HasVertices = mesh.HasVertices;
                HasNormals = mesh.HasNormals;
                HasTangents = mesh.HasTangentBasis;

                Vertex vertex = new Vertex();
                vertex.Position = mesh.HasVertices ? mesh.Vertices[i].ToVector3() : Vector3.Zero;
                vertex.Normal = mesh.HasNormals ? mesh.Normals[i].ToVector3() : Vector3.Zero;
                vertex.TextureCoords = mesh.HasTextureCoords(0) ? mesh.TextureCoordinateChannels[0][i].ToVector2() : Vector2.Zero;
                vertex.Tangent = mesh.HasTangentBasis ? mesh.Tangents[i].ToVector3() : Vector3.Zero;
                vertex.Bitangent = mesh.HasTangentBasis ? mesh.BiTangents[i].ToVector3() : Vector3.Zero;

                CenterOfMass += vertex.Position;

                verticesList.Add(vertex);
            }

            CenterOfMass /= (float)verticesList.Count;

            Vertices = verticesList.ToArray();

            //Load indices
            Indices = mesh.GetIndices().Select(i => (uint)i).ToArray();

            //Load material
            var material = scene.Materials[mesh.MaterialIndex];
            MeshMaterial = material.ToMaterial();

            SetupMesh();
        }

        protected MeshVertices() { }


        /// <summary>
        /// Set shader uniforms regarding mesh material.
        /// </summary>
        /// <param name="shader"></param>
        public void SetMaterialUniforms(Shader shader)
        {
            if (_disposed)
                return;

            //shader.SetColor4("material.ambientVec", MeshMaterial.Ambient);
            //shader.SetColor4("material.diffuseVec", MeshMaterial.Diffuse);
            //shader.SetColor4("material.specularVec", MeshMaterial.Specular);
            //shader.SetColor4("material.reflectVec", MeshMaterial.Reflect);
            //shader.SetFloat("material.power", MeshMaterial.Power);
        }

        /// <summary>
        /// Renders mesh.
        /// </summary>
        public void Draw()
        {
            if (_disposed)
                return;

            GL.BindVertexArray(VAO);
            GL.DrawElements(PrimitiveType.Triangles, Indices.Length, DrawElementsType.UnsignedInt, 0);
            GL.BindVertexArray(0);
        }

        /// <summary>
        /// Renders mesh using instancing.
        /// </summary>
        /// <param name="instancesCount"></param>
        public void DrawInstanced(int instancesCount)
        {
            if (_disposed)
                return;

            GL.BindVertexArray(VAO);
            GL.DrawElementsInstanced(PrimitiveType.Triangles, Indices.Length, DrawElementsType.UnsignedInt, (IntPtr)0,
                instancesCount);
            GL.BindVertexArray(0);
        }

        public virtual void Dispose()
        {
            if (_disposed)
                return;

            GL.BindVertexArray(0);

            GL.DeleteVertexArray(VAO);
            GL.DeleteBuffer(VBO);
            GL.DeleteBuffer(EBO);

            _disposed = true;
        }

        protected void SetupMesh()
        {
            VAO = GL.GenVertexArray();
            VBO = GL.GenBuffer();
            EBO = GL.GenBuffer();

            var stride = Marshal.SizeOf(Vertices[0]);

            GL.BindVertexArray(VAO);

            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);

            GL.BufferData(BufferTarget.ArrayBuffer, Vertices.Length * stride, Vertices, BufferUsageHint.StaticDraw);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);
            GL.BufferData(BufferTarget.ElementArrayBuffer, Indices.Length * sizeof(uint), Indices, BufferUsageHint.StaticDraw);

            //Vertex Positions
            if (HasVertices)
            {
                GL.EnableVertexAttribArray(ShaderConstants.ShaderVertexPositionLocation);
                GL.VertexAttribPointer(ShaderConstants.ShaderVertexPositionLocation, 3, VertexAttribPointerType.Float, false, stride, Marshal.OffsetOf(typeof(Vertex), "Position"));
            }
            //Vertex Normals
            if (HasNormals)
            {
                GL.EnableVertexAttribArray(ShaderConstants.ShaderVertexNormalsLocation);
                GL.VertexAttribPointer(ShaderConstants.ShaderVertexNormalsLocation, 3, VertexAttribPointerType.Float, false, stride, Marshal.OffsetOf(typeof(Vertex), "Normal"));
            }
            //Texture Coordinates
            GL.EnableVertexAttribArray(ShaderConstants.ShaderTextureCoordsLocation);
            GL.VertexAttribPointer(ShaderConstants.ShaderTextureCoordsLocation, 2, VertexAttribPointerType.Float, false, stride, Marshal.OffsetOf(typeof(Vertex), "TextureCoords"));
            if (HasTangents)
            {
                //Vertex Tangent
                GL.EnableVertexAttribArray(ShaderConstants.ShaderVertexTangentLocation);
                GL.VertexAttribPointer(ShaderConstants.ShaderVertexTangentLocation, 3, VertexAttribPointerType.Float, false, stride, Marshal.OffsetOf(typeof(Vertex), "Tangent"));
                //Vertex Bitangent
                GL.EnableVertexAttribArray(ShaderConstants.ShaderVertexBiTangentLocation);
                GL.VertexAttribPointer(ShaderConstants.ShaderVertexBiTangentLocation, 3, VertexAttribPointerType.Float, false, stride, Marshal.OffsetOf(typeof(Vertex), "Bitangent"));
            }

            GL.BindVertexArray(0);
        }

        private bool _disposed = false;

        public void Print()
        {
            Console.WriteLine("\tVertices:");
            foreach (var item in Vertices)
            {
                Console.WriteLine($"\t\tPosition: {item.Position}");
                Console.WriteLine($"\t\tNormal: {item.Normal}");
            }

            Console.WriteLine("\tIndices:");
            foreach (var item in Indices)
            {
                Console.WriteLine($"\t\t{item}");
            }
        }
    }
}
