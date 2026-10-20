using UnityEngine;

namespace SnackTheData
{
    public class SnackTheDataTailTrigger : MonoBehaviour
    {
        public bool HitTail = false;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.name == "Player")
            {
                HitTail = true;
            }
        }
    }
}
