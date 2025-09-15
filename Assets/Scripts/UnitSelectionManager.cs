using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class UnitSelectionManager : MonoBehaviour
    {
        public static UnitSelectionManager Instance { get; set; }

        [SerializeField]
        internal List<GameObject> _unitsList;

        internal List<GameObject> _unitsList2;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }
    }
}
