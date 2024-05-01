using System;

namespace MyGame.FSM
{
    public class Transition
    {
        public BaseState ToState { get; }       // Состояние
        public Func<bool> Condition { get; }    // Делегат, в который мы будем записывать условие, при котором выполняется переход в другое состояние

        public Transition (BaseState toState, Func<bool> condition)     // Конструктор
        {
            ToState = toState;
            Condition = condition;
        }
    }
}
