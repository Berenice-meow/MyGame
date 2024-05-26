using UnityEngine;

namespace MyGame.GameManager
{
    public class LosePanel : MonoBehaviour
    {
        [SerializeField] private GameManager _gameManager;

        protected void Start()
        {
            _gameManager.Lose += ShowPanel;
            gameObject.SetActive(false);
        }

        private void ShowPanel()
        {
            gameObject.SetActive(true);
        }
    }
}