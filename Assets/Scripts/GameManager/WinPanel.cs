using UnityEngine;

namespace MyGame.GameManager
{
    public class WinPanel : MonoBehaviour
    {
        [SerializeField] private GameManager _gameManager;

        [SerializeField] private AudioSource _winSound;

        protected void Start()
        {
            _winSound.Play();

            _gameManager.Win += ShowPanel;
            gameObject.SetActive(false);
        }

        private void ShowPanel()
        {
            gameObject.SetActive(true);
        }
    }
}