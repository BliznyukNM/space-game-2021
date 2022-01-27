using NUnit.Framework;

namespace NB.ECS.Test
{
    public class ThreeComponentFilterTest: BaseTest
    {
        [Test]
        public void ThreeComponentsWithAllTest()
        {
            CreateEntity();
            CreateEntity<ComponentA, ComponentB, ComponentC>();
            CreateEntity<ComponentA>();
            CreateEntity<ComponentB, ComponentC>();
            var filter = world.WithAll<ComponentA, ComponentB, ComponentC>();
            var count = GetObjectsCount(filter);
            Assert.AreEqual(1, count);
        }

        [Test]
        public void ThreeComponentsWithAnyTest()
        {
            CreateEntity();
            CreateEntity<ComponentA, ComponentB, ComponentC>();
            CreateEntity<ComponentA>();
            CreateEntity<ComponentB, ComponentC>();
            var filter = world.WithAny<ComponentA, ComponentB, ComponentC>();
            var count = GetObjectsCount(filter);
            Assert.AreEqual(3, count);
        }

        [Test]
        public void TwoComponentABWithAnyTest()
        {
            CreateEntity();
            CreateEntity<ComponentA, ComponentB, ComponentC>();
            CreateEntity<ComponentC>();
            CreateEntity<ComponentB, ComponentC>();
            var filter = world.WithAny<ComponentA, ComponentB>();
            var count = GetObjectsCount(filter);
            Assert.AreEqual(2, count);
        }

        [Test]
        public void TwoComponentABWithAllTest()
        {
            CreateEntity();
            CreateEntity<ComponentA, ComponentB, ComponentC>();
            CreateEntity<ComponentC>();
            CreateEntity<ComponentB, ComponentC>();
            var filter = world.WithAll<ComponentA, ComponentB>();
            var count = GetObjectsCount(filter);
            Assert.AreEqual(1, count);
        }
    }
}
