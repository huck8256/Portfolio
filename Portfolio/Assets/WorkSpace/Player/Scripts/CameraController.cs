using UnityEngine;

namespace Cookie.RPG
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] Transform _target;

        Vector3 _originPosition;
        void Start() => _originPosition = transform.position;
        void FixedUpdate() => transform.position = _originPosition + _target.position;
    }
}
