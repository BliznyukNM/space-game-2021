using NUnit.Framework;

namespace NB.ECS.Test
{
    public class SystemTest
    {
        private class TestSystem : IStartSystem, IUpdateSystem, IStopSystem
        {
            public void OnStart(World world)
            {
                Assert.Pass();
            }

            public void OnUpdate(World world)
            {
                Assert.Pass();
                /*
                var filter = world.WithAll<A, B, C>().WithAny<D>().WithNone<E>();
                for (var entity: filter)
                {
                    ref var b = ref entity.GetComponent<B>(world);
                    b.data++;
                }
                */
            }

            public void OnStop(World world)
            {
                Assert.Pass();
            }
        }

        private World world;

        [SetUp]
        public void CreateWorld()
        {
            world = new World();
        }

        [Test]
        public void StartSystemsTest()
        {
            var systems = new Systems(world)
                .Register(new TestSystem());
            systems.Start();
        }

        [Test]
        public void UpdateSystemsTest()
        {
            var systems = new Systems(world)
                .Register(new TestSystem());
            systems.Update();
        }

        [Test]
        public void StopSystemsTest()
        {
            var systems = new Systems(world)
                .Register(new TestSystem());
            systems.Stop();
        }
    }
}
