using NUnit.Framework;

namespace NB.ECS.Test
{
    public class ComponentTest: BaseTest
    {
        private struct TestComponent
        {
            public int a;
            public string b;
        }
        
        [Test]
        public void AddComponentTest()
        {
            var entity = world.CreateEntity();
            entity.AddComponent(world, new TestComponent { a = 10, b = "test" });
            var result = entity.HasComponent<TestComponent>(world);
            Assert.True(result);
        }

        [Test]
        public void RemoveComponentTest()
        {
            var entity = world.CreateEntity();
            entity.AddComponent(world, new TestComponent { a = 10, b = "test" });
            entity.RemoveComponent<TestComponent>(world);
            var result = entity.HasComponent<TestComponent>(world);
            Assert.False(result);
        }
    }
}
