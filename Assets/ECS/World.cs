using System;
using System.Collections.Generic;

namespace NB.ECS
{
    internal static class Settings
    {
        public const int EntityCacheInitialSize = 1024;
        public const int ComponentPoolCacheInitialSize = 64;
    }

    internal struct EntityData
    {
        public ushort Generation;
    }

    public class World
    {
        private EntityData[] entities;
        private int entityCount;

        private int[] recycledEntities;
        private int recycledEntityCount;

        private readonly Dictionary<Type, IPool> pools;

        public World()
        {
            entities = new EntityData[Settings.EntityCacheInitialSize];
            recycledEntities = new int[Settings.EntityCacheInitialSize];
            pools = new Dictionary<Type, IPool>(Settings.EntityCacheInitialSize);
        }

        public int CreateEntity()
        {
            int entity;

            if (recycledEntityCount > 0)
            {
                entity = recycledEntities[--recycledEntityCount];
                ref var data = ref entities[entity];
                data.Generation++;
            }
            else
            {
                entity = entityCount++;
                entities[entity].Generation = 1;
            }

            return entity;
        }

        public bool RemoveEntity(int entity)
        {
            if (entity >= entityCount)
            {
                return false;
            }

            recycledEntities[recycledEntityCount++] = entity;
            entities[entity].Generation = 0;
            return true;
        }

        internal Pool<T> GetPool<T>() where T: struct
        {
            var type = typeof(T);

            if (pools.TryGetValue(type, out var pool))
            {
                return (Pool<T>) pool;
            }

            pool = new Pool<T>(Settings.ComponentPoolCacheInitialSize);
            pools[type] = pool;
            return (Pool<T>) pool;
        }

        internal IPool GetPool(Type type)
        {
            if (pools.TryGetValue(type, out var pool))
            {
                return pool;
            }

            return null;
        }

        internal int[] GetAllEntities()
        {
            var count = entityCount - recycledEntityCount;
            var result = new int[count];

            var i = 0;
            for (int j = 0; j < entityCount; j++)
            {
                if (entities[j].Generation > 0)
                {
                    result[i++] = j;
                }
            }

            return result;
        }
    }
}
