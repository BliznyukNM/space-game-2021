using NUnit.Framework;
using UnityEngine;

namespace NB.SpaceGame.Test
{
    public class CoordinatesTest
    {
        [Test]
        public void KilometersToVector2Test()
        {
            var km = new Kilometers(100, -3);
            var res = km.ToVector2();
            Assert.AreEqual(new Vector2(100, -3), res);
        }

        [Test]
        public void KilometersToMetersVector2Test()
        {
            var km = new Kilometers(100, -3);
            var res = km.ToMetersVector2();
            Assert.AreEqual(new Vector2(100000, -3000), res);
        }

        [Test]
        public void KilometersToVector2WithScaleTest()
        {
            var km = new Kilometers(100, -3);
            var res = km.ToVector2(3);
            Assert.AreEqual(new Vector2(100, -3) / 3, res);
        }

        [Test]
        public void KilometersToMetersVector2WithScaleTest()
        {
            var km = new Kilometers(100, -3);
            var res = km.ToMetersVector2(5);
            Assert.AreEqual(new Vector2(100, -3) * 1000 / 5, res);
        }
    }
}
