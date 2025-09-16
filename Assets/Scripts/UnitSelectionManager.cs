using System;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        internal List<GameObject> _selectedUnits;

        [SerializeField]
        LayerMask _clikable;

        [SerializeField]
        LayerMask _ground;

        [SerializeField]
        Transform _groundMarker;

        internal bool _canMultiSelect;
        RaycastHit _hit;
        Camera _camera;

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

        private void Start()
        {
            _camera = Camera.main;
        }

        internal void Selection()
        {
            Ray ray = _camera.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _clikable))
            {
                if (_canMultiSelect)
                {
                    MultiSelect(hit.collider.gameObject);
                }
                else
                {
                    SelectByClicking(hit.collider.gameObject);
                }
            }
            else
            {
                DeselectAll();
            }
        }

        internal void Target()
        {
            Ray ray = _camera.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray, out _hit, Mathf.Infinity, _ground))
            {
                if (_selectedUnits.Count > 0)
                {
                    SetGroundMark(true, _hit.point);
                    foreach (GameObject unit in _selectedUnits)
                    {

                        if (!unit.TryGetComponent<UnitMovement>(out UnitMovement unitMovement)) return;

                        unitMovement.OnMovement(_hit.point);
                    }
                }
                else
                {
                    SetGroundMark(false, _hit.point);
                }
            }
        }

        private void MultiSelect(GameObject unit)
        {
            if (!_selectedUnits.Contains(unit))
            {
                _selectedUnits.Add(unit);
                EnableUnitMovement(unit, true);
            }
            else
            {
                EnableUnitMovement(unit, false);
                _selectedUnits.Remove(unit);
            }
        }

        private void DeselectAll()
        {
            foreach (GameObject unit in _unitsList)
            {
                EnableUnitMovement(unit, false);
            }

            SetGroundMark(false, _hit.point);
            _selectedUnits.Clear();
        }

        private void SelectByClicking(GameObject unit)
        {
            DeselectAll();

            _selectedUnits.Add(unit);

            EnableUnitMovement(unit, true);
        }

        private void EnableUnitMovement(GameObject unit, bool isActive)
        {
            
            if (!unit.TryGetComponent<UnitMovement>(out UnitMovement unitMovement)) return;

            unitMovement.ToggelActivity(isActive);
        }

        private void SetGroundMark(bool visibility, Vector3 position)
        {
            _groundMarker.gameObject.SetActive(visibility);
            _groundMarker.position = position;
        }
    }
}
