using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace template
{
    public abstract class Primitives
    {
        public Vector3 colour, primLocation;
        public Materials material;
        public string texureType;
        //stuff for circles
        public float radius;
        //stuff for planes
        public float distanceToOrigin;
        public Vector3 planeNormal;

        abstract public Tuple<Vector3, float> Intersect(Raytracer.Ray ray, Primitives prim);

    }


    class Plane : Primitives
    {


        public Plane(Vector3 vecLoc, Vector3 vecCol, Materials mat, float distance, Vector3 normal, string texture)
        {
            material = mat;
            primLocation = vecLoc;
            colour = vecCol;
            distanceToOrigin = distance;
            planeNormal = normal;
            texureType = texture;
        }

        public int CreateRGB(int red, int green, int blue) //Lets just keep this function here. Seems usefull
        {
            return (red << 16) + (green << 8) + blue;
        }

        public override Tuple<Vector3, float> Intersect(Raytracer.Ray ray, Primitives prim)
        {
            Vector3 colour = new Vector3(0f, 0f, 0f);
            float t;
            float distance = ray.Distance;
            t = -(Vector3.Dot(ray.Origin, prim.planeNormal) + distanceToOrigin) / (Vector3.Dot(ray.Direction, prim.planeNormal));
            if (t < distance && t > 0)
            {
                distance = t; colour = prim.colour;
            }
            Tuple<Vector3, float> tuple = Tuple.Create<Vector3, float>(colour, distance);
            return tuple;
        }

    }

    class Sphere : Primitives
    {


        public Sphere(Vector3 vecLoc, Vector3 vecCol, Materials mat, float radius1, int segments, int rings)
        {
            material = mat;
            primLocation.X = vecLoc.X;
            primLocation.Y = vecLoc.Y;
            primLocation.Z = vecLoc.Z;
            colour = vecCol;
            radius = radius1;
        }

        public override Tuple<Vector3, float> Intersect(Raytracer.Ray ray, Primitives prim)
        {
            Vector3 colour = new Vector3(0f, 0f, 0f);
            float distance = ray.Distance;

            Vector3 c = Vector3.Subtract(primLocation, ray.Origin);
            float t = Vector3.Dot(c, ray.Direction);
            Vector3 q = Vector3.Subtract(c, (Vector3.Multiply(ray.Direction, t)));
            float p2 = Vector3.Dot(q, q);
            if (p2 > (Math.Pow(prim.radius, 2)))
            {
                //if a ray does not hit a primitive, incase we want to add functionalty to the intersect function.
            }
            else
            {
                t -= (float)Math.Sqrt((Math.Pow(prim.radius, 2)) - p2);
                if ((t < ray.Distance) && (t > 0))
                {
                    distance = t;
                    colour = prim.colour;
                }
            }
            Tuple<Vector3, float> tuple = Tuple.Create<Vector3, float>(colour, distance);

            return tuple;

        }

    }

    class nullPrim : Primitives
    {
        public override Tuple<Vector3, float> Intersect(Raytracer.Ray ray, Primitives prim)
        {
            throw new NotImplementedException();
        }
    }

}
