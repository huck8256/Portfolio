using UnityEngine;

namespace Cookie.RPG
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : MonoBehaviour
    {
        [SerializeField] float _disableTime;

        Rigidbody _rigidbody;
        float _time;
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
        private void OnDisable()
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
            _time = 0f;
        }
        private void Update()
        {
            _time += Time.deltaTime;
            if (_time >= _disableTime)
                gameObject.SetActive(false);
        }
    }
}
