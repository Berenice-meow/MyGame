using MyGame.Enemy;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MyGame.GameManager
{
    public class GameManager : MonoBehaviour
    {
        public event Action Win;
        public event Action Lose;

        public PlayerCharacterView Player {  get; private set; }
        public List<EnemyCharacterView> Enemies { get; private set;}

        public TimerUI Timer { get; private set; }

        protected void Start()
        {
            Player = FindObjectOfType<PlayerCharacterView>();
            Enemies = FindObjectsOfType<EnemyCharacterView>().ToList();
            Timer = FindObjectOfType<TimerUI>();

            Player.Dead += OnPlayerDead;

            foreach (var enemy in Enemies)
                enemy.Dead += OnEnemyDead;

            Timer.TimeEnd += PlayerLose;

            Time.timeScale = 1f;
        }

        private void OnPlayerDead(BaseCharacterView sender)
        {
            Player.Dead -= OnPlayerDead;
            Lose?.Invoke();
            Time.timeScale = 0f;
        }

        private void OnEnemyDead(BaseCharacterView sender)
        {
            var enemy = sender as EnemyCharacterView;
            Enemies.Remove(enemy);

            enemy.Dead -= OnEnemyDead;

            if (Enemies.Count == 0)
            {
                Win?.Invoke();
                Time.timeScale = 0f;
            }
        }

        private void PlayerLose()
        {
            Timer.TimeEnd -= PlayerLose;
            Lose?.Invoke();
            Time.timeScale = 0f;
        }
    }
}