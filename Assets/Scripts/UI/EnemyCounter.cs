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
        private int _counter;

        public List<EnemyCharacter> Enemies { get; private set; }

        private void Start()
        {
            Enemies = FindObjectsOfType<EnemyCharacter>().ToList();
            foreach (var enemy in Enemies)
                enemy.Dead += OnEnemyDead;

            _format = _outputText.text;
            _counter = Enemies.Count;
            _outputText.text = string.Format(_format, _counter);
        }

        private void OnEnemyDead(BaseCharacter sender)
        {
            var enemy = sender as EnemyCharacter;
            enemy.Dead -= OnEnemyDead;
            _outputText.text = string.Format(_format, --_counter);
        }
    }
}