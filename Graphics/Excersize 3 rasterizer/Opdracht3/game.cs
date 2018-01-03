using System.Diagnostics;
using OpenTK;
using System;
using OpenTK.Input;

// minimal OpenTK rendering framework for UU/INFOGR
// Jacco Bikker, 2016

namespace Template_P3 {

    public class Game
    {
	    // member variables
	    public Surface screen;					// background surface for printing etc.
	    const float PI = 3.1415926535f;			// PI
	    float a = 0;							// teapot rotation angle
	    Stopwatch timer;                        // timer for measuring frame duration
        Shader shader;                           // shader to use for rendering
        Shader postproc;                        // shader to use for post processing
        Texture wood;                           // texture to use for rendering
        SceneGraph scene;
            float Camx = 0;                     // the starting input for camera and car
            float Camy = 0;
            float Camz = 0;

            float Carx = 0;
            float Carz = 0;
            float Carrotation = PI / 2;
            bool carcamlock = false, cPressed = false;
        float updown = 0;
        float leftright = 0;
        MouseState current, previous;
        Matrix4 oldtranslation = Matrix4.CreateTranslation(0, -40, -500); //camera starting location
        public static Matrix4 translation;             //The camera Matrix
        RenderTarget target;                    // intermediate render target
        ScreenQuad quad;                        // screen filling quad for post processing
        bool useRenderTarget = true;
        int tick = 0;

        // initialize
        public void Init()
	    {
                // create scene
                scene = new SceneGraph();
        
		    // initialize stopwatch
		    timer = new Stopwatch();
		    timer.Reset();
		    timer.Start();

            // create shaders
            shader = new Shader("../../shaders/vs.glsl", "../../shaders/fs.glsl");
            postproc = new Shader("../../shaders/vs_post.glsl", "../../shaders/fs_post.glsl");
            
            // create the render target
            target = new RenderTarget(screen.width, screen.height);
            quad = new ScreenQuad();

            //the starting translations for the car and its exhaust
            scene.childMeshes[1].childMeshes[0].childMeshes[0].modelMatrix *= Matrix4.CreateFromAxisAngle(new Vector3(0, 1, 0), -PI / 2);

            scene.childMeshes[1].childMeshes[0].modelMatrix *= Matrix4.CreateFromAxisAngle(new Vector3(0, 1, 0), -(PI / 2));
            scene.childMeshes[1].childMeshes[0].modelMatrix *= 0.01f;

            //the grid of light
            for(int x = -100; x < 101; x++)
            {
                for(int z = 0; z < 201; z++)
                {
                    if(x % 10 == 0 && z % 10 == 0)
                    {
                        if (x % 100 == 0 && z % 100 == 0)
                        {
                            scene.AddLight(new Light(new Vector3((float)(x * 10), -10f, (float)(z * -10)), 100f, new Vector4(5f, 0.3f, 0.3f, 0.5f), new Vector4(1f, 1f, 1f, 1.0f)));
                        }
                        else
                        {
                            scene.AddLight(new Light(new Vector3((float)(x * 10), -10f, (float)(z * -10)), 100f, new Vector4(5f, 0.3f, 0.3f, 0.5f), new Vector4(1f, 1f, 1f, 1.0f)));
                        }
                    }
                }
            }
        }

	    // tick for background surface
	    public void Tick()
	    {
		    screen.Clear( 0 );
            screen.Print("Press C to lock camara to car", 2, 2, 0xffff00);

            // the wave of light
            tick++;
            if (tick % 10 == 0 && tick < 2001)
            {
                if (tick % 100 == 0)
                {

                    for (int i = -1000; i < 1001; i += 175)
                    {

                        if (tick % 100 == 0)
                        {
                            if (tick % 1000 == 0)
                            {
                                scene.AddLight(new Light(new Vector3((float)i, -10f, -(float)tick), 500f, new Vector4(1f, 1f, 1f, 0.5f), new Vector4(1f, 1f, 1f, 1.0f)));
                            }
                            else
                            {
                                scene.AddLight(new Light(new Vector3((float)i, -10f, -(float)tick), 500f, new Vector4(1f, 1f, 1f, 0.5f), new Vector4(1f, 1f, 1f, 1.0f)));
                            }
                        }
                        scene.AddLight(new Light(new Vector3((float)i, -10f, -(float)tick), 500f, new Vector4(1f, 1f, 1f, 0.5f), new Vector4(1f, 1f, 1f, 1.0f)));
                    }
                }
                else
                {
                    scene.AddLight(new Light(new Vector3(0f, -10f, -(float)tick), 500f, new Vector4(1f, 1f, 1f, 0.5f), new Vector4(1f, 1f, 1f, 1.0f)));
                }
             }
	    }

	    // tick for OpenGL rendering code
	    public void RenderGL()
	    {
		    // measure frame duration
		    float frameDuration = timer.ElapsedMilliseconds;
		    timer.Reset();
		    timer.Start();
            
		    // update rotation of the teapot
            scene.childMeshes[1].childMeshes[0].childMeshes[1].childMeshes[0].distanceFromParent = Vector3.TransformVector(scene.childMeshes[1].childMeshes[0].childMeshes[1].childMeshes[0].distanceFromParent, Matrix4.CreateFromAxisAngle(new Vector3(0, 1, 0), a));
            a = 0.01f;

		    // render scene
            CheckMovement();
            Matrix4 translation = Matrix4.CreateTranslation(Camx, Camy, Camz);
            Vector3 eye = new Vector3(Camx,Camy,Camz);
            Vector3 Target = new Vector3(Camx+0,Camy+0,Camz+-1);
            Vector3 UP = new Vector3(0,1,0);
            Matrix4 lookat =Matrix4.LookAt(eye, Target, UP);
            translation = lookat;
 
            //the camera movement
            Matrix4 movement = Matrix4.CreateTranslation(-Camx, -Camy, -Camz);
            movement *= Matrix4.CreateFromAxisAngle(new Vector3(0, 1, 0), leftright);

            translation = oldtranslation * movement;
            oldtranslation = translation;

            //the car movement
            Matrix4 carMovement = Matrix4.CreateTranslation(0, 0, -Carz);
            carMovement *= Matrix4.CreateFromAxisAngle(new Vector3(0, 1, 0), Carrotation);

            scene.childMeshes[1].childMeshes[0].distanceFromParent += Vector3.Transform(Vector3.Zero, carMovement);
            scene.childMeshes[1].childMeshes[0].modelMatrix *= Matrix4.CreateFromAxisAngle(new Vector3(0, 1, 0), Carx);

            
            translation *= Matrix4.CreateFromAxisAngle(new Vector3(1, 0, 0), updown);
            translation *= Matrix4.CreatePerspectiveFieldOfView(1.2f, 1.3f, .1f, 1000);

            //if you press C to lock the camera
            if (carcamlock)
            {
                carMovement = Matrix4.CreateTranslation(0, 0, -20);
                carMovement *= Matrix4.CreateFromAxisAngle(new Vector3(0, 1, 0), Carrotation);

                eye = scene.childMeshes[1].childMeshes[0].distanceFromParent + Vector3.Transform(Vector3.Zero, carMovement);
                eye.Y += 10;


                translation = lookat = Matrix4.LookAt(eye, scene.childMeshes[1].childMeshes[0].distanceFromParent, UP);
                translation *= Matrix4.CreatePerspectiveFieldOfView(1.2f, 1.3f, .1f, 1000);
            }

            if (useRenderTarget)
            {
                // enable render target
                target.Bind();

                // render scene to render target
                scene.Render(translation, shader, wood, Target);

                // render quad
                target.Unbind();
                quad.Render(postproc, target.GetTextureID());
            }
            else
            {
                // render scene directly to the screen
                scene.Render(translation, shader, wood, Target);
                quad.Render(postproc, target.GetTextureID());
            }

            screen.Print("hello world", 2, 2, 0xffff00);
        }

        public void CheckMovement() //the methode to update de input
        {
            var keyboard = OpenTK.Input.Keyboard.GetState();
            var mouse = OpenTK.Input.Mouse.GetState();
            Carx = 0;
            Carz = 0;

            int xdelta, ydelta, zdelta;
            xdelta = 0; ydelta = 0;

            current = Mouse.GetState();
            if (current != previous)
            {
                // Mouse state has changed
                xdelta = current.X - previous.X;
                ydelta = current.Y - previous.Y;
                zdelta = current.Wheel - previous.Wheel;
            }
            previous = current;

            //camera movement
            Camx = 0;
            Camy = 0;
            Camz = 0;
            if (keyboard[OpenTK.Input.Key.Space])
            {
                Camy = 0.5f;
            }
            if (keyboard[OpenTK.Input.Key.ShiftLeft])
            {
                Camy = -0.5f;
            }
            if (keyboard[OpenTK.Input.Key.W])
            {
                Camz = -3;
            }
            if (keyboard[OpenTK.Input.Key.S])
            {
                Camz = 1;
            }
            if (keyboard[OpenTK.Input.Key.A])
            {
                Camx = -1;
            }
            if (keyboard[OpenTK.Input.Key.D])
            {
                Camx = 1;
            }
            //car movement
            if (keyboard[OpenTK.Input.Key.Up])
            {
                Carz = -2;
            }
            if (keyboard[OpenTK.Input.Key.Down])
            {
                Carz = 1;
            }
            if (keyboard[OpenTK.Input.Key.Left] && (keyboard[OpenTK.Input.Key.Up] || keyboard[OpenTK.Input.Key.Down]))
            {
                if (keyboard[OpenTK.Input.Key.Down])
                {
                    Carx = -0.04f;
                    Carrotation -= 0.04f;
                }
                else
                {
                    Carx = 0.04f;
                    Carrotation += 0.04f;
                }
            }
            if (keyboard[OpenTK.Input.Key.Right] && (keyboard[OpenTK.Input.Key.Up] || keyboard[OpenTK.Input.Key.Down]) && !keyboard[OpenTK.Input.Key.Left])
            {
                if (keyboard[OpenTK.Input.Key.Down])
                {
                    Carx = 0.04f;
                    Carrotation += 0.04f;
                }
                else
                {
                    Carx = -0.04f;
                    Carrotation += -0.04f;
                }
            }
            //the camera locking
            if (keyboard[OpenTK.Input.Key.C] && !carcamlock & !cPressed)
            {
                carcamlock = true;
                cPressed = true;
            }
            else
            {
                if (keyboard[OpenTK.Input.Key.C] && carcamlock & !cPressed)
                {
                    carcamlock = false;
                    cPressed = true;
                }
            }
            if (!keyboard[OpenTK.Input.Key.C])
            {
                cPressed = false;
            }


            leftright = 0;
            //updown = 0;
            leftright += (float)((float)xdelta / 256);
            updown += (float)((float)ydelta / 256);
        }

        public static Matrix4 _CameraMatrix
        {
            get
            {
                return translation;
            }
        }
    }

} // namespace Template_P3