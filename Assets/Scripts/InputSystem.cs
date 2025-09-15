using UnityEngine;
using UnityEngine.InputSystem;

namespace Scripts
{
    public class InputSystem : MonoBehaviour
    {
        [SerializeField]
        InputActionAsset _inputActions;

        UnitSelectionManager _unitSelectionManager;
        InputActionMap _map;
        InputAction _movement;
        InputAction _selection;
        InputAction _multiSelect;

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
                _unitSelectionManager.Target();
            };

            _selection = _map.FindAction("Selection");
            _selection.performed += _ =>
            {
                _unitSelectionManager.Selection();
            };

            _multiSelect = _map.FindAction("MultiSelect");
            _multiSelect.started += _ =>
            {
                _unitSelectionManager._canMultiSelect = true;
            };
            _multiSelect.canceled += _ =>
            {
                _unitSelectionManager._canMultiSelect = false;
            };
        }
    }
}
