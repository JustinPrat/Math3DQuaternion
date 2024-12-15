using System.Numerics;
using Maths_Matrices.Tests;

namespace Math3DUnit;

public class Vector4
{
    private float _x, _y, _z, _w;

    public float X => _x;
    public float Y => _y;
    public float Z => _z;
    public float W => _w;
    
    public Vector4(float x, float y, float z, float w)
    {
        _x = x;
        _y = y;
        _z = z;
        _w = w;
    }
    
    public static Vector4 operator *(MatrixFloat _matrix, Vector4 factor)
    {
        MatrixFloat newMatrix = _matrix.Multiply(new MatrixFloat(new[,]
        {
            { factor._x },
            { factor._y },
            { factor._z },
            { factor._w }
        }));
        
        return new Vector4(newMatrix[0, 0], newMatrix[1, 0], newMatrix[2, 0], newMatrix[3, 0]);
    }
}

public static class Extensions
{
    public static Vector3 Multiply(this MatrixFloat _matrix, Vector3 factor)
    {
        MatrixFloat newMatrix = _matrix.Multiply(new MatrixFloat(new[,]
        {
            { factor.X },
            { factor.Y },
            { factor.Z },
            { 1 }
        }));
        
        return new Vector3(newMatrix[0, 0], newMatrix[1, 0], newMatrix[2, 0]);
    }
}