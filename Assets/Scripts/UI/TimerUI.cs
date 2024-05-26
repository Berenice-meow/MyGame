using UnityEngine;
using TMPro;
using System;

public class TimerUI : MonoBehaviour
{
    public event Action TimeEnd;

    [SerializeField] private TextMeshProUGUI _outputText;
    private string _format;

    [field: SerializeField] public float GameDurationSeconds {  get; private set; }

    public float TimerSeconds { get; private set; }

    private bool _timerEnd;

    protected void Start()
    {
        _format = _outputText.text;
        _timerEnd = false;
    }

    protected void Update()
    {
        if (_timerEnd) return;      //  Если таймер закончился, то дальше мы не будем считать время

        TimerSeconds += Time.deltaTime;
        if (TimerSeconds >= GameDurationSeconds)
        {
            TimeEnd?.Invoke();
            _timerEnd = true;
        }

        int time = (int)(GameDurationSeconds - TimerSeconds);
        _outputText.text = string.Format(_format, time / 60, time % 60);
    }
}
