using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace FloatingText
{
    internal class FloatingTextFactory
    {
        private GameObject _floatingTextPrefab;

        private Dictionary<int, ObjectPool<FloatingTextView>> _objectPools = new();

        public FloatingTextFactory()
        {
            _floatingTextPrefab = Resources.Load<FloatingTextView>("FloatingText Prefab (TMP)").gameObject;
        }

        public FloatingTextView Create(int style = 0)
        {
            var pool = GetPoolByStyle(style);
            return pool.Get();
        }

        public void Release(FloatingTextView view, int style)
        {
            var pool = GetPoolByStyle(style);
            pool.Release(view);
        }

        private ObjectPool<FloatingTextView> GetPoolByStyle(int style)
        {
            if (!_objectPools.ContainsKey(style))
            {
                _objectPools[style] = InstantiateNewPool();
                
            }

            return _objectPools[style];
        }

        private ObjectPool<FloatingTextView> InstantiateNewPool()
        {
            return new ObjectPool<FloatingTextView>
            (
                createFunc: () =>
                {
                    var view = Object.Instantiate(_floatingTextPrefab);
                    view.hideFlags = HideFlags.HideInHierarchy;
                    return view.GetComponent<FloatingTextView>();
                },
                actionOnGet: view =>
                {
                    view.gameObject.SetActive(true);
                },
                actionOnRelease: view =>
                {
                    view.gameObject.SetActive(false);
                },
                actionOnDestroy: view =>
                {
                    Object.Destroy(view.gameObject);
                },
                collectionCheck: false,
                maxSize: 2048
            );
        }
    }
}
