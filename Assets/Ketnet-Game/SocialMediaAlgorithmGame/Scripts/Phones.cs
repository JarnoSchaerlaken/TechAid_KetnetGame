using UnityEngine;

namespace SocialMediaAlgorithmGame
{
    public class Phones : MonoBehaviour
    {
        public static Phones Instance;
        [SerializeField]GameObject[] _phones;
        [SerializeField]float _consumeDistance;

        void Awake()
        {
            Instance = this;
        }

        public GameObject GetClosestPhone (Vector3 pos)
        {
            GameObject closestPhone = null;
            float smallestDistance = 999999999999;
            foreach(GameObject p in _phones)
            {
                if (Vector3.Distance(pos, p.transform.position) < smallestDistance)
                {
                    smallestDistance = Vector3.Distance(pos, p.transform.position);
                    closestPhone = p;
                }
            }

            if (smallestDistance <= _consumeDistance)
            {
                return closestPhone;
            }
            return null;
        }
    }
}
