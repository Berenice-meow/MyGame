using MyGame.Exceptions;
using System.Collections.Generic;

namespace MyGame.FSM
{
    public class BaseStateMachine
    {
        private BaseState _currentState;

        private List<BaseState> _states;                                // Список состояний
        private Dictionary<BaseState, List<Transition>> _transitions;   // Переходы между состояниями: из BaseState в другие состояния (список Transition, где содержится куда и при каком условии)
        
        public BaseStateMachine ()                                      // Делаем конструктор по умолчанию, в котором инициализируем поля
        {
            _states = new List<BaseState>();
            _transitions = new Dictionary<BaseState, List<Transition>>();
        }

        public void SetInitialState(BaseState state)                    // Стартовое состояние
        {
            _currentState = state;
        }

        public void AddState(BaseState state, List<Transition> transitions)     // Метод, который будет добавлять новые состояния
        {
            if (!_states.Contains(state))                               // Проверяем не содержит ли уже список состояний добавляемое состояние
            {
                _states.Add(state);
                _transitions.Add(state, transitions);
            }
            else
            {
                throw new AlreadyExistException($"State {state.GetType()} already exists in state machine");
            }
        }

        public void Update()                    // Метод, который будет обновлять State Machine каждый кадр
        {
            foreach (var transition in _transitions[_currentState]) 
            {
                if (transition.Condition())
                {
                    _currentState = transition.ToState;
                    break;
                }
            }
            _currentState.Execute();
        }
    }
}
