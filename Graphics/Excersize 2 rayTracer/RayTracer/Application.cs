using System;
using System.IO;
using OpenTK.Input;
using System.Diagnostics;

namespace template
{

    class Application
    {
        // member variables
        public Stopwatch timer;
        public Surface screen;
        public Raytracer raytracer;
        public double ms, msl;
        // initialize
        public void Init()
        {
            timer = new Stopwatch();
            timer.Start();
            ms = 0;
            msl = 0;
            raytracer = new Raytracer(screen);
        }
        // tick: renders one frame
        public void Tick()
        {

            var state = Keyboard.GetState();
            if (state[Key.W])//move camera forward
                raytracer.raytracerCamera.cameraLocation.Z += 0.1f;

            if (state[Key.S])//move camera backwards
                raytracer.raytracerCamera.cameraLocation.Z -= 0.1f;

            if (state[Key.D])//move camera to the right
                raytracer.raytracerCamera.cameraLocation.X += 0.1f;

            if (state[Key.A])//move camera to the left
                raytracer.raytracerCamera.cameraLocation.X -= 0.1f;

            if (state[Key.Q])//move camera up
                raytracer.raytracerCamera.cameraLocation.Y += 0.1f;

            if (state[Key.E])//move camera down
                raytracer.raytracerCamera.cameraLocation.Y -= 0.1f;

            if (state[Key.I])//rotate camera up
                raytracer.raytracerCamera.rotateUp();

            if (state[Key.K])//rotate camera down
                raytracer.raytracerCamera.rotateDown();

            if (state[Key.L])//rotate camera right
                raytracer.raytracerCamera.rotateRight();

            if (state[Key.J])//rotate camera left
                raytracer.raytracerCamera.rotateLeft();

            if (state[Key.U])//increase the FoV
                raytracer.raytracerCamera.FieldOfView += 1.5f;

            if (state[Key.O])//decrease the FoV
                raytracer.raytracerCamera.FieldOfView -= 1.5f;

            if (state[Key.M])//increase the AA(note this follows the 2^X series)
                raytracer.alterAA(1);

            if (state[Key.N])//decrease the AA(note this follows the 2^X series) limited to 1 AA(essentialy turned off)
                raytracer.alterAA(-1);

            raytracer.raytracerCamera.UpdateScreen();//update the screen

            screen.Clear(0);
            raytracer.Render();
            msl = ms;
            ms = timer.ElapsedMilliseconds;
            double msSinceLast = ms - msl;
            double fps = 1000 / msSinceLast;
            screen.Print("fps " + fps, 2, 2, 0xffffff);
            screen.Print("Anti Ailasing " + raytracer.AA, 2, 20, 0xffffff);//print the AA and the fps values to the screen
        }
    }

} // namespace Template