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

        public PlayerCharacter Player {  get; private set; }
        public List<EnemyCharacter> Enemies { get; private set;}

        public TimerUI Timer { get; private set; }

        protected void Start()
        {
            Player = FindObjectOfType<PlayerCharacter>();
            Enemies = FindObjectsOfType<EnemyCharacter>().ToList();
            Timer = FindObjectOfType<TimerUI>();

            Player.Dead += OnPlayerDead;

            foreach (var enemy in Enemies)
                enemy.Dead += OnEnemyDead;

            Timer.TimeEnd += PlayerLose;
        }

        private void OnPlayerDead(BaseCharacter sender)
        {
            Player.Dead -= OnPlayerDead;
            Lose?.Invoke();
            Time.timeScale = 0f;
        }

        private void OnEnemyDead(BaseCharacter sender)
        {
            var enemy = sender as EnemyCharacter;
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