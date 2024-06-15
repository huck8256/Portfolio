using UnityEngine;

namespace Cookie.RPG
{
    public class Player : MonoBehaviour
    {
        public int MoveSpeed { get => _moveSpeed; }

        [SerializeField] protected int _moveSpeed;
    }
}

