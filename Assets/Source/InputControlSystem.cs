using NB.ECS;
using NB.ECS.Wrapper;
using UnityEngine;

namespace NB.SpaceGame
{
    public class InputControlSystem : SceneSystem, IUpdateSystem
    {
        public void OnUpdate(World world)
        {
            var input = Vector3.zero;
        }
    }
}
