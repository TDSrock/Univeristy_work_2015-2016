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
    public class Camera
    {
        //Variables
        //public float cameraLocationX, cameraLocationY, cameraLocationZ; These valyes are within the vector3's
        //public float cameraDirectionX, cameraDirectionY, cameraDirectionZ; These valyes are within the vector3's
        //public float cameraToScreenDistance;
        public float FieldOfView;
        private float fieldOfView, targetDistance;
        private double phi, theta;
        public Vector3 cameraLocation, targetLocation, viewDirection, left, right, up, down, cameraC, screen00, screen10, screen01;

        private float aspectRatio;

        public Camera(float clx, float cly, float clz, float cdx, float cdy, float cdz, float fov, float width, float height)
        {
            phi = Math.PI / 2;//This defaults the camera's orentation forwards
            theta = Math.PI / 2;//this defaults the camera's orentation forwards Note that the all the CD values are still required to create the radius of the target...
            FieldOfView = fov;
            cameraLocation = new Vector3(clx, cly, clz);
            targetLocation = new Vector3(cdx, cdy, cdz);
            targetDistance = Vector3.Add(cameraLocation, -targetLocation).Length;
            aspectRatio = height / width;
            UpdateScreen();
        }
        public void rotateRight()
        {
            phi -= 0.2;
            if (phi < 0)
            {
                phi += Math.PI * 2;
            }
        }
        public void rotateLeft()
        {
            phi += 0.2;
            if (phi > Math.PI * 2)
            {
                phi -= Math.PI * 2;
            }
        }
        public void rotateUp()
        {
            theta -= 0.2;
            if (theta < 0)
            {
                theta += Math.PI * 2;
            }
        }
        public void rotateDown()
        {
            theta += 0.2;
            if (theta > Math.PI * 2)
            {
                theta -= Math.PI * 2;
            }
        }
        //for all rotations. I have gauranteed that theta and phi always stay between 0 and 2 to ensure acuracy and avoid large number problems.

        private void UpdateScreenLocation()//Based on theta and phi alter the location of the target to properly adjust acording to the inputs
        {
            targetLocation.X = cameraLocation.X + targetDistance * (float)(Math.Cos(phi) * Math.Sin(theta));
            targetLocation.Y = cameraLocation.Y + targetDistance * (float)Math.Cos(theta);
            targetLocation.Z = cameraLocation.Z + targetDistance * (float)(Math.Sin(phi) * Math.Sin(theta));
        }

        public void UpdateScreen()
        {

            FieldOfView = MathHelper.Clamp(FieldOfView, 1, 179);
            fieldOfView = (FieldOfView * 0.5f) * ((float)(Math.PI / 180));
            float fovA = 1f / ((float)Math.Tan((fieldOfView)));
            viewDirection = (targetLocation - cameraLocation);

            viewDirection.Normalize();
            cameraC = cameraLocation + viewDirection * fovA;

            left = new Vector3(-1, 0, 0);
            right = new Vector3(1, 0, 0);
            up = new Vector3(0, 1, 0);
            down = new Vector3(0, -1, 0);

            left = Vector3.Cross(viewDirection, up);
            up = Vector3.Cross(viewDirection, left);
            right = Vector3.Cross(viewDirection, down);
            down = Vector3.Cross(viewDirection, right);

            screen00 = cameraC + left + up * aspectRatio;
            screen10 = cameraC + right + up * aspectRatio;
            screen01 = cameraC + left + down * aspectRatio;

            Vector3 Screencenter = new Vector3(cameraLocation.X, cameraLocation.Y, cameraLocation.Z) + (fovA * new Vector3(viewDirection.X, viewDirection.Y, viewDirection.Z));
            UpdateScreenLocation();
            //calculate 00,10,01 of the raytracer screen(11 is implicit)
        }
    }
}
