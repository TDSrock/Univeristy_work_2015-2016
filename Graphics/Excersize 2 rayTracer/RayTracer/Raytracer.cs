using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using System.Threading;

namespace template
{
    public class Raytracer
    {
        Thread[] rayThreads = new Thread[Environment.ProcessorCount * 2];
        public Surface raytracerScreen;
        Scene raytracerScene;
        public Camera raytracerCamera;
        int debugWindowWidth = 512;
        int debugWindowHeight = 512;
        int debugLocationX = 512;
        float debugXpos = 0, debugYpos = 0, debugUnit, debugZoom = 1f;
        List<Vector2> plotList = new List<Vector2>();
        public int counter = 1, AApow = 0; //AAPow is used to modify AA
        public int AA = 1;


        public Raytracer(Surface screen)
        {
            raytracerScreen = screen;
            raytracerScene = new Scene();
            //FOV is the 7th parameter!
            raytracerCamera = new Camera(0f, 0f, -4f, 0f, 0f, 1f, 90f, raytracerScreen.width/2, raytracerScreen.height);
        }

        public void Render()
        {
            List<Tuple<int ,int  ,int>> plotListRender = new List<Tuple<int, int, int>>();

            RenderDebug(debugWindowWidth, debugWindowHeight);
            int[,] colorPixelArray = new int [512, 512];
            //counter = 1;
            int jumpDistance = 512 / rayThreads.Length;
            for (int y = 0; y < rayThreads.Length; y++)
            {
                int dupY = y;
                int startingPoint = dupY * jumpDistance;
                rayThreads[dupY] = new Thread(() => singleRayThread(startingPoint));
                rayThreads[dupY].Start();
                //Console.WriteLine("thread was started with startingPoint: " + startingPoint);
            }
            foreach (Thread rayThread in rayThreads)
                rayThread.Join();
            //rayThread.Join();
            //int i = 1;
            //drawPlotList(plotListRender);
        }
        public void singleRayThread(int startingPoint)
        {
            //Console.WriteLine("thread was started with startingPoint: " + startingPoint);
            //colorPixelArray[x, y] = GenerateRay(x, y);
            //plotListRender.Add(new Tuple<int, int, int>(x, y, GenerateRay(x, y)));
            float stepSize = 1f / AA;
            for (int y = startingPoint; y < startingPoint + (512 / rayThreads.Length); y++)
            { 
                for (int x = 0; x < 512; x++)
                {
                    //Console.WriteLine("Attempting to render raytracer coordinate X: " + x + " Y: " + y);
                    Vector3 colour = new Vector3(0, 0, 0);
                    for(float yaa = y; yaa < 1 + y; yaa += stepSize)
                        for (float xaa = x; xaa < 1 + x; xaa += stepSize)
                        {
                            if(yaa == 256 && xaa % 32 == 0)
                            {
                                colour += GenerateRay(xaa, yaa, true);
                            }
                            else
                            {
                                colour += GenerateRay(xaa, yaa);
                            }
                             
                        }
                    
                    raytracerScreen.Plot(x, (512 - y), CreateRGB((int)(colour.X / (AA * AA) * 255), (int)(colour.Y / (AA * AA) * 255), (int)(colour.Z / (AA * AA) * 255)));
                }
            }
            //Console.WriteLine("thread has ended with startingPoint: " + startingPoint);

        }

        public Vector3 GenerateRay (float x, float y, bool debug = false)
        {
            float X = x / 512;
            float Y = y / 512;
            Vector3 originCam = raytracerCamera.cameraLocation;
            Vector3 dir = raytracerCamera.screen00 + X * (raytracerCamera.screen10 - raytracerCamera.screen00) + Y * (raytracerCamera.screen01 - raytracerCamera.screen00);
            Vector3 rayDir = dir - originCam;
            Vector3 rayOrigin = originCam;
            Tuple<Vector3,float> r;
            rayDir.Normalize();
            Ray Ray = new Ray(rayOrigin, rayDir, 20, debug);
            Intersection intersection = new Intersection();
            r = intersection.IntersectAll(Ray, raytracerScene, this);

           

            return r.Item1; // = intersection.sphereintersection(Ray, raytracerScene); 
        }

        public float getShortestLength(Vector3 rayDir, Vector3 rayOrigin)
        {
            float sx, sy, sz;

            if (rayDir.X == 0)
                sx = 99999999999f;
            else
            {
                if (rayDir.X > 0)
                {
                    sx = (float)(5 - rayOrigin.X) / rayDir.X;
                }
                else
                {
                    sx = (float)((-5) - rayOrigin.X) / rayDir.X;
                }
            }
            if (rayDir.Y == 0)
                sy = 99999999999f;
            else
            {
                if (rayDir.Y > 0)
                {
                    sy = (float)(5 - rayOrigin.Y) / rayDir.Y;
                }
                else
                {
                    sy = (float)((-5) - rayOrigin.Y) / rayDir.Y;
                }
            }
            if (rayDir.Z == 0)
                sz = 99999999999f;
            else
            {
                if (rayDir.Z > 0)
                {
                    sz = (float)(5 - rayOrigin.Z) / rayDir.Z;
                }
                else
                {
                    sz = (float)((-5) - rayOrigin.Z) / rayDir.Z;
                }
            }

            if (sx < sz)
            {
                if (sx < sy)
                {
                    return sx -0.01f;
                }
                {
                    return sy -0.01f;
                }
            }
            else if (sz < sy)
            {
                return sz -0.01f;
            }
            else
            {
                return sy -0.01f;
            }
            
        }


        public struct Ray
        {
            Vector3 origin;
            Vector3 direction;
            float distance;
            int reflectionCount;
            bool debug;

            public Ray (Vector3 orig, Vector3 dir, float dist, bool Debug, int ReflectionCount = 0 )
            {
                origin = orig;
                direction = dir;
                distance = dist;
                reflectionCount = ReflectionCount;
                debug = Debug;
            }

            public Vector3 Origin { get { return origin; } }
            public Vector3 Direction { get { return direction; } }
            public float Distance { get { return distance; } }
            public int ReflectionCount { get { return reflectionCount; } }
            public bool Debug { get { return debug; } }
        }


        public void RenderDebug(int windowWidth, int windowHeight)
        {
            List<Tuple<float, float, int>> plotList = new List<Tuple<float, float, int>>();
            List<Primitives> primList = raytracerScene.getScenePrim();
            List<Lights> lightList = raytracerScene.getSceneLights();
            Camera debugCamera = raytracerCamera;
            debugUnit = raytracerScreen.width / 2 / 10;
            for (int i = 0; i < lightList.Count; i++)
            {
                //As we currently assume each light is a point light we don't need to subspecify anything here. Each point light will apear as a 9x9 area, NOTE lights are NOT handled through the plotlist!
                for (int l = -3; l != 3; l++)
                    for (int k = -3; k != 3; k++)
                        raytracerScreen.Plot(TX(lightList[i].lightLocation.X) + l, TY(lightList[i].lightLocation.Z) + k, CreateRGB(255, 255, 0));
            }
            for (int i = 0; i < primList.Count; i++)
            {
                //NOTICE, IF WE MAKE MORE PRIM TYPES BE SURE TO SUPPORT IT HERE!
                if (primList[i].GetType() == typeof(Sphere))
                {
                    double sliceLocation = primList[i].primLocation.Y - raytracerCamera.cameraLocation.Y;
                    double sliceRadius = Math.Sqrt(Math.Pow(primList[i].radius, 2) - Math.Pow(sliceLocation, 2));
                    double plots = sliceRadius * 360;
                    double increaseincrement = 420 / plots;
                    for (double k = 0; k < 360; k += increaseincrement)
                    {
                        double angle = k * Math.PI / 180;

                        if (Math.Abs(sliceLocation) < primList[i].radius)
                        {

                            float x = (float)(primList[i].primLocation.X + sliceRadius * Math.Cos(angle));
                            float y = (float)(primList[i].primLocation.Z + sliceRadius * Math.Sin(angle));
                            int color = CreateRGB((int)(primList[i].colour.X * 255), (int)(primList[i].colour.Y * 255),(int)(primList[i].colour.Z * 255));
                            plotList.Add(new Tuple<float, float, int>(x, y, color));
                        }

                    }
                }
                else if (primList[i].GetType() == typeof(Plane))
                {

                }
                else
                {
                    raytracerScreen.Print("NOTICE, PRIMITIVE :NOT DETECTED: ERROR!", 514, raytracerScreen.height - 20, 0xfffff);
                }
            }

            drawPlotList(plotList);
            //Draw the camera

            Vector3 scrLeft = raytracerCamera.screen00;
            Vector3 scrRight = raytracerCamera.screen10;
            Vector3 camLoc = raytracerCamera.cameraLocation;

            raytracerScreen.Line(TX(scrLeft.X), TY(scrLeft.Z),TX(scrRight.X), TY(scrRight.Z), CreateRGB(255,0,0));
            //This draws a redline representing the screen. We will use this to test our raycasting(whether they actualy get cast through the screen and if our intersections are working properly)

            for (int l = -3; l != 3; l++) 
                for (int k = -3; k != 3; k++)
                    raytracerScreen.Plot(TX(camLoc.X) + l, TY(camLoc.Z) + k, CreateRGB(0,255,0));
            //This draws the cam as a 9x9 green block(due to debugs lines doing this is essential, and it means we maintain clarity of where the center of the camera is

            for (int l = -3; l != 3; l++)
                for (int k = -3; k != 3; k++)
                    raytracerScreen.Plot(TX(raytracerCamera.targetLocation.X) + l, TY(raytracerCamera.targetLocation.Z) + k, CreateRGB(0, 255, 255));
            //This draws the targetLocation to verify the screen is updating properly.

            

        }

        private void drawPlotList(List<Tuple<float, float, int>> PlotList)
        {
            int plotCount = 0;
            raytracerScreen.Print("0,0", TX(0f), TY(0f), 0xffffff);
            raytracerScreen.Print("3,0", TX(3f), TY(0f), 0xffffff);
            raytracerScreen.Print("-3,0", TX(-3f), TY(0f), 0xffffff);
            raytracerScreen.Print("0,3", TX(0f), TY(3f), 0xffffff);
            raytracerScreen.Print("0,-3", TX(0f), TY(-3f), 0xffffff);//These prints are mainly here for debugging purposes.
            raytracerScreen.Print("Debug camera Height: " + raytracerCamera.cameraLocation.Y, 2 + debugLocationX, 20, 0xffffff);
            raytracerScreen.Line(TX(0), TY(5), TX(0), TY(-5), 0xffffff);//draws the y axis, this is for debugging purposes
            raytracerScreen.Line(TX(5), TY(0), TX(-5), TY(0)-1, 0xffffff);//draws the x axis, this is for debugging purposes
            raytracerScreen.Box(debugLocationX, 0, raytracerScreen.width - 1, raytracerScreen.height - 1, 0xffffff);
            for (int i = 0; i < PlotList.Count; i++)
            {
                if (TX(PlotList[i].Item1) > debugLocationX)
                {
                    raytracerScreen.Plot(TX(PlotList[i].Item1), TY(PlotList[i].Item2), PlotList[i].Item3);
                    plotCount++;
                }
            }
            raytracerScreen.Print("Debug plots drawing: " + plotCount, 2 + debugLocationX, 0, 0xffffff);

        }

        public int CreateRGB(int red, int green, int blue) //Lets just keep this function here. Seems usefull
        {
            return (red << 16) + (green << 8) + blue;
        }
        public int TX(float x)//translate the floats obtained from the scene to points on the DebugScreen
        {
            int xcenter = (int)(raytracerScreen.width * 0.25 + debugLocationX);//center of the graphs x axis
            int r = (int)(x * debugUnit * debugZoom + xcenter + debugXpos);
            
            return r;
        }
        public int TY(float y)//translate the floats obtained from the scene to points on the DebugScreen
        {
            int ycenter = (int)(raytracerScreen.height * 0.5);//center of the graphs y axis
            
            int r = (int)(-1 * y * debugUnit * debugZoom + ycenter + -debugYpos);
            return r;
        }
        public void alterAA(int direction)
        {
            AApow += direction;
            if(AApow < 0)
            {
                AApow = 0;
            }
            
            AA = (int)Math.Pow((double)2, (double)AApow);
        }
    }
}
