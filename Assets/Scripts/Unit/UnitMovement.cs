using UnityEngine;
using UnityEngine.AI;

namespace Scripts
{
    public class UnitMovement : MonoBehaviour
    {
        bool _shouldMove;
        NavMeshAgent _agent;

        [SerializeField]
        Transform _activityMarker;

        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        internal void OnMovement(Vector3 pos)
        {
            if (!_shouldMove) return; 
 
            _agent.SetDestination(pos);
        }

        internal void ToggelActivity(bool isActive)
        {
            _shouldMove = isActive;
            _activityMarker.gameObject.SetActive(isActive);
        }
    }
}
