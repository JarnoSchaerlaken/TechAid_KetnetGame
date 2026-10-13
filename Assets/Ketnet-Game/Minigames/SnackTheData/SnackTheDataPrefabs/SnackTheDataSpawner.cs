using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace SnackTheData 
{
    public class SnackTheDataSpawner : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> _pickupPrefabs = new List<GameObject>();

        private List<GameObject> _spawnedPickups = new List<GameObject>();

        [SerializeField]
        private GameObject _warningType1;

        [SerializeField]
        private GameObject _warningType2;

        private int _amountPickup1;

        private int _amountPickup2;

        [SerializeField]
        private Image _playField;

        [SerializeField]
        private SnackTheDataMovement _player;

        [SerializeField]
        private int _maxPickups = 9;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            Canvas.ForceUpdateCanvases();
        }

        // Update is called once per frame
        void Update()
        {
            for (int i = _spawnedPickups.Count - 1; i >= 0; i--)
            {
                if (_spawnedPickups[i] == null)
                {
                    _spawnedPickups.RemoveAt(i);
                }
            }

            _amountPickup1 = 0;
            _amountPickup2 = 0;
            foreach (GameObject pickup in _spawnedPickups)
            {
                if (pickup.GetComponent<SnackTheDataTypeHolder>().Datatype == 0)
                {
                    _amountPickup1++;
                }
                else if (pickup.GetComponent<SnackTheDataTypeHolder>().Datatype == 1)
                {
                    _amountPickup2++;
                }
            }

            if (_player.TypeOneAmount - _player.TypeTwoAmount >= _player.AllowedAmountBetweenHighestAndLowest - 1)
            {
                _warningType1.SetActive(true);
            }
            else
            {
                _warningType1.SetActive(false);
            }

            if (_player.TypeTwoAmount - _player.TypeOneAmount >= _player.AllowedAmountBetweenHighestAndLowest - 1)
            {
                _warningType2.SetActive(true);
            }
            else
            {
                _warningType2.SetActive(false);
            }

            if (_spawnedPickups.Count < _maxPickups)
            {
                SpawnPickup();
            }
        }

        public void SpawnPickup()
        {
            float x = Random.Range(0.05f, 0.45f);
            float y = Random.Range(0.05f, 0.95f);

            Vector3 position = Camera.main.ViewportToScreenPoint(new Vector3(x, y, 0));

            if (_amountPickup1 >= _maxPickups - _player.AllowedAmountBetweenHighestAndLowest * 2)
            {
                _spawnedPickups.Add(Instantiate(_pickupPrefabs[1], position, Quaternion.identity, _playField.transform));
            }
            else if (_amountPickup2 >= _maxPickups - _player.AllowedAmountBetweenHighestAndLowest * 2)
            {
                _spawnedPickups.Add(Instantiate(_pickupPrefabs[0], position, Quaternion.identity, _playField.transform));
            }
            else
            {
                int z = Random.Range(0, _pickupPrefabs.Count);

                _spawnedPickups.Add(Instantiate(_pickupPrefabs[z], position, Quaternion.identity, _playField.transform));
            }
        }
    }
}
