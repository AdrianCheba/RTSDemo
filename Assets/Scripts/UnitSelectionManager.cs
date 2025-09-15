using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scripts
{

    public class UnitSelectionManager : MonoBehaviour
    {
        public static UnitSelectionManager Instance { get; set; }

        [SerializeField]
        internal List<GameObject> _unitsList;

        [SerializeField]
        InputActionAsset _inputActions;

        InputAction _movement;
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

            _movement = _inputActions.FindActionMap("Player").FindAction("Movement");
        }

        private void OnEnable()
        {
            _movement.Enable();
            _movement.performed += OnMovement;
        }

        private void OnDisable()
        {
            _movement.performed -= OnMovement;
            _movement.Disable();
        }

        private void OnMovement(InputAction.CallbackContext context)
        {
            _movement.performed += _ =>
            {
                foreach (GameObject unit in _unitsList)
                {
                    unit.GetComponent<UnitMovement>().OnMovement();
                }
            };
        }
    }
}