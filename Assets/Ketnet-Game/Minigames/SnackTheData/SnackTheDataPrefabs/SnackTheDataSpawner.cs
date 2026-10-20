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

        private int _amountPickup1;

        private int _amountPickup2;

        [SerializeField]
        private Image _playField;

        [SerializeField]
        private int _maxPickups = 25;

        public static bool Enabled = false;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            Canvas.ForceUpdateCanvases();
        }

        // Update is called once per frame
        void Update()
        {
            if (!Enabled) return;
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

            if (_spawnedPickups.Count < _maxPickups)
            {
                SpawnPickup();
            }
        }

        public void SpawnPickup()
        {
            float x = Random.Range(0.05f, 0.75f);
            float y = Random.Range(0.05f, 0.90f);

            Vector3 playerViewportPosition = Camera.main.ScreenToViewportPoint(SnackTheDataStateTracker.Instance.Player.transform.position);

            if (x > playerViewportPosition.x - 0.05f && x < playerViewportPosition.x + 0.05f && y > playerViewportPosition.y - 0.05f && y < playerViewportPosition.y + 0.05f) return;

            Vector3 position = Camera.main.ViewportToScreenPoint(new Vector3(x, y, 0));

            if (_amountPickup1 >= _maxPickups - SnackTheDataStateTracker.AllowedAmountBetweenHighestAndLowest * 3)
            {
                _spawnedPickups.Add(Instantiate(_pickupPrefabs[1], position, Quaternion.identity, _playField.transform));
            }
            else if (_amountPickup2 >= _maxPickups - SnackTheDataStateTracker.AllowedAmountBetweenHighestAndLowest * 3)
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
