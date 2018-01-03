using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Template_P3
{
    class SceneGraph
    {
       
        int SceneGraphID;
        public List<Mesh> childMeshes;
        static List<Light> LightList;

        public SceneGraph()
        {
            //for the lights
            LightList = new List<Light>();
            //LightList.Add(new Light(new Vector3(6f, 3f, 6f), 50000f, new Vector4(1f, 1f, 0.4f, 0.5f), new Vector4(1.9f, 1.5f, 1.5f, 1.0f)));
            //LightList.Add(new Light(new Vector3(6f, -40f, -600f), 5000f, new Vector4(1f, 1f, 1f, 0.5f), new Vector4(1f, 1f, 1f, 1.0f)));

            //for the meshes
            childMeshes = new List<Mesh>();
            childMeshes.Add(new Mesh("../../assets/Miami 2525.obj", //the City
                new Vector3(100f, -5f, 1050f),
                new Texture("../../assets/BLF1.jpg")));

            childMeshes.Add(new Mesh("../../assets/track01_.obj", //the racetrack
                new Vector3(0f, 0f, 0f), 
                new Texture("../../assets/track01.jpg")));

            childMeshes[1].setChild(new Mesh("../../assets/bluefalcon.obj", //the car, child of the racetrack
                 new Vector3(0, 20f, 470f),
                 new Texture("../../assets/bluefalcon.jpg")));

            childMeshes[1].childMeshes[0].setChild(new Mesh("../../assets/cone.obj", //a cone to simulate an exhaust, child of the car
                 new Vector3(0, 0, 0),
                 new Texture("../../assets/flame.jpg")));

            childMeshes[1].childMeshes[0].setChild(new Mesh("../../assets/teapot.obj", //first teapot child of the car
                new Vector3(0, 40f, 0),
                new Texture("../../assets/wood.jpg")));

            childMeshes[1].childMeshes[0].childMeshes[1].setChild(new Mesh("../../assets/teapot.obj", // second teapot child of the first teapot
                new Vector3(0, 0, 30),
                new Texture("../../assets/wood.jpg")));

        }
   
        public void Render (Matrix4 cameraMatrix, Shader s, Texture t,Vector3 target)
        {
            foreach (Mesh m in childMeshes)
            {      
                m.RenderM(cameraMatrix, Matrix4.CreateTranslation(0f,0f,0f), s, t);
                
            }
        }

        public void AddLight(Light l)
        {
            LightList.Add(l);
        }

        public static List<Light> _Light
        {
            get
            {
                return LightList;
            }
        }
    }
}
