using NB.ECS.Wrapper;
using UnityEngine;

namespace NB.SpaceGame.Physics
{
    public class VelocityComponent: ComponentWrapper<Velocity> {}

    [System.Serializable]
    public struct Velocity
    {
        public Vector2 value;
    }
}
