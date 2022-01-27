using NUnit.Framework;

namespace NB.ECS.Test
{
    public class OneComponentFilterTest: BaseTest
    {
        [Test]
        public void OneComponentWithTest()
        {
            CreateEntity<ComponentA>();
            CreateEntity<ComponentB>();
            var filter = world.With<ComponentA>();
            var count = GetObjectsCount(filter);
            Assert.AreEqual(1, count);
        }

        [Test]
        public void TwoComponentSequentialWithTest()
        {
            CreateEntity<ComponentA>();
            CreateEntity<ComponentB>();
            CreateEntity<ComponentA, ComponentB>();
            CreateEntity();
            var filter = world.With<ComponentA>().With<ComponentB>();
            var count = GetObjectsCount(filter);
            Assert.AreEqual(1, count);
        }

        [Test]
        public void OneComponentNotExistWithTest()
        {
            CreateEntity();
            CreateEntity<ComponentA>();
            var filter = world.With<ComponentB>();
            var count = GetObjectsCount(filter);
            Assert.AreEqual(0, count);
        }
    }
}
