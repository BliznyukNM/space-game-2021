using NB.ECS;
using NB.ECS.Wrapper;
using UnityEngine;

namespace NB.SpaceGame.Physics
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(EntityObject))]
    public class EntityRigidbody2D : MonoBehaviour
    {
        [SerializeField] private new Rigidbody2D rigidbody; // TODO: assign automatically via [ComponentReference]
        [SerializeField] private EntityObject entityObject;

        private void FixedUpdate()
        {
            if (entityObject.Entity.HasComponent<Velocity>(entityObject.World))
            {
                var velocity = entityObject.Entity.GetComponent<Velocity>(entityObject.World);
                rigidbody.velocity = velocity.value;
            }
            if (entityObject.Entity.HasComponent<Position>(entityObject.World))
            {
                ref var position = ref entityObject.Entity.GetComponent<Position>(entityObject.World);
                position.value = rigidbody.position;
            }
        }
    }
}
