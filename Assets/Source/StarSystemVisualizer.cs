using NB.ECS;
using NB.SpaceGame.Physics;
using NB.SpaceGame.Physics.NewtonLaw;
using UnityEngine;

namespace NB.SpaceGame
{
    [ExecuteInEditMode]
    public class StarSystemVisualizer : MonoBehaviour
    {
        [SerializeField] private StarSystemData data;
        [SerializeField] private float G; // TODO: temporary

        [SerializeField] private int simulationSteps;
        [SerializeField] private float deltaTime;
        public bool simulateOnUpdate;

        [Range(1, 30000)]
        [SerializeField] private float scale;

        private Vector2[,] drawPoints;

        public void CalculateTrajectory()
        {
            var world = new World();
            var system = new GravitySystem(G);

            drawPoints = new Vector2[data.Bodies.Count, simulationSteps];

            foreach (var body in data.Bodies)
            {
                var entity = world.CreateEntity();
                entity.AddComponent(world, body.coordinate);
                entity.AddComponent(world, body.velocity);
                entity.AddComponent(world, body.mass);
                entity.AddComponent(world, new Body());
            }

            var filter = world.WithAll<Kilometers, Velocity, Mass>();

            for (int i = 0; i < simulationSteps; i++)
            {
                system.OnPhysicsUpdate(world, deltaTime);

                var j = 0;
                foreach (var entity in filter)
                {
                    ref var position = ref entity.GetComponent<Kilometers>(world);
                    var velocity = entity.GetComponent<Velocity>(world);
                    position += (velocity.value * deltaTime).ConvertMetersToKilometers();
                    drawPoints[j, i] = position.ToVector2(scale);
                    j++;
                }
            }
        }

        private void Update()
        {
            if (simulateOnUpdate) CalculateTrajectory();
        }

        private void OnDrawGizmos()
        {
            if (drawPoints != null) DrawTrajectory();
        }

        private void DrawTrajectory()
        {
            for (int i = 0; i < drawPoints.GetLength(0); i++)
            {
                for (int j = 0; j < drawPoints.GetLength(1) - 1; j++)
                {
                    Gizmos.color = Color.blue;
                    Gizmos.DrawLine(drawPoints[i, j], drawPoints[i, j + 1]);
                }
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(drawPoints[i, drawPoints.GetLength(1) - 1], data.Bodies[i].radius / scale);
            }
        }
    }
}
