using UnityEngine;

namespace NB.ECS.Wrapper
{
    [DefaultExecutionOrder(-1)]
    public class SceneSpace : MonoBehaviour, ISpace
    {
        public World World { get; private set; }
        public Systems Systems { get; private set; }

        private void Awake()
        {
            World = new World();
            Systems = new Systems(World);
        }

        private void Start()
        {
            Systems.Start();
        }

        private void Update()
        {
            Systems.Update(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            Systems.PhysicsUpdate(Time.fixedDeltaTime);
        }

        private void OnDestroy()
        {
            Systems.Stop();
        }
    }
}
