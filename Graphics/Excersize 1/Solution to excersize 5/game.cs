using OpenTK.Input;
using System;
using System.IO;

namespace Template
{

    class Game
    {

        // member variables
        public Surface screen;
        public int screenUnit;
        public float a = 0, xpos = 0, ypos = 0, zoom = 1;
        // initialize
        public void Init()
        {

        }
        // tick: renders one frame
        public void Tick()
        {
            screenUnit = (int)(screen.height * 0.25);
            if (screenUnit > screen.width * 0.25)
            {
                screenUnit = (int)(screen.width * 0.25);
            }
            var state = Keyboard.GetState();
            if (state[Key.Up])
            {
                ypos++;
            }
            if (state[Key.Down])
            {
                ypos--;
            }
            if (state[Key.Left])
            {
                xpos--;
            }
            if (state[Key.Right])
            {
                xpos++;
            }
            if (state[Key.Z])
            {
                zoom -= 0.01f;
            }
            if (state[Key.X])
            {
                zoom += 0.01f;
            }
            a += 0.02f;
            screen.Clear(0);
            screen.Print("hello world, xpos = " + xpos + ", ypos = " + ypos + ", zoom = " + zoom, 2, 2, 0xffffff);
            /*int barheight = 256;
            int barwidth = 256;
            int barleft = screen.width / 2 - barwidth / 2;
            int bartop = screen.height / 2 - barheight / 2;
            int color;
 
            for (int i = 0; i < barwidth; i++)
            {
                for (int j = 0; j < barheight; j++)
                {
                    color = CreateColor(i, j, 0);
                    screen.Plot(barleft + i, bartop + j, color);
                }
            }*/

            /*screen.Print("1", TX(1), TY(0), CreateColor(255, 255, 255));
            screen.Print("-1", TX(-1), TY(0), CreateColor(255, 255, 255));
            screen.Print("1", TX(0), TY(1), CreateColor(255, 255, 255));
            screen.Print("-1", TX(0), TY(-1), CreateColor(255, 255, 255));
            screen.Print("2", TX(2), TY(0), CreateColor(255, 255, 255));
            screen.Print("-2", TX(-2), TY(0), CreateColor(255, 255, 255));
            screen.Print("2", TX(0), TY(2), CreateColor(255, 255, 255));*/
            //screen.Print("-2", TX(0), TY(-2), CreateColor(255, 255, 255));//this line chrases the program due to the remainder of the font being attempted to be draw outside the screen.

            // top-left corner
            float x1 = -1, y1 = 1.0f;
            float rx1 = (float)(x1 * Math.Cos(a) - y1 * Math.Sin(a));
            float ry1 = (float)(x1 * Math.Sin(a) + y1 * Math.Cos(a));
            // top-left corner
            float x2 = 1, y2 = 1.0f;
            float rx2 = (float)(x2 * Math.Cos(a) - y2 * Math.Sin(a));
            float ry2 = (float)(x2 * Math.Sin(a) + y2 * Math.Cos(a));
            // top-left corner
            float x3 = 1, y3 = -1.0f;
            float rx3 = (float)(x3 * Math.Cos(a) - y3 * Math.Sin(a));
            float ry3 = (float)(x3 * Math.Sin(a) + y3 * Math.Cos(a));
            // top-left corner
            float x4 = -1, y4 = -1.0f;
            float rx4 = (float)(x4 * Math.Cos(a) - y4 * Math.Sin(a));
            float ry4 = (float)(x4 * Math.Sin(a) + y4 * Math.Cos(a));
            screen.Line(TX(rx1), TY(ry1), TX(rx2), TY(ry2), 0xffffff);
            screen.Line(TX(rx2), TY(ry2), TX(rx3), TY(ry3), 0xffffff);
            screen.Line(TX(rx3), TY(ry3), TX(rx4), TY(ry4), 0xffffff);
            screen.Line(TX(rx4), TY(ry4), TX(rx1), TY(ry1), 0xffffff);


        }
        public int TX(float x)
        {
            int xcenter = (int)(screen.width * 0.5);//center of the graphs x axis
            int r = (int)(x * screenUnit * zoom + xcenter + xpos);
            screen.Line(xcenter + (int)xpos, 0, xcenter + (int)xpos, screen.height - 1, 0xffffff);//draws the x axis, this is for debugging purposes
            return r;
        }
        public int TY(float y)
        {
            int ycenter = (int)(screen.height * 0.5);//center of the graphs y axis
            screen.Line(0, ycenter + (int)-ypos, screen.width - 1, ycenter + (int)-ypos, 0xffffff);//draws the y axis, this is for debugging purposes
            int r = (int)(-1 * y * screenUnit * zoom + ycenter + -ypos);
            return r;
        }
        public int CreateColor(int red, int green, int blue) //Lets just keep this function here. Seems usefull
        {
            return (red << 16) + (green << 8) + blue;
        }
    }
} // namespace Template