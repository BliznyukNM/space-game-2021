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
            for(int i = 0; i < entityCount; i++)
            {
                yield return entities[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public static class FilterExtentions
    {
        #region WithAll
        public static Filter WithAll<A>(this in Filter filter)
            where A: struct
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

        public static Filter WithAll<A, B>(this in Filter filter)
            where A: struct
            where B: struct
        {
            var result = new Filter(filter);
            var world = filter.world;

            foreach(var entity in filter)
            {
                if (entity.HasAllComponents(world, typeof(A), typeof(B)))
                {
                    result.Add(entity);
                }
            }

            return result;
        }
        
        public static Filter WithAll<A, B, C>(this in Filter filter)
            where A: struct
            where B: struct
            where C: struct
        {
            var result = new Filter(filter);
            var world = filter.world;

            foreach(var entity in filter)
            {
                if (entity.HasAllComponents(world, typeof(A), typeof(B), typeof(C)))
                {
                    result.Add(entity);
                }
            }

            return result;
        }

        public static Filter WithAll<A>(this World world)
            where A: struct
        {
            return new Filter(world).WithAll<A>();
        }

        public static Filter WithAll<A, B>(this World world)
            where A: struct
            where B: struct
        {
            return new Filter(world).WithAll<A, B>();
        }

        public static Filter WithAll<A, B, C>(this World world)
            where A: struct
            where B: struct
            where C: struct
        {
            return new Filter(world).WithAll<A, B, C>();
        }
        #endregion
    
        #region WithAny
        public static Filter WithAny<A>(this in Filter filter)
            where A: struct
        {
            return WithAll<A>(filter);
        }

        public static Filter WithAny<A, B>(this in Filter filter)
            where A: struct
            where B: struct
        {
            var result = new Filter(filter);
            var world = filter.world;

            foreach(var entity in filter)
            {
                if (entity.HasAnyComponent(world, typeof(A), typeof(B)))
                {
                    result.Add(entity);
                }
            }

            return result;
        }

        public static Filter WithAny<A, B, C>(this in Filter filter)
            where A: struct
            where B: struct
            where C: struct
        {
            var result = new Filter(filter);
            var world = filter.world;

            foreach(var entity in filter)
            {
                if (entity.HasAnyComponent(world, typeof(A), typeof(B), typeof(C)))
                {
                    result.Add(entity);
                }
            }

            return result;
        }

        public static Filter WithAny<A>(this World world)
            where A: struct
        {
            return new Filter(world).WithAll<A>(); // With all with 1 argument is equal to with any
        }

        public static Filter WithAny<A, B>(this World world)
            where A: struct
            where B: struct
        {
            return new Filter(world).WithAny<A, B>();
        }

        public static Filter WithAny<A, B, C>(this World world)
            where A: struct
            where B: struct
            where C: struct
        {
            return new Filter(world).WithAny<A, B, C>();
        }
        #endregion
    }
}
