using System;
using System.Runtime.InteropServices;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;

namespace Template_P3
{

    // mesh and loader based on work by JTalton; http://www.opentk.com/node/642

    public class Mesh
    {
        // data members
        
        public ObjVertex[] vertices;			// vertex positions, model space
        public ObjTriangle[] triangles;			// triangles (3 vertex indices)
        public ObjQuad[] quads;					// quads (4 vertex indices)
        int vertexBufferId;						// vertex buffer
        int triangleBufferId;					// triangle buffer
        int quadBufferId;                       // quad buffer
        int LightBufferID;                      // Light buffer
        public Vector3 distanceFromParent;
        int lightCount = 0;

        public List<Mesh> childMeshes;
        public Matrix4 modelMatrix;
        public static Matrix4 worldMatrix;
        Matrix4 orthoMatrix;
        Matrix4 canonicalMatrix;
        Matrix4 screenMatrix;
        Matrix4 resultMatrix;
        Texture texture;

        // constructor
        public Mesh(string fileName, Vector3 d, Texture t)
        {
            MeshLoader loader = new MeshLoader();
            loader.Load(this, fileName);
            modelMatrix = Matrix4.Identity;
            childMeshes = new List<Mesh>();
            distanceFromParent = d;
            texture = t;
        }

        //adding child meshes
        public void setChild(Mesh child)
        {
            childMeshes.Add(child);
        }

        public void RenderM(Matrix4 cameraMatrix, Matrix4 parentModelMatrix, Shader s, Texture t)
        {
            //Render the current mesh
            modelToView(parentModelMatrix);

            //World space  camera space  orthographic view  canonical view

            toOrtho(cameraMatrix);
            calculateCanonicalMatrix();
            calculateScreenMatrix();

            resultMatrix = worldMatrix * cameraMatrix;

            Render(s, resultMatrix, texture, worldMatrix);

            //Render all of it's child meshes
            foreach (Mesh m in childMeshes)
            {
                m.RenderM(cameraMatrix, worldMatrix, s,t);
            }
        }

        public void calculateScreenMatrix()
        {
            int xMax = 1024;
            int yMax = 1024;
            Matrix4 screen = new Matrix4(
                //Row 1
                (xMax / 2), 0, 0, (xMax / 2),
                //Row 2
                0, (yMax / 2), 0, (yMax / 2),
                //Row 3
                0, 0, 1, 0,
                //Row 4
                0, 0, 0, 1
                );
            screenMatrix = screen;//* canonicalMatrix;
        }


        public void calculateCanonicalMatrix()
        {

            int r, l, t, b, n, f;
            r = 1;
            l = -1;
            t = -1;
            b = 1;
            n = 1;
            f = -1;
            Matrix4 canonOrtho = new Matrix4(
                //Row 1
                ((2 * n) / (r - l)), 0, ((l + r) / (l - r)), 0
                //Row 2
                , 0, ((2 * n) / (t - b)), ((b + t) / (b - t)), 0
                //Row 3
                , 0, 0, ((n + f) / (n - f)), ((2 * f * n) / (f - n))
                //Row 4
                , 0, 0, 1, 0

                );
            canonicalMatrix = canonOrtho; //* viewMatrix;
        }

        public void toOrtho(Matrix4 cameraMatrix)
        {

            Vector3 up = new Vector3(cameraMatrix.Row1.X, cameraMatrix.Row1.Y, cameraMatrix.Row1.Z);
            Vector3 pos = new Vector3(cameraMatrix.Column3.X, cameraMatrix.Column3.Y, cameraMatrix.Column3.Z);
            Vector3 xVec = new Vector3(cameraMatrix.Row0.X, cameraMatrix.Row0.Y, cameraMatrix.Row0.Z);

            Vector3 V = new Vector3(cameraMatrix.Row2.X, cameraMatrix.Row2.Y, cameraMatrix.Row2.Z);//new Vector3(cameraMatrix.Column3.X, cameraMatrix.Column3.Y, cameraMatrix.Column3.Z);
            Vector3 z_axis = -V;
            Vector3 x_axis = Vector3.Cross((-V), up);
            Vector3 y_axis = Vector3.Cross(V, xVec);


            Matrix4 translationOrtho = new Matrix4(
                //Row 1
                x_axis.X, x_axis.Y, x_axis.Y, -pos.X
                //Row 2
                , y_axis.X, y_axis.Y, y_axis.Z, -pos.Y
                //Row 3
                , (-V.X), (-V.Y), (-V.Z), -pos.Z
                //Row 4
                , 0, 0, 0, 1
                );


            orthoMatrix = translationOrtho;// *invCamLoc;
        }

        //Alter the model matrix to allign with the parent matrix
        public void modelToView(Matrix4 parent)
        {
            Matrix4 d = Matrix4.CreateTranslation(distanceFromParent.X,
                distanceFromParent.Y, distanceFromParent.Z);

            worldMatrix = modelMatrix * parent;
            worldMatrix *= d;
        }

        // initialization; called during first render
        public void Prepare(Shader shader)
        {
            if (vertexBufferId == 0)
            {
                // generate interleaved vertex data (uv/normal/position (total 8 floats) per vertex)
                GL.GenBuffers(1, out vertexBufferId);
                GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferId);
                GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(vertices.Length * Marshal.SizeOf(typeof(ObjVertex))), vertices, BufferUsageHint.StaticDraw);

                // generate triangle index array
                GL.GenBuffers(1, out triangleBufferId);
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, triangleBufferId);
                GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(triangles.Length * Marshal.SizeOf(typeof(ObjTriangle))), triangles, BufferUsageHint.StaticDraw);

                // generate quad index array
                GL.GenBuffers(1, out quadBufferId);
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, quadBufferId);
                GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(quads.Length * Marshal.SizeOf(typeof(ObjQuad))), quads, BufferUsageHint.StaticDraw);

                //build LightList buffer
                GL.GenBuffers(1, out LightBufferID);
                GL.BindBuffer(BufferTarget.ShaderStorageBuffer, LightBufferID);
                GL.BufferData(BufferTarget.ShaderStorageBuffer, (IntPtr)(SceneGraph._Light.Count * sizeof(float) * 12), (IntPtr)(null), BufferUsageHint.DynamicDraw);//Note the 16 represents the amount of floats in the light struct KEEP THIS AS A MUTIPULE OF 4
#pragma warning disable CS0618 // Type or member is obsolete
                GL.BindBufferBase(BufferTarget.ShaderStorageBuffer, 0, LightBufferID);
#pragma warning restore CS0618 // Type or member is obsolete
            }
        }

        // render the mesh using the supplied shader and matrix
        public void Render(Shader shader, Matrix4 transform, Texture texture, Matrix4 worldTrans)
        {
            // on first run, prepare buffers
            Prepare(shader);

            // safety dance
            GL.PushClientAttrib(ClientAttribMask.ClientVertexArrayBit);

            // enable texture
            int texLoc = GL.GetUniformLocation(shader.programID, "pixels");
            GL.Uniform1(texLoc, 0);
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, texture.id);

            // enable shader
            GL.UseProgram(shader.programID);

            // pass transform to vertex shader
            GL.UniformMatrix4(shader.uniform_mview, false, ref transform);

            shader.SetUniform("ambient", new Vector4(0.7f, 0.7f, 0.7f, 1.0f));
            shader.SetUniform("camPos", Vector3.Transform(Vector3.Zero, Game._CameraMatrix));
            shader.SetUniform("worldTrans", false, worldTrans);



            //update the light buffer
            //GL.BindBuffer(BufferTarget.ShaderStorageBuffer, LightBufferID);
#pragma warning disable CS0618 // Type or member is obsolete
            GL.BindBufferBase(BufferTarget.ShaderStorageBuffer, 0, LightBufferID);
#pragma warning restore CS0618 // Type or member is obsolete
            Light[] lightArray = SceneGraph._Light.ToArray();
            if (lightArray.Length != lightCount)
            {
                lightCount = lightArray.Length;
                GL.BufferData(BufferTarget.ShaderStorageBuffer, (IntPtr)(lightArray.Length * sizeof(float) * 12), (IntPtr)(null), BufferUsageHint.DynamicDraw);
            }
            GL.BufferSubData(BufferTarget.ShaderStorageBuffer, (IntPtr)0, (IntPtr)(lightArray.Length * sizeof(float) * 12), lightArray);

            // enable position, normal and uv attributes
            GL.EnableVertexAttribArray(shader.attribute_vpos);
            GL.EnableVertexAttribArray(shader.attribute_vnrm);
            GL.EnableVertexAttribArray(shader.attribute_vuvs);

            // bind interleaved vertex data
            GL.EnableClientState(ArrayCap.VertexArray);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferId);
            GL.InterleavedArrays(InterleavedArrayFormat.T2fN3fV3f, Marshal.SizeOf(typeof(ObjVertex)), IntPtr.Zero);

            // link vertex attributes to shader parameters 
            GL.VertexAttribPointer(shader.attribute_vuvs, 2, VertexAttribPointerType.Float, false, 32, 0);
            GL.VertexAttribPointer(shader.attribute_vnrm, 3, VertexAttribPointerType.Float, true, 32, 2 * 4);
            GL.VertexAttribPointer(shader.attribute_vpos, 3, VertexAttribPointerType.Float, false, 32, 5 * 4);

            // bind triangle index data and render
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, triangleBufferId);
            GL.DrawArrays(PrimitiveType.Triangles, 0, triangles.Length * 3);

            // bind quad index data and render
            if (quads.Length > 0)
            {
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, quadBufferId);
                GL.DrawArrays(PrimitiveType.Quads, 0, quads.Length * 4);
            }

            // restore previous OpenGL state
            GL.UseProgram(0);
            GL.PopClientAttrib();
        }

        // layout of a single vertex
        [StructLayout(LayoutKind.Sequential)]
        public struct ObjVertex
        {
            public Vector2 TexCoord;
            public Vector3 Normal;
            public Vector3 Vertex;
        }

        // layout of a single triangle
        [StructLayout(LayoutKind.Sequential)]
        public struct ObjTriangle
        {
            public int Index0, Index1, Index2;
        }

        // layout of a single quad
        [StructLayout(LayoutKind.Sequential)]
        public struct ObjQuad
        {
            public int Index0, Index1, Index2, Index3;
        }
    }

} // namespace Template_P3