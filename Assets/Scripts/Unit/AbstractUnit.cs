using UnityEngine;

namespace Scripts
{
    public class AbstractUnit : MonoBehaviour
    {
        void Start()
        {
            UnitSelectionManager.Instance._unitsList.Add(gameObject);
        }

        private void OnDestroy()
        {
            UnitSelectionManager.Instance._unitsList.Remove(gameObject);
        }
    }
}