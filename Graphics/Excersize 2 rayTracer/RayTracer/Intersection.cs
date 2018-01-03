using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace template
{
   public class Intersection
    {

        public Tuple<Vector3, float> IntersectAll(Raytracer.Ray ray, Scene scene, Raytracer raytracer)
        {
            Tuple<Vector3, float> resultOf;
            Tuple<Vector3, float, Primitives> closestIntersection = new Tuple<Vector3, float, Primitives>(new Vector3 (0f, 0f, 0f), ray.Distance, new nullPrim());

            Vector3 colour = new Vector3(0.001f, 0.001f, 0.001f);
            List<Primitives> primitiveList = new List<Primitives>();
            primitiveList = scene.getScenePrim();
            float distance = ray.Distance;

            //Get closest intersect if there is even one
            //If there is an intersection, save the color of the prim, the distance and the prim itself
            foreach (Primitives prim in primitiveList)
            {
                resultOf = prim.Intersect(ray, prim);
                if (resultOf.Item2 < closestIntersection.Item2)
                    closestIntersection = new Tuple<Vector3,float, Primitives>(resultOf.Item1, resultOf.Item2, prim);

            }

            // reflective primitives
            if (closestIntersection.Item3.GetType() != typeof(nullPrim))
            {
                if (closestIntersection.Item3.material.reflective && ray.ReflectionCount < 20)
                {
                    Vector3 collisionPoint = ray.Origin + ray.Direction * closestIntersection.Item2;
                    Vector3 normal = getNormal(closestIntersection.Item3, collisionPoint);
                    Vector3 normalizedDirection = Vector3.Normalize(ray.Direction);
                    Vector3 v = (ray.Direction - 2 * Vector3.Dot(ray.Direction, normal) * normal);

                    v.Normalize();
                    int reflectionCount = ray.ReflectionCount + 1;
                    Raytracer.Ray reflectRay = new Raytracer.Ray(collisionPoint, v, 20, ray.Debug, reflectionCount);
                    Tuple<Vector3, float> r = IntersectAll(reflectRay, scene, raytracer);
                    if(ray.Debug)//draw the debug
                        raytracer.raytracerScreen.Line(raytracer.TX(ray.Origin.X), raytracer.TY(ray.Origin.Z), raytracer.TX(collisionPoint.X), raytracer.TY(collisionPoint.Z), raytracer.CreateRGB(100, 255, 100));
                    return new Tuple<Vector3, float>(closestIntersection.Item3.colour * r.Item1, r.Item2);

                }
            }


            //If there was an intersection with a primitive, cast a shadowray
            if (closestIntersection.Item3.GetType() != typeof(nullPrim))
            {

                //Get the real color value by casting a shadowray using the primitive that it is casted from
                //The distance from the camera to that primitive
                //The intersection point by scaling the direction vector by the distance and adding the origin
                Shadowray castedShadowray = new Shadowray();
                Vector3 intersectionOrgin = (Vector3.Add(Vector3.Multiply(ray.Direction, closestIntersection.Item2), ray.Origin));
                Tuple<Vector3, float, Primitives> tupleDuup = Tuple.Create<Vector3, float, Primitives>
                    (castedShadowray.castShadowray(closestIntersection.Item3,closestIntersection.Item2, intersectionOrgin, scene, ray.Debug, raytracer), closestIntersection.Item2, closestIntersection.Item3);
                closestIntersection = tupleDuup;//tuples are read only and we needed to change the tuple somewhat, thus this tuupleduup was required...
                if (ray.Debug && raytracer.TX(ray.Origin.X) > 512 && raytracer.TX(intersectionOrgin.X) > 512)//draw the debug
                    raytracer.raytracerScreen.Line(raytracer.TX(ray.Origin.X), raytracer.TY(ray.Origin.Z), raytracer.TX(intersectionOrgin.X), raytracer.TY(intersectionOrgin.Z), raytracer.CreateRGB(255, 255, 100));
            }

            Tuple<Vector3, float> result = Tuple.Create<Vector3, float>(closestIntersection.Item1, closestIntersection.Item2);  
            return result;
        }

        public Vector3 getNormal(Primitives prim, Vector3 intersectionOrigin)
        {
            Vector3 Normal = new Vector3();
            if (prim.GetType() == typeof(Plane))
            {
                Normal = prim.planeNormal;
            }
            else if (prim.GetType() == typeof(Sphere))
            {
                Normal = Vector3.Add(intersectionOrigin, -prim.primLocation);
            }
            return Vector3.Normalize(Normal);
        }


    }
}
