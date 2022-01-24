using NB.ECS;
using NB.ECS.Wrapper;
using UnityEngine;

namespace NB.SpaceGame
{
    public class MoveSystem : SceneSystem, IUpdateSystem
    {
        public void OnUpdate(World world)
        {
            var deltaTime = Time.deltaTime;
            var filter = world.WithAll<Translation>();

            foreach(var entity in filter)
            {
                ref var translation = ref entity.GetComponent<Translation>(world);
                translation.position += Vector3.up * deltaTime;
            }
        }
    }
}
