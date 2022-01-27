using UnityEngine;

namespace NB.ECS.Wrapper
{
    [RequireComponent(typeof(EntityObject))]
    public abstract class ComponentWrapper<T> : MonoBehaviour, IComponentRegistrator where T: struct
    {
        [SerializeField] private T data;

        public void Register(int entity, World world)
        {
            entity.AddComponent(world, data);
        }
    }
}
