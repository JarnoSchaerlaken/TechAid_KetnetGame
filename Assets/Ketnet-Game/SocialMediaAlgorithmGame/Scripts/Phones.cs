using UnityEngine;

namespace SocialMediaAlgorithmGame
{
    public class Phones : MonoBehaviour
    {
        public static Phones Instance;
        [SerializeField]GameObject[] _phones;
        [SerializeField]AlgorithmGame _game;
        [SerializeField]float _consumeDistance;

        void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            foreach (AlgorithmTopic at in _game.Topics)
            {
                Vector3 randomVector = new(Random.value, Random.value, Random.value);
                randomVector.Normalize();

                randomVector.x *= randomVector.x;
                randomVector.y *= randomVector.y;
                randomVector.z *= randomVector.z;

                randomVector *= 1.75f;
                
                _phones[0].GetComponent<Phone>().AddOpinion(at, randomVector.x);
                _phones[1].GetComponent<Phone>().AddOpinion(at, randomVector.y);
                _phones[2].GetComponent<Phone>().AddOpinion(at, randomVector.z);
            }
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
