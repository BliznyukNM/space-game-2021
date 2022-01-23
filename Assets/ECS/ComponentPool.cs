namespace NB.ECS
{
    internal interface IPool
    {
        void Add(int entity, object data);
        void Set(int entity, object data);
        bool Has(int entity);
        void Remove(int entity);
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

        public void Add(int entity, object data)
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
        }

        public void Remove(int entity)
        {
            recycledComponents[recycledComponentCount++] = entityComponentBinds[entity];
            entityComponentBinds[entity] = -1;
        }

        public bool Has(int entity)
        {
            return entityComponentBinds[entity] >= 0;
        }

        public ref T Get(int entity)
        {
            return ref components[entityComponentBinds[entity]];
        }

        public void Set(int entity, object data)
        {
            components[entityComponentBinds[entity]] = (T) data;
        }
    }
}
