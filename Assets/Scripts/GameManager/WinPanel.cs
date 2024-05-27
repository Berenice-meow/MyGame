﻿using UnityEngine;

namespace MyGame.GameManager
{
    public class WinPanel : MonoBehaviour
    {
        [SerializeField] private GameManager _gameManager;

        protected void Start()
        {
            _gameManager.Win += ShowPanel;
            gameObject.SetActive(false);
        }

        private void ShowPanel()
        {
            gameObject.SetActive(true);
        }
    }
}