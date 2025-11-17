using UnityEngine;

public abstract class EntityState
{
    private float _dt;
    protected CommandSystem _commandSystem;

    public EntityState(CommandSystem commandSystem)
    {
        _commandSystem = commandSystem;
    }

    public virtual void EnterState()
    {
        OnEnterState();
    }

    protected virtual void OnEnterState()
    {
        if (_commandSystem != null)
        {
            _commandSystem.CommandTriggered += ProcessCommand;
        }
    }

    public virtual void Update(float dt)
    {
        // override this for menus or global dt calculations
        _dt = dt * Globals.GameTimeScale;
    }

    public virtual void ExitState()
    {
        OnExitState();
    }

    protected virtual void OnExitState()
    {
        if (_commandSystem != null)
        {
            _commandSystem.CommandTriggered -= ProcessCommand;
        }
    }

    protected virtual void ProcessCommand(Command command) { }
}
