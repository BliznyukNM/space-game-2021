using NUnit.Framework;

namespace NB.ECS.Test
{
    public abstract class BaseTest
    {
        protected struct ComponentA {}
        protected struct ComponentB {}
        protected struct ComponentC {}

        protected World world;

        [SetUp]
        public void Setup()
        {
            world = new World();
        }

        protected int GetObjectsCount(Filter filter)
        {
            var count = 0;
            foreach (var entity in filter)
            {
                count++;
            }
            return count;
        }

        protected int CreateEntity()
        {
            var e = world.CreateEntity();
            return e;
        }

        protected int CreateEntity<A>()
            where A: struct
        {
            var e = world.CreateEntity();
            e.AddComponent(world, new A());
            return e;
        }

        protected int CreateEntity<A, B>()
            where A: struct
            where B: struct
        {
            var e = world.CreateEntity();
            e.AddComponent(world, new A());
            e.AddComponent(world, new B());
            return e;
        }

        protected int CreateEntity<A, B, C>()
            where A: struct
            where B: struct
            where C: struct
        {
            var e = world.CreateEntity();
            e.AddComponent(world, new A());
            e.AddComponent(world, new B());
            e.AddComponent(world, new C());
            return e;
        }
    }
}
