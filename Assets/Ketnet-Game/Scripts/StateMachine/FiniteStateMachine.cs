using System;
using UnityEngine;

public abstract class FiniteStateMachine
{
    public event EventHandler CurrentStateChanged;
    IState _currentState;
    public IState CurrentState
    {
        get { return _currentState; }
        set
        {
            if (_currentState == value) return;
            _currentState = value;
            OnStateChanged();
        }
    }

    protected virtual void OnStateChanged()
    {
        CurrentStateChanged?.Invoke(this, EventArgs.Empty);
    }

    public virtual void Update(float deltaTime)
    {
        _currentState.Update(deltaTime);
    }
    
    public virtual void ChangeTo (IState newState)
    {
        if (newState == null || newState == _currentState) return;
        
        _currentState?.OnExit();
        _currentState = newState;
        _currentState.OnEnter();
    }
}
