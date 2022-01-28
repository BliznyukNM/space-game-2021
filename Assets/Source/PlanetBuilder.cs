using NB.ECS;
using NB.SpaceGame.Physics;
using NB.SpaceGame.Physics.NewtonLaw;
using UnityEngine;

namespace NB.SpaceGame
{
    [ExecuteInEditMode]
    public class PlanetBuilder : MonoBehaviour
    {
        [Tooltip("Radius in kilometers")]
        [Range(1, 10000)]
        [SerializeField] private int radius;

        [Range(8, 1024)]
        [SerializeField] private ushort resolution;

        [SerializeField] private Vector2 velocity;

        [SerializeField] private float mass;

        [SerializeField] private Texture2D texture;

        public Color trailDebugColor = Color.white;

        private bool updateShape;

        public int Entity { get; private set; }

        private void Update()
        {
            if (Application.isPlaying)
            {
                return;
            }

            if (updateShape)
            {
                updateShape = false;
                Generate();
            }
        }

        public void Register(World world, float scale)
        {
            Entity = world.CreateEntity();
            Entity.AddComponent(world, new Position { value = transform.position });
            Entity.AddComponent(world, new Velocity { value = velocity });
            Entity.AddComponent(world, new Body {});
            Entity.AddComponent(world, new Mass { value = mass });
        }

        public void UpdateGameObject(World world, float deltaTime)
        {
            var velocity = Entity.GetComponent<Velocity>(world);
            Vector3 deltaPos = velocity.value * deltaTime;
            transform.position += deltaPos;
            ref var position = ref Entity.GetComponent<Position>(world);
            position.value = transform.position;
        }

        public void Generate()
        {
            var spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            var spriteSize = Mathf.Min(texture.width, texture.height) / 2f;
            var sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 256);

            var verticies = new Vector2[resolution + 1];
            verticies[0] = Vector3.one * spriteSize;

            for (int i = 1; i <= resolution; i++)
            {
                var angle = 2f * (i - 1) / resolution * Mathf.PI;
                verticies[i] = new Vector2(1 + Mathf.Sin(angle), 1 + Mathf.Cos(angle)) * spriteSize;
            }

            var triangles = new ushort[resolution * 3];

            for (ushort i = 0; i < resolution; i++)
            {
                triangles[i * 3 + 0] = 0;
                triangles[i * 3 + 1] = (ushort)(i + 1);
                triangles[i * 3 + 2] = (ushort)((i + 1) % resolution + 1);
            }

            sprite.OverrideGeometry(verticies, triangles);
            spriteRenderer.sprite = sprite;
            transform.localScale = Vector3.one * radius;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            updateShape = true;
        }
#endif
    }
}
