using NB.SpaceGame.Physics;
using NB.SpaceGame.Physics.NewtonLaw;
using UnityEngine;

namespace NB.SpaceGame
{
    [CreateAssetMenu(menuName = "Space Game/Celestial body data")]
    public class CelestialBodyData : ScriptableObject
    {
        public Kilometers coordinate;
        public Velocity velocity;
        public Mass mass;
        public float radius;
    }
}
