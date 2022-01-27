using NUnit.Framework;

namespace NB.ECS.Test
{
    public class EntityTest: BaseTest
    {
        [Test]
        public void CreateEntityTest() {
            var entity = world.CreateEntity();
            Assert.GreaterOrEqual(entity, 0);
        }

        [Test]
        public void DeleteEntityTest()
        {
            var entity = world.CreateEntity();
            var result = world.RemoveEntity(entity);
            Assert.True(result);
        }
    }
}
