using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NB.ECS
{
    public struct Filter: IEnumerable<int>
    {
        private int[] entities;
        private int entityCount;
        internal World world { get; }

        internal Filter(Filter other)
        {
            entities = new int[other.entityCount];
            entityCount = 0;
            world = other.world;
        }

        internal Filter(World world)
        {
            entities = world.GetAllEntities();
            entityCount = entities.Length;
            this.world = world;
        }

        internal void Add(int entity)
        {
            entities[entityCount++] = entity;
        }

        public IEnumerator<int> GetEnumerator()
        {
            foreach(var entity in entities)
            {
                yield return entity;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public static class FilterExtentions
    {
        public static Filter WithAll<A>(this in Filter filter) where A: struct
        {
            var result = new Filter(filter);
            var world = filter.world;

            foreach(var entity in filter)
            {
                if (entity.HasComponent<A>(world))
                {
                    result.Add(entity);
                }
            }

            return result;
        }

        public static Filter WithAll<A>(this World world) where A: struct
        {
            return new Filter(world).WithAll<A>();
        }
    }
}
