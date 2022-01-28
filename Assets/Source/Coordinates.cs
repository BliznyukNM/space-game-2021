using System;
using UnityEngine;

namespace NB.SpaceGame
{
    [Serializable]
    public struct Meters
    {
        public float x, y;

        public Meters(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public static Meters operator+(Meters a, Meters b)
        { 
            return new Meters(a.x + b.x, a.y + b.y);
        }

        public static Meters operator-(Meters a, Meters b)
        { 
            return new Meters(a.x - b.x, a.y - b.y);
        }

        public static Meters operator+(Meters a, Vector2 b)
        { 
            return new Meters(a.x + b.x, a.y + b.y);
        }

        public static Meters operator-(Meters a, Vector2 b)
        { 
            return new Meters(a.x - b.x, a.y - b.y);
        }

        public Vector2 ToVector2(float scale = 1)
        {
            return new Vector2(x, y) / scale;
        }
    }

    [Serializable]
    public struct Kilometers
    {
        public int x, y;
        public const float MetersInKilometer = 1000f;

        public Kilometers(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static Kilometers operator+(Kilometers a, Kilometers b)
        { 
            return new Kilometers(a.x + b.x, a.y + b.y);
        }

        public static Kilometers operator-(Kilometers a, Kilometers b)
        { 
            return new Kilometers(a.x - b.x, a.y - b.y);
        }

        public Vector2 ToVector2(float scale = 1)
        {
            return new Vector2(x, y) / scale;
        }

        public Vector2 ToVector2(Kilometers origin, float scale = 1)
        {
            var relative = this - origin;
            return relative.ToVector2(scale);
        }

        public Vector2 ToMetersVector2(float scale = 1)
        {
            return new Vector2(x, y) / scale * MetersInKilometer;
        }

        public Vector2 ToMetersVector2(Kilometers origin, float scale = 1)
        {
            var relative = this - origin;
            return relative.ToMetersVector2(scale);
        }

        public override String ToString()
        {
            return $"({x}, {y})";
        }
    }

    [Serializable]
    public struct LightSeconds
    {
        public int x, y;
        public const int KilometersInLightSecond = 299792;

        public LightSeconds(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static LightSeconds operator+(LightSeconds a, LightSeconds b)
        { 
            return new LightSeconds(a.x + b.x, a.y + b.y);
        }

        public static LightSeconds operator-(LightSeconds a, LightSeconds b)
        { 
            return new LightSeconds(a.x - b.x, a.y - b.y);
        }

        public Vector2 ToVector2(float scale = 1)
        {
            return new Vector2(x, y) / scale;
        }

        public Vector2 ToVector2(LightSeconds origin, float scale = 1)
        {
            var relative = this - origin;
            return relative.ToVector2(scale);
        }

        public Vector2 ToKilometersVector2(float scale = 1)
        {
            return new Vector2(x, y) / scale * KilometersInLightSecond;
        }

        public Vector2 ToKilometersVector2(LightSeconds origin, float scale = 1)
        {
            var relative = this - origin;
            return relative.ToKilometersVector2(scale);
        }

        public Vector2 ToMetersVector2(float scale = 1)
        {
            return new Vector2(x, y) / scale * KilometersInLightSecond * Kilometers.MetersInKilometer;
        }

        public Vector2 ToMetersVector2(LightSeconds origin, float scale = 1)
        {
            var relative = this - origin;
            return relative.ToMetersVector2(scale);
        }
    }

    [Serializable]
    public struct SpaceCoordinate
    {
        public Meters m;
        public Kilometers km;
        public LightSeconds ls;

        public SpaceCoordinate(Meters m, Kilometers km, LightSeconds ls)
        {
            this.m = m;
            this.km = km;
            this.ls = ls;
        }

        public static SpaceCoordinate operator+(SpaceCoordinate a, SpaceCoordinate b)
        { 
            return new SpaceCoordinate(a.m + b.m, a.km + b.km, a.ls + b.ls);
        }

        public static SpaceCoordinate operator-(SpaceCoordinate a, SpaceCoordinate b)
        { 
            return new SpaceCoordinate(a.m - b.m, a.km - b.km, a.ls - b.ls);
        }

        public Vector2 ToLightSecondsVector2(float scale = 1)
        {
            return ls.ToVector2(scale) + km.ToVector2(scale * LightSeconds.KilometersInLightSecond); // skipping meters, to low values
        }

        public Vector2 ToLightSecondsVector2(SpaceCoordinate origin, float scale = 1)
        {
            var relative = this - origin;
            return relative.ToLightSecondsVector2(scale);
        }

        public Vector2 ToKilometersVector2(float scale = 1)
        {
            return ls.ToVector2(scale) * LightSeconds.KilometersInLightSecond + km.ToVector2(scale) + m.ToVector2(scale * Kilometers.MetersInKilometer);
        }

        public Vector2 ToKilometersVector2(SpaceCoordinate origin, float scale = 1)
        {
            var relative = this - origin;
            return relative.ToKilometersVector2(scale);
        }

        public Vector2 ToMetersVector2(float scale = 1)
        {
            return (ls.ToVector2(scale) * LightSeconds.KilometersInLightSecond + km.ToVector2(scale)) * Kilometers.MetersInKilometer + m.ToVector2(scale);
        }

        public Vector2 ToMetersVector2(SpaceCoordinate origin, float scale = 1)
        {
            var relative = this - origin;
            return relative.ToKilometersVector2(scale);
        }
    }

    public static class CoordinateTools
    {
        public static SpaceCoordinate ConvertKilometersToSpaceCoordinate(this in Vector2 pos)
        {
            var meters = new Meters();
            var kilometers = new Kilometers();
            var lightSeconds = new LightSeconds();
            return new SpaceCoordinate(meters, kilometers, lightSeconds);
        }

        public static Kilometers ConvertMetersToKilometers(this in Vector2 pos)
        {
            var scaledPos = pos / Kilometers.MetersInKilometer;
            return new Kilometers((int)scaledPos.x, (int)scaledPos.y);
        }
    }
}
