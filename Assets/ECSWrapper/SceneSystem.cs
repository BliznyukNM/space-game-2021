using UnityEngine;

namespace NB.ECS.Wrapper
{
    [RequireComponent(typeof(SceneSpace))]
    public abstract class SceneSystem : MonoBehaviour, ISystem
    {
        private SceneSpace Space => space ?? (space = GetComponent<SceneSpace>());
        private SceneSpace space;

        private void Awake()
        {
            Space.Systems.Register(this);
        }
    }
}
