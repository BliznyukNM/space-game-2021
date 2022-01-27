using UnityEngine;

namespace NB.SpaceGame
{
    public struct Centimeters
    {
        public byte x, y;
    }

    public struct Meters
    {
        public short x, y; // = 100 santimeters
        public static readonly Meters Zero = new Meters { x = 0, y = 0 };
    }

    public struct Kilometers
    {
        public int x, y; // = 1000 meters
        public static readonly Kilometers Zero = new Kilometers { x = 0, y = 0 };
    }

    public struct LightSeconds
    {
        public int x, y; // = 299792 kilometers
        public static readonly LightSeconds Zero = new LightSeconds { x = 0, y = 0 };
    }

    public struct LightYears
    {
        public long x, y; // = 31557600 light seconds

        public LightYears(long x, long y)
        {
            this.x = x;
            this.y = y;
        }

        public static LightYears operator-(LightYears ly1, LightYears ly2)
        {
            return new LightYears(ly1.x - ly2.x, ly1.y - ly2.y);
        }

        public Vector2 ToVector2(long lightYearsInUnit = 1)
        {
            return new Vector2(x / lightYearsInUnit, y / lightYearsInUnit);
        }

        public Vector2 ToVector2(LightYears relative, long lightYearsInUnit = 1)
        {
            var fixedLy = this - relative;
            return fixedLy.ToVector2(lightYearsInUnit);
        }
    }

    public struct Coordinate
    {
        public LightYears ly;
        public LightSeconds ls;
        public Kilometers km;
        public Meters m;
        public Centimeters cm;
    } // 2 * 8 + 2 * 4 + 2 * 4 + 2 * 2 + 2 * 1 = 36 byte
}
