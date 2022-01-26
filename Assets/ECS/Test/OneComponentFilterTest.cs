using NUnit.Framework;

namespace NB.ECS.Test
{
    public class OneComponentFilterTest
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
        }

        [Test]
        public void OneComponentWithTest()
        {
            var filter = world.With<ComponentA>();
            var count = 0;

            foreach (var entity in filter)
            {
                count++;
            }

            Assert.AreEqual(1, count);
        }

        [Test]
        public void OneComponentNotExistWithTest()
        {
            var filter = world.With<ComponentB>();
            var count = 0;

            foreach (var entity in filter)
            {
                count++;
            }

            Assert.AreEqual(0, count);
        }
    }
}
