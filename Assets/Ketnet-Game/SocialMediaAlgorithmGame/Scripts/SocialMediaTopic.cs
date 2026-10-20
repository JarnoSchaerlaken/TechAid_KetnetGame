using UnityEngine;

namespace SocialMediaAlgorithmGame
{
    public class SocialMediaTopic : DraggableObject
    {
        [SerializeField]float _xSpeed;
        [SerializeField]float _ySpeed;
        [SerializeField]SpriteRenderer _renderer;
        [HideInInspector]public Phones Phones;
        AlgorithmTopic _topic;

        Vector2 _homePosition;
        Vector2 _draggedToPosition;
        float _positionT = 999;

        float _startHeight;

        public override void Start()
        {
            _startHeight = transform.position.y;

            base.Start();
        }

        public override void Update()
        {
            if (_positionT < 1)
            {
                _positionT += 3 * Time.deltaTime;
                transform.position = Vector3.Lerp(_draggedToPosition, _homePosition, _positionT);
            }
            else if (!Dragging)
            {
                transform.position += _xSpeed * Time.deltaTime * Vector3.right;
                transform.position += _ySpeed * Time.deltaTime * Mathf.Sin(transform.position.x) * Vector3.up;
            }

            if (transform.position.x > 10)
            {
                Destroy(gameObject);
            }

            base.Update();
        }

        public override void OnStartDrag()
        {
            _homePosition = transform.position;

            base.OnStartDrag();
        }
        public override void OnStopDrag()
        {
            _draggedToPosition = transform.position;
            _positionT = 0;

            GameObject p = Phones.GetClosestPhone(transform.position);
            if (p != null)
            {
                p.GetComponent<Phone>().GiveTopic(_topic);

                Destroy(gameObject);
            }

            base.OnStopDrag();
        }

        public void SetTopic (AlgorithmTopic topic)
        {
            _renderer.color = topic.TopicColor;
            _topic = topic;
        }
    }
}
