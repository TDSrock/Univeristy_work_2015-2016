using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace template
{
    public abstract class Lights
    {
        public Vector3 lightLocation, lightRGB;

    }

    public class BaseLight : Lights
    {

        public BaseLight (Vector3 loc, float r, float g, float b)
        {
            lightRGB = new Vector3(r, g, b);

            lightLocation = new Vector3(loc.X, loc.Y, loc.Z);
         }

    }
}
