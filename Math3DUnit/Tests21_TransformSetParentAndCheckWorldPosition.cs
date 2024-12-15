using System.Numerics;
using Math3DUnit;
using NUnit.Framework;

namespace Maths_Matrices.Tests
{
    [TestFixture]
    public class Tests21_TransformSetParentAndCheckWorldPosition
    {
        [Test, DefaultFloatingPointTolerance(0.001f)]
        public void TestParentChangePosition()
        {
            Transform tParent = new Transform();
            tParent.LocalPosition = new Vector3(10f, 5f, 2f);

            Transform tChild = new Transform();
            tChild.SetParent(tParent);
        
            Assert.AreEqual(new[,]
            {
                { 1f, 0f, 0f, 10f },
                { 0f, 1f, 0f, 5f },
                { 0f, 0f, 1f, 2f },
                { 0f, 0f, 0f, 1f },
            }, tParent.LocalToWorldMatrix.ToArray2D());

            Assert.AreEqual(10f, tParent.WorldPosition.X);
            Assert.AreEqual(5f, tParent.WorldPosition.Y);
            Assert.AreEqual(2f, tParent.WorldPosition.Z);
            
            Assert.AreEqual(new[,]
            {
                { 1f, 0f, 0f, 10f },
                { 0f, 1f, 0f, 5f },
                { 0f, 0f, 1f, 2f },
                { 0f, 0f, 0f, 1f },
            }, tChild.LocalToWorldMatrix.ToArray2D());

            Assert.AreEqual(10f, tChild.WorldPosition.X);
            Assert.AreEqual(5f, tChild.WorldPosition.Y);
            Assert.AreEqual(2f, tChild.WorldPosition.Z);
        }
        
        [Test, DefaultFloatingPointTolerance(0.001f)]
        public void TestDoubleParentChangePosition()
        {
            //tRoot (10,5,2)
            //  -> tParent (1,4,42) => (11,9,44)
            //      -> tChild (1,2,3) => (12,11,47)
            
            Transform tRoot = new Transform();
            tRoot.LocalPosition = new Vector3(10f, 5f, 2f);

            Transform tParent = new Transform();
            tParent.LocalPosition = new Vector3(1f, 4f, 42f);
            tParent.SetParent(tRoot);

            Transform tChild = new Transform();
            tChild.LocalPosition = new Vector3(-1f, 2f, 3f);
            tChild.SetParent(tParent);
            
            //Check tParent Matrix and World Position
            Assert.AreEqual(new[,]
            {
                { 1f, 0f, 0f, 11f },
                { 0f, 1f, 0f, 9f },
                { 0f, 0f, 1f, 44f },
                { 0f, 0f, 0f, 1f },
            }, tParent.LocalToWorldMatrix.ToArray2D());
            
            Assert.AreEqual(11f, tParent.WorldPosition.X);
            Assert.AreEqual(9f, tParent.WorldPosition.Y);
            Assert.AreEqual(44f, tParent.WorldPosition.Z);
        
            //Check tChild Matrix and World Position
            Assert.AreEqual(new[,]
            {
                { 1f, 0f, 0f, 10f },
                { 0f, 1f, 0f, 11f },
                { 0f, 0f, 1f, 47f },
                { 0f, 0f, 0f, 1f },
            }, tChild.LocalToWorldMatrix.ToArray2D());
            
            Assert.AreEqual(10f, tChild.WorldPosition.X);
            Assert.AreEqual(11f, tChild.WorldPosition.Y);
            Assert.AreEqual(47f, tChild.WorldPosition.Z);
        }

        [Test, DefaultFloatingPointTolerance(0.001f)]
        public void TestParentChangePositionAndRotation()
        {
            Transform tParent = new Transform();
            tParent.LocalPosition = new Vector3(10f, 5f, 2f);
            tParent.LocalRotation = new Vector3(0f, 0f, 45f);

            Transform tChild = new Transform();
            tChild.LocalPosition = new Vector3(1f, 0f, 0f);
            tChild.SetParent(tParent);
        
            Assert.AreEqual(new[,]
            {
                { 0.707f, -0.707f, 0.000f, 10.707f },
                { 0.707f, 0.707f, 0.000f, 5.707f },
                { 0.000f, 0.000f, 1.000f, 2.000f },
                { 0.000f, 0.000f, 0.000f, 1.000f },
            }, tChild.LocalToWorldMatrix.ToArray2D());

            Assert.AreEqual(10.707f, tChild.WorldPosition.X);
            Assert.AreEqual(5.707f, tChild.WorldPosition.Y);
            Assert.AreEqual(2f, tChild.WorldPosition.Z);
        }
        
        [Test, DefaultFloatingPointTolerance(0.001f)]
        public void TestParentChangePositionAndScale()
        {
            Transform tParent = new Transform();
            tParent.LocalPosition = new Vector3(100f, 0f, -20f);
            tParent.LocalScale = new Vector3(2f, 4f, 6f);

            Transform tChild = new Transform();
            tChild.LocalPosition = new Vector3(1f, 1f, 1f);
            tChild.SetParent(tParent);
        
            Assert.AreEqual(new[,]
            {
                { 2f, 0f, 0f, 102f },
                { 0f, 4f, 0f, 4f },
                { 0f, 0f, 6f, -14f },
                { 0f, 0f, 0f, 1f },
            }, tChild.LocalToWorldMatrix.ToArray2D());

            Assert.AreEqual(102f, tChild.WorldPosition.X);
            Assert.AreEqual(4f, tChild.WorldPosition.Y);
            Assert.AreEqual(-14f, tChild.WorldPosition.Z);
        }
    }
}
