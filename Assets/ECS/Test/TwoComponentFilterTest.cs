using NUnit.Framework;

namespace NB.ECS.Test
{
    public class TwoComponentFilterTest
    {
        private struct ComponentA {}
        private struct ComponentB {}

        private World world;

        [SetUp]
        public void CreateWorld()
        {
            world = new World();

            var e1 = world.CreateEntity();
            var e2 = world.CreateEntity();
            e2.AddComponent(world, new ComponentA());
            var e3 = world.CreateEntity();
            e3.AddComponent(world, new ComponentB());
            var e4 = world.CreateEntity();
            e4.AddComponent(world, new ComponentA());
            e4.AddComponent(world, new ComponentB());
        }

        [Test]
        public void TwoComponentWithAllTest()
        {
            var filter = world.WithAll<ComponentA, ComponentB>();
            var count = 0;

            foreach (var entity in filter)
            {
                count++;
            }

            Assert.AreEqual(1, count);
        }

        [Test]
        public void TwoComponentWithAnyTest()
        {
            var filter = world.WithAny<ComponentA, ComponentB>();
            var count = 0;

            foreach (var entity in filter)
            {
                count++;
            }

            Assert.AreEqual(3, count);
        }

        [Test]
        public void OneComponentAWithTest()
        {
            var filter = world.With<ComponentA>();
            var count = 0;

            foreach (var entity in filter)
            {
                count++;
            }

            Assert.AreEqual(2, count);
        }
    }
}
