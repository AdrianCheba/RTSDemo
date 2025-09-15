using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace Scripts
{
    public class UnitMovement : MonoBehaviour
    {
        Camera _camera;
        NavMeshAgent _agent;

        [SerializeField]
        LayerMask _ground;

        private void Start()
        {
            _camera = Camera.main;
            _agent = GetComponent<NavMeshAgent>();
        }

        public void OnMovement()
        {
            Ray ray = _camera.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _ground))
            {
                _agent.SetDestination(hit.point);
            }
        }
    }
}
