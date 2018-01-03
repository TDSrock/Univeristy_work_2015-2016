using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace template
{
    public class Scene
    {
        List<Primitives> primitiveList = new List<Primitives>();
        List<Lights> LightList = new List<Lights>();

        public Scene()
        {
            primitiveList.Add(new Plane(new Vector3(0f, -1f, 0f), new Vector3(1f, 0.5f, 0.25f), new Mat(1f, false), 2f, new Vector3(0f, -1f, 0f), ""));
            primitiveList.Add(new Plane(new Vector3(0f, -1f, 0f), new Vector3(0f, 1f, 0f), new Mat(1f, false), 2f, new Vector3(0f, 1f, 0f), "checkerd"));
            primitiveList.Add(new Plane(new Vector3(0f, -1f, 0f), new Vector3(1f, 1f, 1f), new Mat(1f, false), 4f, new Vector3(0f, 0f, -1f), ""));
            primitiveList.Add(new Plane(new Vector3(0f, -1f, 0f), new Vector3(1f, 0.2f, 1f), new Mat(1f, false), 5f, new Vector3(0f, 0f, 1f), ""));

            primitiveList.Add(new Sphere(new Vector3(-3f, 0f, 2f), new Vector3(1f, 0f, 0f), new Mat(1f, false), 1.2f, 10, 10));
            primitiveList.Add(new Sphere(new Vector3(0f, 0f, 2f), new Vector3(1f, 1f, 1f),new Mat(1f, true), 1.2f, 10, 10));
            primitiveList.Add(new Sphere(new Vector3(3f, 0f, 2f), new Vector3(1f, 0f, 0f),new Mat(0.3f, false), 1.2f, 10, 10));
            primitiveList.Add(new Sphere(new Vector3(.4f, 0f, 0f), new Vector3(1f, 1f, 1f), new Mat(1f, true), 0.2f, 10, 10));
            primitiveList.Add(new Sphere(new Vector3(1f, -.5f, -1.5f), new Vector3(1f, 1f, 0f), new Mat(1f, false), .6f, 10, 10));
            //primitiveList.Add(new Sphere(new Vector3(-3f, 0f, 2f), new Vector3(0f, 255f, 0f),/* new MatBal(),*/ 1.2f, 10, 10));
            //primitiveList.Add(new Sphere(new Vector3(3f, 0f, 2f), new Vector3(255f, 0f, 0f),/* new MatBal(),*/ 1.2f, 10, 10));
            //primitiveList.Add(new Sphere(new Vector3(-4f, 0f, 2f), new Vector3(0f, 255f, 0f),/* new MatBal(),*/ 1.2f, 10, 10));
            //primitiveList.Add(new Sphere(new Vector3(4f, 0f, 2f), new Vector3(255f, 0f, 0f),/* new MatBal(),*/ 1.2f, 10, 10));

            //LightList.Add(new BaseLight(new Vector3(0f, 1f, 3f), 5f, 5f, 5f));
            //LightList.Add(new BaseLight(new Vector3(-3f, 1f, -1f), 5f, 5f, 5f));
            //LightList.Add(new BaseLight(new Vector3(3f, 1f, -1f), 5f, 5f, 5f));
            //LightList.Add(new BaseLight(new Vector3(0f, 1f, -2f), 2f, 5f, 5f));
            LightList.Add(new BaseLight(new Vector3(-2f, 1f, -1f), 8f, 8f, 8f));
            LightList.Add(new BaseLight(new Vector3(2f, 1f, -1f), 8f, 8f, 8f));

        }
        public List<Primitives> getScenePrim()
        {
            return primitiveList;
        }
        public List<Lights> getSceneLights()
        {
            return LightList;
        }

        public void Intersect()
        {

        }
    }
}
