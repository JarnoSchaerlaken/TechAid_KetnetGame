using UnityEngine;
namespace SnackTheData{
    public class SnackTheDataPickup : MonoBehaviour
    {
        [SerializeField]
        private GameObject _correspondingTailSegment;

        public int Score = 100;

        private GameObject _player = null;

        [SerializeField]
        private int _dataType = 0; // count starts at 0 

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (_player != null)
            {
                PickedUp(_player.GetComponent<SnackTheDataMovement>());
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.name == "Player")
            {
                _player = collision.gameObject;
            }
        }

        private void PickedUp(SnackTheDataMovement player)
        {
            if (player.Tail.Count == 0)
            {
                GameObject segment = Instantiate(_correspondingTailSegment, player.Player.transform.position - player.Direction * player.TailSegmentSpacing, Quaternion.identity, player.PlayField.transform);
                player.SetSegmentToBeAdded(segment);
            }
            else
            {
                GameObject segment = Instantiate(_correspondingTailSegment, player.Tail[player.Tail.Count - 1].transform.position - player.TailSegmentDirections[player.Tail.Count - 1] * player.TailSegmentSpacing, Quaternion.identity, player.PlayField.transform);
                player.SetSegmentToBeAdded(segment);
            }

            if (_dataType == 0)
            {
                player.TypeOneAmount++;
            }
            else if (_dataType == 1)
            {
                player.TypeTwoAmount++;
            }

            SnackTheDataScoreKeeper.AddScore(Score);
            Destroy(this.gameObject);
        }
    }
}
