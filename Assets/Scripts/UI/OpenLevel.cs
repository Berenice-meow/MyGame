using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OpenLevel : MonoBehaviour
{
    private Button _openLevelButton;

    [SerializeField] private List<int> _levelList = new List<int>();

    protected void Start()
    {
        _openLevelButton = GetComponent<Button>();
        _openLevelButton.onClick.RemoveAllListeners();
        _openLevelButton.onClick.AddListener(OpenFirstLevel);
    }
    
    private void OpenFirstLevel()
    {
        int level = _levelList[0];
        SceneManager.LoadScene(level);
        Time.timeScale = 1f;
    }

    /*
    // Метод для запуска рандомного уровня

    private void OpenRandomLevel()
    {
        int level = _levelList[Random.Range(0, _levelList.Count)];
        SceneManager.LoadScene(level);
    }
    */
}
