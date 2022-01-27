using UnityEngine;

namespace NB.ECS.Wrapper
{
    public class EntityObject : MonoBehaviour
    {
        [SerializeField] private SceneSpace space; // TODO: init via code or smth

        public int Entity { get; private set; }
        public World World => space.World;

        private void Awake()
        {
            Entity = space.World.CreateEntity();

            var registrators = GetComponents<IComponentRegistrator>();
            foreach (var registrator in registrators)
            {
                registrator.Register(Entity, World);
            }
        }
    }
}
