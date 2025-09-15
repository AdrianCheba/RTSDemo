using UnityEngine;
using UnityEngine.InputSystem;

namespace Scripts
{
    public class InputSystem : MonoBehaviour
    {
        UnitSelectionManager _unitSelectionManager;

        [SerializeField]
        InputActionAsset _inputActions;

        InputActionMap _map;

        InputAction _movement;

        private void Awake()
        {
            _map = _inputActions.FindActionMap("Player");
            _unitSelectionManager = GetComponent<UnitSelectionManager>();
            Initialization();
        }

        private void OnEnable()
        {
            _map.Enable();
        }

        private void OnDisable()
        {
            _map.Disable();
        }

        private void Initialization()
        {
            _movement = _map.FindAction("Movement");
            _movement.performed += _ =>
            {
                foreach (GameObject unit in _unitSelectionManager._unitsList)
                {
                    unit.GetComponent<UnitMovement>().OnMovement();
                }
            };
        }
    }
}
