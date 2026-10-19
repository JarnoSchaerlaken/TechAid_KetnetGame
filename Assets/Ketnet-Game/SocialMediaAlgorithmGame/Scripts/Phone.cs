using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SocialMediaAlgorithmGame
{
    public class Phone : MonoBehaviour
    {
        [SerializeField]AlgorithmGame _game;
        [SerializeField]Dictionary<AlgorithmTopic, float> _opinions = new();

        [SerializeField]TMP_Text _text;
        [SerializeField]GameObject _textBoxObject;

        public void AddOpinion (AlgorithmTopic topic, float opinion)
        {
            _opinions.Add(topic, opinion);
        }

        public void GiveTopic (AlgorithmTopic topic)
        {
            if (_opinions[topic] < 0.4)
            {
                _text.text = topic.Name.FirstCharacterToUpper() + " is slecht";
                _game.AddScore(-25);
            }
            else if (_opinions[topic] < 0.6)
            {
                _text.text = "Ik heb geen mening over "  + topic.Name.FirstCharacterToLower();
            }
            else
            {
                _text.text = topic.Name.FirstCharacterToUpper() + " is goed";
                _game.AddScore(50);
            }
            _textBoxObject.SetActive(true);
            _game.RemoveTutorial();
        }
    }
}
