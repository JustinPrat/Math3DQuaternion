using System;
using Math3DUnit;
using NUnit.Framework;

namespace Maths_Matrices.Tests
{
    [TestFixture]
    public class Tests16_TransformationMatrices
    {
        [Test, DefaultFloatingPointTolerance(0.001f)]
        public void TestTranslatePoint()
        {
            Vector4 v = new Vector4(1f, 0f, 0f, 1f);
            MatrixFloat m = new MatrixFloat(new[,]
            {
                { 1f, 0f, 0f, 5f },
                { 0f, 1f, 0f, 3f },
                { 0f, 0f, 1f, 1f },
                { 0f, 0f, 0f, 1f },
            });

            Vector4 vTransformed = m * v;
            Assert.AreEqual(vTransformed.X, 6f);
            Assert.AreEqual(vTransformed.Y, 3f);
            Assert.AreEqual(vTransformed.Z, 1f);

            Vector4 vTransformedInverted = m.InvertByRowReduction() * vTransformed;
            Assert.AreEqual(1f, vTransformedInverted.X);
            Assert.AreEqual(0f, vTransformedInverted.Y);
            Assert.AreEqual(0f, vTransformedInverted.Z);

            vTransformedInverted = m.InvertByDeterminant() * vTransformed;
            Assert.AreEqual(1f, vTransformedInverted.X);
            Assert.AreEqual(0f, vTransformedInverted.Y);
            Assert.AreEqual(0f, vTransformedInverted.Z);
        }

        [Test, DefaultFloatingPointTolerance(0.001f)]
        public void TestTranslateDirection()
        {
            Vector4 v = new Vector4(1f, 0f, 0f, 0f);
            MatrixFloat m = new MatrixFloat(new[,]
            {
                { 1f, 0f, 0f, 5f },
                { 0f, 1f, 0f, 3f },
                { 0f, 0f, 1f, 1f },
                { 0f, 0f, 0f, 1f },
            });
            Vector4 vTransformed = m * v;

            Assert.AreEqual(1f, vTransformed.X);
            Assert.AreEqual(0f, vTransformed.Y);
            Assert.AreEqual(0f, vTransformed.Z);

            Vector4 vTransformedInverted = m.InvertByRowReduction() * vTransformed;
            Assert.AreEqual(1f, vTransformedInverted.X);
            Assert.AreEqual(0f, vTransformedInverted.Y);
            Assert.AreEqual(0f, vTransformedInverted.Z);

            vTransformedInverted = m.InvertByDeterminant() * vTransformed;
            Assert.AreEqual(1f, vTransformedInverted.X);
            Assert.AreEqual(0f, vTransformedInverted.Y);
            Assert.AreEqual(0f, vTransformedInverted.Z);
        }

        [Test, DefaultFloatingPointTolerance(0.001f)]
        public void TestScalePoint()
        {
            Vector4 v = new Vector4(2f, 1f, 3f, 1f);
            MatrixFloat m = new MatrixFloat(new[,]
            {
                { 0.5f, 0f, 0f, 0f },
                { 0.0f, 2f, 0f, 0f },
                { 0.0f, 0f, 3f, 0f },
                { 0.0f, 0f, 0f, 1f },
            });

            Vector4 vTransformed = m * v;
            Assert.AreEqual(1f, vTransformed.X);
            Assert.AreEqual(2f, vTransformed.Y);
            Assert.AreEqual(9f, vTransformed.Z);

            Vector4 vTransformedInverted = m.InvertByRowReduction() * vTransformed;
            Assert.AreEqual(2f, vTransformedInverted.X);
            Assert.AreEqual(1f, vTransformedInverted.Y);
            Assert.AreEqual(3f, vTransformedInverted.Z);

            vTransformedInverted = m.InvertByDeterminant() * vTransformed;
            Assert.AreEqual(2f, vTransformedInverted.X);
            Assert.AreEqual(1f, vTransformedInverted.Y);
            Assert.AreEqual(3f, vTransformedInverted.Z);
        }

        [Test, DefaultFloatingPointTolerance(0.001f)]
        public void TestRotatePoint()
        {
            Vector4 v = new Vector4(1f, 4f, 7f, 1f);
            double a = Math.PI / 2d;
            float cosA = (float)Math.Cos(a);
            float sinA = (float)Math.Sin(a);
            MatrixFloat m = new MatrixFloat(new[,]
            {
                { cosA, -sinA, 0f, 0f },
                { sinA, cosA, 0f, 0f },
                { 0f, 0f, 1f, 0f },
                { 0f, 0f, 0f, 1f },
            });

            Vector4 vTransformed = m * v;
            Assert.AreEqual(-4f, vTransformed.X);
            Assert.AreEqual(1f, vTransformed.Y);
            Assert.AreEqual(7f, vTransformed.Z);
            
            Vector4 vTransformedInverted = m.InvertByRowReduction() * vTransformed;
            Assert.AreEqual(1f, vTransformedInverted.X);
            Assert.AreEqual(-4f, vTransformedInverted.Y);
            Assert.AreEqual(7f, vTransformedInverted.Z);

            vTransformedInverted = m.InvertByDeterminant() * vTransformed;
            Assert.AreEqual(1f, vTransformedInverted.X);
            Assert.AreEqual(4f, vTransformedInverted.Y);
            Assert.AreEqual(7f, vTransformedInverted.Z);
        }
    }
}