using Kor.Infra;
using UnityEngine;
using UnityEngine.InputSystem;
/*
namespace Kor.Controls
{
    //TODO remove this class !
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private InputActionReference _moveAction;
    
        private Vector3 _movementDirection;

        private IEventManager _eventManager;

        private void Awake()
        {
            _eventManager = ServiceLocator.Resolve<EventManager>();
        }
        
        private void Update()
        {
            _movementDirection = _moveAction.action.ReadValue<Vector3>();
        }
    
        private void FixedUpdate()
        {
            _rigidbody.velocity += _movementDirection * _moveSpeed;
        }

        private void OnCollisionEnter(Collision other)
        {
            //Debug.Log(other.gameObject.name);
            _eventManager.TriggerCollisionWithObject("m");
        }
    }
}
*/
