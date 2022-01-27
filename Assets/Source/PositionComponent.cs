using NB.ECS.Wrapper;
using UnityEngine;

namespace NB.SpaceGame
{
    public class PositionComponent: ComponentWrapper<Position> {}

    [System.Serializable]
    public struct Position
    {
        public Vector2 value;
    }
}
