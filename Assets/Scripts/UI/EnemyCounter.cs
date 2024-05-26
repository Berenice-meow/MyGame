using UnityEngine;
using TMPro;
using MyGame.Enemy;
using System.Linq;
using System.Collections.Generic;

namespace MyGame.UI
{
    public class EnemyCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _outputText;
        private string _format;

        public List<EnemyCharacter> Enemies { get; private set; }

        private void Start()
        {
            _format = _outputText.text;
        }

        private void Update()
        {
            Enemies = FindObjectsOfType<EnemyCharacter>().ToList();
            _outputText.text = string.Format(_format, Enemies.Count.ToString());
        }
    }
}