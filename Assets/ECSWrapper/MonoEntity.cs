using UnityEngine;

namespace NB.ECS.Wrapper
{
    public struct Translation
    {
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 scale;
    }

    public abstract class MonoEntity : MonoBehaviour
    {
        protected abstract ISpace space { get; }
        protected int entity;

        private void Awake()
        {
            entity = space.World.CreateEntity();
            entity.AddComponent(space.World, new Translation { position = transform.position, rotation = transform.rotation, scale = transform.localScale });
        }

        private void Update()
        {
            var translation = entity.GetComponent<Translation>(space.World);
            transform.position = translation.position;
            transform.rotation = translation.rotation;
            transform.localScale = translation.scale;
        }
    }
}
