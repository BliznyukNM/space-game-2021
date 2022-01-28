using NB.ECS;
using UnityEngine;

namespace NB.SpaceGame.Physics.NewtonLaw
{
    public class GravitySystem : IPhysicsUpdateSystem
    {
        public GravitySystem(float G)
        {
            this.G = G;
        }

        private float G = 0.01f;

        public void OnPhysicsUpdate(World world, float delta)
        {
            var bodies = world.WithAll<Kilometers, Velocity, Body>();
            var celestials = bodies.With<Mass>();

            foreach (var body in bodies)
            {
                ref var velocity = ref body.GetComponent<Velocity>(world);
                var pos1 = body.GetComponent<Kilometers>(world);

                foreach (var celestial in celestials)
                {
                    if (body == celestial)
                    {
                        continue;
                    }

                    var pos2 = celestial.GetComponent<Kilometers>(world);
                    var mass = celestial.GetComponent<Mass>(world).value;
                    var acceleration = CalculateAcceleration(pos1.ToMetersVector2(), pos2.ToMetersVector2(), mass);
                    velocity.value += acceleration * delta;
                }
            }
        }

        private Vector2 CalculateAcceleration(Vector2 pos1, Vector2 pos2, float mass)
        {
            var sqrDistance = Mathf.Pow(pos1.x - pos2.x, 2) + Mathf.Pow(pos1.y - pos2.y, 2);
            var direction = (pos2 - pos1).normalized;
            return direction * G * mass / sqrDistance;
        }
    }
}
