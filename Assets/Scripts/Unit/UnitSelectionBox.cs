using UnityEngine;
using UnityEngine.InputSystem;

namespace Scripts
{
    public class UnitSelectionBox : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _boxVisual;

        private Camera _myCam;

        private Rect _selectionBox;
        private Vector2 _startPosition;
        private Vector2 _endPosition;

        private void Start()
        {
            _myCam = Camera.main;
            _startPosition = Vector2.zero;
            _endPosition = Vector2.zero;
            _boxVisual.sizeDelta = Vector2.zero;
        }

        public void StartSelection()
        {
            _startPosition = Mouse.current.position.ReadValue();
            _selectionBox = new Rect();
        }

        public void PerformSelection(Vector2 position)
        {
            if (_boxVisual.rect.width > 0 || _boxVisual.rect.height > 0)
            {
                UnitSelectionManager.Instance.DeselectAll();
                SelectUnits();
            }

            _endPosition = position;
            DrawVisual();
            DrawSelection();
        }

        public void EndSelection()
        {
            SelectUnits();

            _startPosition = Vector2.zero;
            _endPosition = Vector2.zero;
            _boxVisual.sizeDelta = Vector2.zero;
        }

        private void DrawVisual()
        {
            Vector2 boxStart = _startPosition;
            Vector2 boxEnd = _endPosition;

            Vector2 boxCenter = (boxStart + boxEnd) / 2;

            _boxVisual.position = boxCenter;

            Vector2 boxSize = new Vector2(
                Mathf.Abs(boxStart.x - boxEnd.x),
                Mathf.Abs(boxStart.y - boxEnd.y)
            );

            _boxVisual.sizeDelta = boxSize;
        }

        private void DrawSelection()
        {
            if (Input.mousePosition.x < _startPosition.x)
            {
                _selectionBox.xMin = Input.mousePosition.x;
                _selectionBox.xMax = _startPosition.x;
            }
            else
            {
                _selectionBox.xMin = _startPosition.x;
                _selectionBox.xMax = Input.mousePosition.x;
            }

            if (Input.mousePosition.y < _startPosition.y)
            {
                _selectionBox.yMin = Input.mousePosition.y;
                _selectionBox.yMax = _startPosition.y;
            }
            else
            {
                _selectionBox.yMin = _startPosition.y;
                _selectionBox.yMax = Input.mousePosition.y;
            }
        }

        private void SelectUnits()
        {
            foreach (GameObject unit in UnitSelectionManager.Instance._unitsList)
            {
                Vector2 screenPos = _myCam.WorldToScreenPoint(unit.transform.position);
                if (_selectionBox.Contains(screenPos))
                {
                    UnitSelectionManager.Instance.DragSelect(unit);
                }
            }
        }
    }
}
