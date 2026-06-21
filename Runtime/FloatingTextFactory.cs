using UnityEngine;

namespace FloatingText
{
    internal class FloatingTextFactory
    {
        private GameObject _floatingTextPrefab;

        public FloatingTextFactory()
        {
            _floatingTextPrefab = Resources.Load<FloatingTextView>("FloatingText Prefab (TMP)").gameObject;
        }

        public FloatingTextView Create()
        {
            var newTextGO = GameObject.Instantiate(_floatingTextPrefab);
            return newTextGO.GetComponent<FloatingTextView>();
        }
    }
}
