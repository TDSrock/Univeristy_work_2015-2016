using System;
using System.Runtime.InteropServices;
using OpenTK;

namespace Template_P3
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    struct Light
    {
        Vector3 _position;
        float _intensity;
        Vector4 _colorSpec;
        Vector4 _colorDiff;

        public Light(Vector3 Position, float Intensity, Vector4 ColorSpec, Vector4 ColorDiff)
        {
            _position = Position;
            _intensity = Intensity;
            _colorSpec = ColorSpec;
            _colorDiff = ColorDiff;
        }
    }
}
