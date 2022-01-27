using NB.ECS;
using NB.SpaceGame.Physics;
using NB.SpaceGame.Physics.NewtonLaw;
using UnityEngine;

namespace NB.SpaceGame
{
    [ExecuteInEditMode]
    public class StarSystemEmulator : MonoBehaviour
    {
        private enum UpdateType
        {
            None,
            OnValidate,
            OnUpdate
        }

        [SerializeField] private int simulationSteps;
        [SerializeField] private float deltaTime;
        [SerializeField] private float G;
        [SerializeField] private PlanetBuilder relativeTo;
        [SerializeField] private UpdateType updateType;

        private World world;
        private Systems systems;
        private Vector2[,] drawPositions;

        private void CalculateTrajectories()
        {
            var world = new World();
            var system = new GravitySystem(G);

            var bodies = FindObjectsOfType<PlanetBuilder>();
            drawPositions = new Vector2[simulationSteps, bodies.Length];
            foreach (var body in bodies)
            {
                body.Register(world);
            }

            var relativeBodyInitPos = relativeTo != null ? relativeTo.Entity.GetComponent<Position>(world).value : Vector2.zero;

            for (int i = 0; i < simulationSteps; i++)
            {
                var relativeBodyPos = relativeTo != null ? relativeTo.Entity.GetComponent<Position>(world).value : Vector2.zero;
                system.OnPhysicsUpdate(world, deltaTime);
                var filter = world.WithAll<Position, Velocity>();
                int j = 0;

                foreach (var entity in filter)
                {
                    ref var position = ref entity.GetComponent<Position>(world);
                    var velocity = entity.GetComponent<Velocity>(world);
                    position.value += velocity.value * deltaTime;
                    var nextPos = position.value;
                    if (relativeTo != null)
                    {
                        nextPos -= relativeBodyPos - relativeBodyInitPos;

                        if (entity == relativeTo.Entity)
                        {
                            nextPos = relativeBodyInitPos;
                        }
                    }
                    drawPositions[i, j] = nextPos;
                    j++;
                }
            }
        }

        private void Start()
        {
            if (!Application.isPlaying) return;

            world = new World();
            systems = new Systems(world).Register(new GravitySystem(G));
            var bodies = FindObjectsOfType<PlanetBuilder>();
            foreach (var body in bodies)
            {
                body.Register(world);
            }
            systems.Start();
            CalculateTrajectories();
        }

        private void FixedUpdate()
        {
            systems.PhysicsUpdate(Time.fixedDeltaTime);

            var bodies = FindObjectsOfType<PlanetBuilder>();
            foreach (var body in bodies)
            {
                body.UpdateGameObject(world, Time.fixedDeltaTime);
            }
        }

        private void DrawTrajectories()
        {
            if (drawPositions == null) return;

            for (int i = 0; i < drawPositions.GetLength(0) - 1; i++)
            {
                for (int j = 0; j < drawPositions.GetLength(1); j++)
                {
                    Debug.DrawLine(drawPositions[i, j], drawPositions[i + 1, j], Color.blue);
                }
            }
        }

        private void Update()
        {
            if (updateType == UpdateType.OnUpdate && !Application.isPlaying) CalculateTrajectories();
            if (updateType != UpdateType.None) DrawTrajectories();
        }

        private void OnValidate()
        {
            if (updateType == UpdateType.OnValidate && !Application.isPlaying) CalculateTrajectories();
        }
    }
}
