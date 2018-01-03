using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;


namespace template
{
    class Shadowray
    {
        public float red, green, blue;
        Intersection intersection = new Intersection();
        List<Vector4> shadowRayLineList = new List<Vector4>();

        public Shadowray()
        {
            red = 0f;
            green = 0f;
            blue = 0f;
            
        }

        //This function returns the light colour on the intersection origin of the given primitive
        public Vector3 castShadowray(Primitives prim, float Primarydistance, Vector3 intersectionOrigin, Scene scene, bool debug, Raytracer raytracer)
        {
            Vector3 Normal = intersection.getNormal(prim, intersectionOrigin);
            bool blocked;
            //Create a list that holds all of the lights in the scene
            List<Lights> lightList = new List<Lights>();
            lightList = scene.getSceneLights();

            //Create a list that holds all primitives to see if it does not block LOS
            List<Primitives> primitiveList = new List<Primitives>();
            primitiveList = scene.getScenePrim();
            float epsilon = 0.01f;

            Vector3 foundRGB = new Vector3(0f, 0f, 0f);
            float lightAngle;
            
            //See if a light is LOS from the intersectionOrigin
            foreach (Lights aLight in lightList)
            {
                blocked = false;
                
                //Get the direction from the origin to the lightsource
                Vector3 dir = Vector3.Subtract(aLight.lightLocation, intersectionOrigin);
                float Lightraydistance = dir.Length;
                dir.Normalize();//L
                
                lightAngle = Vector3.Dot(dir, Normal);
                if(lightAngle < 0)
                {
                    lightAngle = 0;
                }

                intersectionOrigin = intersectionOrigin +(dir * epsilon);
                //Create a ray
                Raytracer.Ray sr = new Raytracer.Ray(intersectionOrigin, dir, Lightraydistance -epsilon, debug);


                if (Vector3.Add(intersectionOrigin,-prim.primLocation).Length <  prim.radius)
                {
                    //blocked = true;
                    //break;
                }

                foreach (Primitives primitive in primitiveList)
                {
                    //If something blocks LoS with the light, block is true
                    if (intersectLight(sr, primitive) == true)
                    {
                        blocked = true;
                        break;
                    }
                }
                //If it was not blocked by any Prim, use the lightsource to alter the color
                if (!blocked)
                {
                    Vector3 colourDuup = prim.colour;
                    if (prim.texureType == "checkerd")
                    {

                        float colourScale = ((int)(2 * intersectionOrigin.X) + (int)intersectionOrigin.Z) & 1;
                        colourDuup.X = prim.colour.X * colourScale;
                        colourDuup.Y = prim.colour.Y * colourScale;
                        colourDuup.Z = prim.colour.Z * colourScale;
                    }
                    //Change the light RGB
                    float lightDroppOff = (float)(1f / Math.Pow(Lightraydistance, 2));
                    Vector3 lightRGB = new Vector3(aLight.lightRGB.X * lightDroppOff, aLight.lightRGB.Y * lightDroppOff, aLight.lightRGB.Z * lightDroppOff);

                    foundRGB.X = foundRGB.X + lightRGB.X * prim.material.absorbtion * colourDuup.X * lightAngle;
                    if (foundRGB.X > 1f)
                        foundRGB.X = 1f;

                    foundRGB.Y = foundRGB.Y + lightRGB.Y * prim.material.absorbtion * colourDuup.Y * lightAngle;
                    if (foundRGB.Y > 1f)
                        foundRGB.Y = 1f;

                    foundRGB.Z = foundRGB.Z + lightRGB.Z * prim.material.absorbtion * colourDuup.Z * lightAngle;
                    if (foundRGB.Z > 1f)
                        foundRGB.Z = 1f;
                    if (debug && raytracer.TX(intersectionOrigin.X) > 512 && raytracer.TX(aLight.lightLocation.X) > 512)
                        raytracer.raytracerScreen.Line(raytracer.TX(intersectionOrigin.X), raytracer.TY(intersectionOrigin.Z), raytracer.TX(aLight.lightLocation.X), raytracer.TY(aLight.lightLocation.Z), raytracer.CreateRGB(170,170,255));
                        
                }
            }
            
            return foundRGB;
        }

        public bool intersectLight(Raytracer.Ray sr, Primitives prim)
        {
            //Get the intersection distance and if it is smaller than the lightdistance, return blocked
            Tuple<Vector3, float> result = prim.Intersect(sr, prim);
            if (result.Item2 < sr.Distance)
                return true;
            else
                return false;

        }
        
    }
}
