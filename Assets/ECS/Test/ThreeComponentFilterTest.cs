using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace NB.ECS.Test
{
    public class ThreeComponentFilterTest
    {
        private struct ComponentA {}
        private struct ComponentB {}
        private struct ComponentC {}

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
            e4.AddComponent(world, new ComponentC());
            var e5 = world.CreateEntity();
            e5.AddComponent(world, new ComponentA());
            e5.AddComponent(world, new ComponentB());
            var e6 = world.CreateEntity();
            e6.AddComponent(world, new ComponentA());
            e6.AddComponent(world, new ComponentC());
            var e7 = world.CreateEntity();
            e7.AddComponent(world, new ComponentB());
            e7.AddComponent(world, new ComponentC());
            var e8 = world.CreateEntity();
            e8.AddComponent(world, new ComponentA());
            e8.AddComponent(world, new ComponentB());
            e8.AddComponent(world, new ComponentC());
        }

        [Test]
        public void ThreeComponentsWithAllTest()
        {
            var filter = world.WithAll<ComponentA, ComponentB, ComponentC>();
            var count = 0;

            foreach (var entity in filter)
            {
                count++;
            }

            Assert.AreEqual(1, count);
        }

        [Test]
        public void ThreeComponentsWithAnyTest()
        {
            var filter = world.WithAny<ComponentA, ComponentB, ComponentC>();
            var count = 0;

            foreach (var entity in filter)
            {
                count++;
            }

            Assert.AreEqual(7, count);
        }

        [Test]
        public void TwoComponentABWithAnyTest()
        {
            var filter = world.WithAny<ComponentA, ComponentB>();
            var count = 0;

            foreach (var entity in filter)
            {
                count++;
            }

            Assert.AreEqual(6, count);
        }

        [Test]
        public void TwoComponentABWithAllTest()
        {
            var filter = world.WithAll<ComponentA, ComponentB>();
            var count = 0;

            foreach (var entity in filter)
            {
                count++;
            }

            Assert.AreEqual(2, count);
        }
    }
}
