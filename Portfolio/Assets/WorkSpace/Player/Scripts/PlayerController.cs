using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

namespace Cookie.RPG
{
    [RequireComponent(typeof(Player))]
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        Player _player;
        Rigidbody _rigidbody;
        public Vector3 MoveDirection { get; private set; }
        public Vector3 ForwardDirection { get; private set; }
        public UnityEvent OnShoot = new UnityEvent();
        private void Start()
        {
            _player = GetComponent<Player>();
            _rigidbody = GetComponent<Rigidbody>();

            // �ʱ�ȭ
            ForwardDirection = Vector3.forward;
        }
        public void OnMove(InputAction.CallbackContext context)
        {
            // ���������� input�� ���� �� ���� ��� Return
            if (context.started) return;

            Vector2 input = context.ReadValue<Vector2>();
            MoveDirection = new Vector3(input.x, 0f, input.y);
        }
        public void OnAttack(InputAction.CallbackContext context)
        {
            if (!context.performed) return;

            Attack();
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

                // ���� �ٶ󺸰��ִ� ����
                ForwardDirection = MoveDirection;
            }
        }
        private void Attack()
        {
            Debug.Log("Attack");
            OnShoot.Invoke();
        }
    }
}
