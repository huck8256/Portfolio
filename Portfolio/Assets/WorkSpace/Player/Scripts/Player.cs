using UnityEngine;

namespace Cookie.RPG
{
    public class Player : MonoBehaviour
    {
        public int MoveSpeed { get => moveSpeed; }

        [SerializeField] protected int moveSpeed;
    }
}

