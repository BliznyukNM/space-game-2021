using NUnit.Framework;

namespace NB.ECS.Test
{
    public class TwoComponentFilterTest: BaseTest
    {
        [Test]
        public void TwoComponentWithAllTest()
        {
            CreateEntity<ComponentA>();
            CreateEntity<ComponentA, ComponentB>();
            CreateEntity<ComponentB, ComponentC>();
            var filter = world.WithAll<ComponentA, ComponentB>();
            var count = GetObjectsCount(filter);
            Assert.AreEqual(1, count);
        }

        [Test]
        public void TwoComponentWithAnyTest()
        {
            CreateEntity<ComponentC>();
            CreateEntity<ComponentA>();
            CreateEntity<ComponentA, ComponentB>();
            CreateEntity();
            CreateEntity<ComponentB, ComponentC>();
            var filter = world.WithAny<ComponentA, ComponentB>();
            var count = GetObjectsCount(filter);
            Assert.AreEqual(3, count);
        }

        [Test]
        public void TwoComponentsSequentialWithAllWithAnyTest()
        {
            CreateEntity<ComponentA>();
            CreateEntity<ComponentA, ComponentB>();
            CreateEntity<ComponentA, ComponentB, ComponentD>();
            CreateEntity<ComponentB, ComponentC, ComponentD>();
            CreateEntity<ComponentB, ComponentC, ComponentA>();
            CreateEntity<ComponentD, ComponentC, ComponentA>();
            var filter = world.WithAll<ComponentA, ComponentB>().WithAny<ComponentC, ComponentD>();
            var count = GetObjectsCount(filter);
            Assert.AreEqual(2, count);
        }
    }
}
