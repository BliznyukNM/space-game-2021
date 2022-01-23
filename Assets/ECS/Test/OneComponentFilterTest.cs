using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace NB.ECS.Test
{
    public class OneComponentFilterTest
    {
        private struct ComponentA {}

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
        public void OneComponentWithAllTest()
        {
            var filter = world.WithAll<ComponentA>();
            var count = 0;

            foreach (var entity in filter)
            {
                count++;
            }

            Assert.AreEqual(1, count);
        }

        [Test]
        public void OneComponentWithAnyTest()
        {
            var filter = world.WithAny<ComponentA>();
            var count = 0;

            foreach (var entity in filter)
            {
                count++;
            }

            Assert.AreEqual(1, count);
        }
    }
}
