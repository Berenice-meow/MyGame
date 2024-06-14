using UnityEngine;

namespace MyGame.GameManager
{
    public class LosePanel : MonoBehaviour
    {
        [SerializeField] private GameManager _gameManager;

        [SerializeField] private AudioSource _loseSound;

        protected void Start()
        {
            _loseSound.Play();

            _gameManager.Lose += ShowPanel;
            gameObject.SetActive(false);
        }

        private void ShowPanel()
        {
            gameObject.SetActive(true);
        }
    }
}