using UnityEngine;
using UnityEngine.InputSystem;

namespace Cookie.RPG
{
    [RequireComponent(typeof(Player))]
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        Player _player;
        Rigidbody _rigidbody;
        public Vector3 MoveDirection { get; private set; }

        private void Start()
        {
            _player = GetComponent<Player>();
            _rigidbody = GetComponent<Rigidbody>();
        }
        public void OnMove(InputAction.CallbackContext context)
        {
            // 성공적으로 input을 받지 못 했을 경우 Return
            if (context.started) return;

            Vector2 input = context.ReadValue<Vector2>();
            MoveDirection = new Vector3(input.x, 0f, input.y);
        }
        private void FixedUpdate()
        {
            Move();
        }
        private void Move()
        {
            LookAt();
            _rigidbody.velocity = MoveDirection * _player.MoveSpeed;
        }
        private void LookAt()
        {
            if(MoveDirection != Vector3.zero)
            { 
                Quaternion targetAngle = Quaternion.LookRotation(MoveDirection);
                _rigidbody.rotation = targetAngle;
            }
        }
    }
}
