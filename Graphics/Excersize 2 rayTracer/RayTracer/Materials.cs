using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace template
{
    public abstract class Materials
    {
        public float absorbtion;
        public bool reflective;
    }

    class Mat : Materials
    {
        public Mat(float abs, bool Reflective)
        {
            absorbtion = abs;
            reflective = Reflective;
        }
    }
}
