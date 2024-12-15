using System.Numerics;
using Maths_Matrices.Tests;

namespace Math3DUnit;

public class Transform
{
    public MatrixFloat LocalTranslationMatrix { get; set; }
    public MatrixFloat LocalRotationXMatrix { get; set; }
    public MatrixFloat LocalRotationYMatrix { get; set; }
    public MatrixFloat LocalRotationZMatrix { get; set; }
    public MatrixFloat LocalRotationMatrix { get; set; }
    public MatrixFloat LocalScaleMatrix { get; set; }
    public MatrixFloat LocalToWorldMatrix { get; set; }
    public MatrixFloat WorldToLocalMatrix { get; set; }
    public Transform Parent { get; set; }

    private Quaternion _localRotationQuaternion;
    
    public Quaternion LocalRotationQuaternion
    {
        get { return _localRotationQuaternion; }
        set
        {
            _localRotationQuaternion = value;
            LocalRotation = _localRotationQuaternion.EulerAngles;
        }
    }

    public Vector3 WorldPosition
    {
        get
        {
            return new Vector3(LocalToWorldMatrix[0, LocalToWorldMatrix.NbColumns - 1],
                LocalToWorldMatrix[1, LocalToWorldMatrix.NbColumns - 1],
                LocalToWorldMatrix[2, LocalToWorldMatrix.NbColumns - 1]);
        }
        set
        {
            Vector3 oldWorldPosition = new Vector3(LocalToWorldMatrix[0, LocalToWorldMatrix.NbColumns - 1],
                LocalToWorldMatrix[1, LocalToWorldMatrix.NbColumns - 1],
                LocalToWorldMatrix[2, LocalToWorldMatrix.NbColumns - 1]);
            
            LocalPosition += (value - oldWorldPosition);
        }
    }

    private Vector3 _localRotation;
    private Vector3 _localScale;
    private Vector3 _localPosition;
    
    public Vector3 LocalPosition
    {
        get
        {
            return _localPosition; //new Vector3(LocalTranslationMatrix[0, LocalTranslationMatrix.NbColumns - 1], LocalTranslationMatrix[1, LocalTranslationMatrix.NbColumns - 1], LocalTranslationMatrix[2, LocalTranslationMatrix.NbColumns - 1]);
        }
        set
        {
            _localPosition = value;
            LocalTranslationMatrix[0, LocalTranslationMatrix.NbColumns - 1] = _localPosition.X;
            LocalTranslationMatrix[1, LocalTranslationMatrix.NbColumns - 1] = _localPosition.Y;
            LocalTranslationMatrix[2, LocalTranslationMatrix.NbColumns - 1] = _localPosition.Z;
            
            if (Parent != null)
            {
                _localPosition = Parent.LocalRotationMatrix.InvertByDeterminant().Multiply(_localPosition);
                _localPosition = Parent.LocalScaleMatrix.InvertByDeterminant().Multiply(_localPosition);
            }
            GenerateWorldAndLocalMatrix();
        }
    }
    
    public Vector3 LocalRotation
    {
        get
        {
            return _localRotation;
        }
        set
        {
            LocalRotationXMatrix = MatrixFloat.RotationMatrix_X(value.X);
            LocalRotationYMatrix = MatrixFloat.RotationMatrix_Y(value.Y);
            LocalRotationZMatrix = MatrixFloat.RotationMatrix_Z(value.Z);
            
            LocalRotationMatrix = LocalRotationYMatrix * LocalRotationXMatrix * LocalRotationZMatrix;
            GenerateWorldAndLocalMatrix();
            _localRotation = value;
            _localRotationQuaternion = Quaternion.Euler(_localRotation.X, _localRotation.Y, _localRotation.Z);
        }
    }

    public Vector3 LocalScale
    {
        get
        {
            return _localScale;
        }
        set
        {
            LocalScaleMatrix = MatrixFloat.GetScaleMatrix(value.X, value.Y, value.Z);
            GenerateWorldAndLocalMatrix();
            _localScale = value;
        }
    }
    
    public Transform()
    {
        LocalTranslationMatrix = MatrixFloat.Identity(4);
        LocalRotationXMatrix = MatrixFloat.Identity(4);
        LocalRotationYMatrix = MatrixFloat.Identity(4);
        LocalRotationZMatrix = MatrixFloat.Identity(4);
        LocalRotationMatrix = MatrixFloat.Identity(4);
        LocalScaleMatrix = MatrixFloat.Identity(4);
        LocalToWorldMatrix = MatrixFloat.Identity(4);
        WorldToLocalMatrix = MatrixFloat.Identity(4);
        
        _localPosition = Vector3.Zero;
        _localScale = Vector3.One;
        _localRotation = Vector3.Zero;
        LocalRotationQuaternion = new Quaternion();
    }

    private void GenerateWorldAndLocalMatrix()
    {
        LocalToWorldMatrix = (LocalTranslationMatrix * LocalRotationMatrix * LocalScaleMatrix);
        if (Parent != null)
        {
            LocalToWorldMatrix *= Parent.LocalToWorldMatrix;
        }
        
        WorldToLocalMatrix = LocalToWorldMatrix.InvertByDeterminant();
    }

    public void SetParent(Transform parent)
    {
        Parent = parent;
        LocalToWorldMatrix = parent.LocalToWorldMatrix * LocalToWorldMatrix;
    }
}