using UnityEngine;
namespace SnackTheData
{
    public class SnackTheDataPickup : MonoBehaviour
    {
        [SerializeField]
        private GameObject _correspondingTailSegment;

        public int AddedScore = 100;

        [SerializeField]
        private int _dataType = 0; // count starts at 0 

        public float TailSegmentSpacing = 65f;


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.name == "Player")
            {
                PickedUp(collision.gameObject.GetComponent<SnackTheDataMovement>());
            }
        }

        private void PickedUp(SnackTheDataMovement player)
        {
            if (player.Tail.Count == 0)
            {
                GameObject segment = Instantiate(_correspondingTailSegment, player.Player.transform.position - player.Direction * TailSegmentSpacing, Quaternion.identity, player.PlayField.transform);

                player.Tail.Add(segment);

                player.TailSegmentDirections.Add(Vector3.zero);
            }
            else
            {
                GameObject segment = Instantiate(_correspondingTailSegment, player.Tail[player.Tail.Count - 1].transform.position - player.TailSegmentDirections[player.Tail.Count - 1] * TailSegmentSpacing, Quaternion.identity, player.PlayField.transform);

                player.Tail.Add(segment);

                player.TailSegmentDirections.Add(Vector3.zero);
            }
            
            if (_dataType == 0)
            {
                SnackTheDataStateTracker.TypeOneAmount++;
            }
            else if (_dataType == 1)
            {
                SnackTheDataStateTracker.TypeTwoAmount++;
            }

            if (!(SnackTheDataStateTracker.TypeOneAmount - SnackTheDataStateTracker.TypeTwoAmount >= SnackTheDataStateTracker.AllowedAmountBetweenHighestAndLowest || SnackTheDataStateTracker.TypeTwoAmount - SnackTheDataStateTracker.TypeOneAmount >= SnackTheDataStateTracker.AllowedAmountBetweenHighestAndLowest))
            {
                SnackTheDataScoreKeeper.AddScore(AddedScore);
            }
            
            Destroy(this.gameObject);
        }
    }
}
