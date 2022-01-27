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
            var componentId = entity.AddComponent(world, new TestComponent { a = 10, b = "test" });
            Assert.GreaterOrEqual(0, componentId);
        }

        [Test]
        public void HasComponentTest()
        {
            var entity = world.CreateEntity();
            entity.AddComponent(world, new TestComponent { a = 10, b = "test" });
            var result = entity.HasComponent<TestComponent>(world);
            Assert.True(result);
        }

        [Test]
        public void GetComponentTest()
        {
            var entity = world.CreateEntity();
            entity.AddComponent(world, new TestComponent { a = 10, b = "test" });
            var component = entity.GetComponent<TestComponent>(world);
            Assert.AreEqual(new TestComponent { a = 10, b = "test" }, component);
        }

        [Test]
        public void GetComponentWithoutComponentTest()
        {
            var entity = world.CreateEntity();
            Assert.Throws<System.InvalidOperationException>(() => entity.GetComponent<TestComponent>(world));
        }

        [Test]
        public void DoesntHaveComponentTest()
        {
            var entity = world.CreateEntity();
            var result = entity.HasComponent<TestComponent>(world);
            Assert.False(result);
        }

        [Test]
        public void RemoveComponentTest()
        {
            var entity = world.CreateEntity();
            entity.AddComponent(world, new TestComponent { a = 10, b = "test" });
            var result = entity.RemoveComponent<TestComponent>(world);
            Assert.True(result);
        }

        [Test]
        public void RemoveNotExistingComponentTest()
        {
            var entity = world.CreateEntity();
            var result = entity.RemoveComponent<TestComponent>(world);
            Assert.False(result);
        }
    }
}
