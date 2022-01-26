using NB.ECS;
using NB.ECS.Wrapper;
using UnityEngine;

namespace NB.SpaceGame.NewtonLaw
{
    public class GravitySystem : SceneSystem, IPhysicsUpdateSystem
    {
        [SerializeField] private float G = 9.81f;

        public void OnPhysicsUpdate(World world, float delta)
        {
            var bodies = world.WithAll<Velocity, Position, Body>();
            var celestials = bodies.With<Mass>();

            foreach (var body in bodies)
            {
                ref var acceleration = ref body.GetComponent<Velocity>(world);
                var pos1 = body.GetComponent<Position>(world).value;

                foreach (var celestial in celestials)
                {
                    if (body == celestial)
                    {
                        continue;
                    }

                    var pos2 = body.GetComponent<Position>(world).value;
                    var mass = body.GetComponent<Mass>(world).value;
                    acceleration.value += CalculateAcceleration(pos1, pos2, mass) * delta;
                }
            }
        }

        private Vector2 CalculateAcceleration(Vector2 pos1, Vector2 pos2, float mass)
        {
            var sqrDistance = Mathf.Pow(pos1.x - pos2.x, 2) + Mathf.Pow(pos1.y - pos2.y, 2);
            var direction = pos2 - pos1;
            return direction * G * mass / sqrDistance;
        }
    }
}
