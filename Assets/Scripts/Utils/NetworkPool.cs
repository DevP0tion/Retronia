using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Pool;

namespace Retronia.Utils
{
    /// <summary>
    /// TODO 네트워크에서 사용할 수 있는 풀로 구현 예정
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class NetworkPool<T> : IObjectPool<T> where T : class
    {
        [SerializeField, ReadOnly] private int countInactive;
        
        private readonly Func<T> createFunc;
        private readonly Action<T> actionOnGet;
        private readonly Action<T> actionOnRelease;
        private readonly Action<T> actionOnDestroy;
        private readonly List<T> stack;
        private readonly bool collectionCheck;
        private readonly int maxSize;

        public NetworkPool(
            Func<T> createFunc,
            Action<T> actionOnGet = null,
            Action<T> actionOnRelease = null,
            Action<T> actionOnDestroy = null,
            bool collectionCheck = true,
            int defaultCapacity = 10,
            int maxSize = 10000)
        {
            this.createFunc = createFunc ?? throw new ArgumentNullException(nameof(createFunc));
            this.actionOnGet = actionOnGet;
            this.actionOnRelease = actionOnRelease;
            this.actionOnDestroy = actionOnDestroy;
            this.collectionCheck = collectionCheck;
            this.maxSize = maxSize;
            
            stack = new (defaultCapacity);
        }

        public T Get()
        {
            T item;
            if (stack.Count == 0)
            {
                item = createFunc();
            }
            else
            {
                item = stack[^1];
                stack.RemoveAt(stack.Count - 1);
                countInactive--;
            }

            actionOnGet?.Invoke(item);
            return item;
        }

        [Obsolete("Use Get() instead. This method will be removed in the future.")]
        public PooledObject<T> Get(out T v)
        {
            throw new NotImplementedException();
        }

        public void Release(T element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (collectionCheck)
            {
                if (stack.Count > 0 && stack.Contains(element))
                    throw new InvalidOperationException("Trying to release an object that has already been released to the pool.");
            }

            actionOnRelease?.Invoke(element);

            if (CountInactive < maxSize)
            {
                stack.Add(element);
                countInactive++;
            }
            else
            {
                actionOnDestroy?.Invoke(element);
            }
        }

        public void Clear()
        {
            if (actionOnDestroy != null)
            {
                foreach (T item in stack)
                {
                    actionOnDestroy(item);
                }
            }
            
            stack.Clear();
            countInactive = 0;
        }

        public int CountInactive => countInactive;
    }
}