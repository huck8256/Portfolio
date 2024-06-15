using System.Collections.Generic;
using UnityEngine;

namespace Cookie.RPG
{
    [RequireComponent(typeof(PlayerController))]
    public class BulletManager : MonoBehaviour
    {
        [SerializeField] GameObject _bulletPrefab;
        [SerializeField] int _maxCount;
        [SerializeField] float _power;
        PlayerController _playerController;

        List<GameObject> _bulletList = new List<GameObject>();

        void Start()
        {
            _playerController = GetComponent<PlayerController>();
            InitBullet();
            _playerController.OnShoot.AddListener(() => Shoot());
        }
        void InitBullet()
        {
            for (int i = 0; i < _maxCount; i++)
            {
                GameObject obj = Instantiate(_bulletPrefab, transform);
                obj.SetActive(false);
                _bulletList.Add(obj);
            }
        }
        void Shoot()
        {
            for(int i = 0; i < _maxCount; i++)
            {
                if (!_bulletList[i].activeSelf)
                {
                    _bulletList[i].transform.position = transform.position;
                    _bulletList[i].GetComponent<Rigidbody>().velocity = _playerController.ForwardDirection * _power;
                    _bulletList[i].SetActive(true);
                    break;
                }
            }
        }
    }
}
