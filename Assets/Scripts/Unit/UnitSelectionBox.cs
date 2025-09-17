using UnityEngine;
using UnityEngine.InputSystem;

namespace Scripts
{
    public class UnitSelectionBox : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _boxVisual;

        [SerializeField]
        private Canvas _canvas;

        private Camera _myCam;
        private RectTransform _canvasRect;

        private Rect _selectionBox;
        private Vector2 _startPosition;
        private Vector2 _endPosition;

        private void Start()
        {
            _myCam = Camera.main;
            if (_canvas == null)
                _canvas = GetComponentInParent<Canvas>();
            _canvasRect = _canvas.GetComponent<RectTransform>();

            _startPosition = Vector2.zero;
            _endPosition = Vector2.zero;
            _boxVisual.gameObject.SetActive(false);
        }

        public void StartSelection()
        {
            _startPosition = Mouse.current.position.ReadValue();
            _selectionBox = new Rect();
            _boxVisual.gameObject.SetActive(true);
        }

        public void PerformSelection(Vector2 position)
        {
            _endPosition = position;
            Debug.Log(
                $"Start: {_startPosition} End: {_endPosition} Size: {(_endPosition - _startPosition)}"
            );
            DrawVisual();
            DrawSelection();
        }

        public void EndSelection()
        {
            SelectUnits();

            _startPosition = Vector2.zero;
            _endPosition = Vector2.zero;
            _boxVisual.gameObject.SetActive(false);
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
