using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace FloatingText
{
    internal class FloatingTextFactory
    {
        private GameObject _floatingTextPrefab;

        private Dictionary<FloatingTextStyleSO, ObjectPool<FloatingTextView>> _objectPools = new();

        public FloatingTextFactory()
        {
            _floatingTextPrefab = Resources.Load<FloatingTextView>("FloatingText Prefab (TMP)").gameObject;
        }

        public FloatingTextView Create(FloatingTextStyleSO style)
        {
            var pool = GetPoolByStyle(style);
            return pool.Get();
        }

        public void Release(FloatingTextView view, FloatingTextStyleSO style)
        {
            var pool = GetPoolByStyle(style);
            pool.Release(view);
        }

        private ObjectPool<FloatingTextView> GetPoolByStyle(FloatingTextStyleSO style)
        {
            if (!_objectPools.ContainsKey(style))
            {
                _objectPools[style] = InstantiateNewPoolByStyle(style);
                
            }

            return _objectPools[style];
        }

        private ObjectPool<FloatingTextView> InstantiateNewPoolByStyle(FloatingTextStyleSO style)
        {
            return new ObjectPool<FloatingTextView>
            (
                createFunc: () =>
                {
                    return InstantiateViewWithStyle(style);
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

        private FloatingTextView InstantiateViewWithStyle(FloatingTextStyleSO style)
        {
            var viewGO = Object.Instantiate(_floatingTextPrefab);
            viewGO.hideFlags = HideFlags.HideInHierarchy;

            var view = viewGO.GetComponent<FloatingTextView>();
            view.ApplyStyle(style);
            
            return view;
        }
    }
}
