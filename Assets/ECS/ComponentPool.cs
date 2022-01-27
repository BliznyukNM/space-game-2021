namespace NB.ECS
{
    internal interface IPool
    {
        int Add(int entity, object data);
        void Set(int entity, object data);
        bool Has(int entity);
        bool Remove(int entity);
    }

    internal class Pool<T> : IPool where T : struct
    {
        private T[] components;
        private int componentCount;

        private int[] entityComponentBinds;

        private int[] recycledComponents;
        private int recycledComponentCount;

        public Pool(int initialSize)
        {
            components = new T[initialSize];
            recycledComponents = new int[initialSize];
            entityComponentBinds = new int[initialSize];
            System.Array.Fill(entityComponentBinds, -1);
        }

        public int Add(int entity, object data)
        {
            int id;

            if (recycledComponentCount > 0)
            {
                id = recycledComponents[--recycledComponentCount];
            }
            else
            {
                id = componentCount++;
            }

            entityComponentBinds[entity] = id;
            ref var component = ref components[id];
            component = (T) data;
            return id;
        }

        public bool Remove(int entity)
        {
            if (entityComponentBinds[entity] < 0)
            {
                return false;
            }

            recycledComponents[recycledComponentCount++] = entityComponentBinds[entity];
            entityComponentBinds[entity] = -1;
            return true;
        }

        public bool Has(int entity)
        {
            return entityComponentBinds[entity] >= 0;
        }

        public ref T Get(int entity)
        {
            if (entityComponentBinds[entity] < 0)
            {
                throw new System.InvalidOperationException($"No component {typeof(T).Name} for entity {entity}");
            }

            return ref components[entityComponentBinds[entity]];
        }

        public void Set(int entity, object data)
        {
            components[entityComponentBinds[entity]] = (T) data;
        }
    }
}
