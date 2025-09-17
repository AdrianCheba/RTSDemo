using UnityEngine;
using UnityEngine.InputSystem;

namespace Scripts
{
    public class InputSystem : MonoBehaviour
    {
        [SerializeField]
        InputActionAsset _inputActions;

        [SerializeField]
        UnitSelectionBox _selectionBox;

        UnitSelectionManager _unitSelectionManager;
        InputActionMap _mapPlayer;
        InputActionMap _mapUnitSelectionBox;
        InputAction _movement;
        InputAction _selection;
        InputAction _multiSelect;
        InputAction _unitSelection;
        InputAction _pointer;

        private void Awake()
        {
            _mapPlayer = _inputActions.FindActionMap("Player");
            _mapUnitSelectionBox = _inputActions.FindActionMap("UnitSelctionBox");
            _unitSelectionManager = GetComponent<UnitSelectionManager>();
            Initialization();
        }

        private void OnEnable()
        {
            _mapPlayer.Enable();
            _mapUnitSelectionBox.Enable();
        }

        private void OnDisable()
        {
            _mapPlayer.Disable();
            _mapUnitSelectionBox.Disable();
        }

        private void Initialization()
        {
            _selection = _mapPlayer.FindAction("Selection");
            _selection.performed += _ =>
            {
                _unitSelectionManager.Selection();
            };

            _multiSelect = _mapPlayer.FindAction("MultiSelect");
            _multiSelect.started += _ =>
            {
                _unitSelectionManager._canMultiSelect = true;
            };
            _multiSelect.canceled += _ =>
            {
                _unitSelectionManager._canMultiSelect = false;
            };
            _unitSelection = _mapUnitSelectionBox.FindAction("UnitSelection");
            _unitSelection.started += _ =>
            {
                _selectionBox.StartSelection();
            };
            _unitSelection.canceled += _ =>
            {
                _selectionBox.EndSelection();
            };
            _pointer = _mapUnitSelectionBox.FindAction("Pointer");
            _pointer.performed += _ =>
            {
                if (!_unitSelection.IsPressed())
                    return;

                _selectionBox.PerformSelection(_pointer.ReadValue<Vector2>());
            };
            _movement = _mapPlayer.FindAction("Movement");
            _movement.performed += _ =>
            {
                _unitSelectionManager.Target();
            };
        }
    }
}
