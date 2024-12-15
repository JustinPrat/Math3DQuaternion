using System.Numerics;
using Maths_Matrices.Tests;

namespace Math3DUnit;

public struct Quaternion
{
    public float x { get; set; }
    public float y { get; set; }
    public float z { get; set; }
    public float w { get; set; }

    public MatrixFloat Matrix
    {
        get
        {
            MatrixFloat matrix = MatrixFloat.Identity(4);
            // Première ligne
            matrix[0, 0] = 1 - 2 * (y * y + z * z);
            matrix[0, 1] = 2 * (x * y - w * z);
            matrix[0, 2] = 2 * (x * z + w * y);

            // Deuxième ligne
            matrix[1, 0] = 2 * (x * y + w * z);
            matrix[1, 1] = 1 - 2 * (x * x + z * z);
            matrix[1, 2] = 2 * (y * z - w * x);

            // Troisième ligne
            matrix[2, 0] = 2 * (x * z - w * y);
            matrix[2, 1] = 2 * (y * z + w * x);
            matrix[2, 2] = 1 - 2 * (x * x + y * y);

            return matrix;
        }
    }

    public Vector3 EulerAngles
    {
        get
        {
            //MathF.Asin(-2 * (y * z + w * x))
            //MathF.Atan2(2*x*z - 2*y*w, 1 - 2*(x*x) - 2*(y*y))
            //MathF.Atan2(x * y - w * z, 1 / 2 - (x * x) - (z * z))
            MatrixFloat matrix = Matrix;
            
            float AngleX = MathF.Asin(-matrix[1, 2]);
            AngleX *= 180 / MathF.PI;
            
            float AngleY = MathF.Atan2(matrix[0, 2], matrix[2, 2]);
            AngleY *= 180 / MathF.PI;

            float AngleZ = MathF.Atan2(matrix[1, 0], matrix[1, 1]); 
            AngleZ *= 180 / MathF.PI;
            
            return new Vector3(AngleX, AngleY, AngleZ);
        }
    }

    public static Quaternion Euler(float x, float y, float z)
    {
        Quaternion xRotation = Quaternion.AngleAxis(x, Vector3.UnitX);
        Quaternion yRotation = Quaternion.AngleAxis(y, Vector3.UnitY);
        Quaternion zRotation = Quaternion.AngleAxis(z, Vector3.UnitZ);
        return yRotation * xRotation * zRotation;
    }

    public static Quaternion Identity
    {
        get => new Quaternion(0, 0, 0, 1);
    }


    public static Quaternion operator *(Quaternion quaternionA, Quaternion quaternionB)
    {
        Quaternion result = Quaternion.Identity;
        result.w = quaternionA.w * quaternionB.w - quaternionA.x * quaternionB.x - quaternionA.y * quaternionB.y - quaternionA.z * quaternionB.z;
        result.x = quaternionA.w * quaternionB.x + quaternionA.x * quaternionB.w + quaternionA.y * quaternionB.z - quaternionA.z * quaternionB.y;
        result.y = quaternionA.w * quaternionB.y - quaternionA.x * quaternionB.z + quaternionA.y * quaternionB.w + quaternionA.z * quaternionB.x;
        result.z = quaternionA.w * quaternionB.z + quaternionA.x * quaternionB.y - quaternionA.y * quaternionB.x + quaternionA.z * quaternionB.w;
        return result;
    }
    
    public static Vector3 operator *(Quaternion quaternion, Vector3 vector)
    {
        Quaternion vectorQuaternion = new Quaternion(vector.X, vector.Y, vector.Z, 0);
        Quaternion result = quaternion * vectorQuaternion;
        result *= quaternion.Conjugate();
        
        return new Vector3(result.x, result.y, result.z);
    }

    public Quaternion Conjugate()
    {
        return new Quaternion(x * -1, y * -1, z * -1, w);
    }

    public static Quaternion AngleAxis(float angle, Vector3 axis)
    {
        axis = Vector3.Normalize(axis);
        float cosAngle = MathF.Cos((angle * (MathF.PI/180)) / 2);
        float sinAngle = MathF.Sin((angle * (MathF.PI/180)) / 2);
        
        float x = axis.X * sinAngle;
        float y = axis.Y * sinAngle;
        float z = axis.Z * sinAngle;
        float w = cosAngle;
        return new Quaternion(x, y, z, w);
    }

    public Quaternion(float x, float y, float z, float w)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.w = w;
    }
    
    public Quaternion()
    {
        this.x = 0;
        this.y = 0;
        this.z = 0;
        this.w = 1;
    }
}