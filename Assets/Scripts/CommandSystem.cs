using UnityEngine.InputSystem;

public class CommandSystem
{
    public delegate void OnCommand(Command command);
    public event OnCommand CommandTriggered;

    protected virtual void TriggerCommand(Command command)
    {
        CommandTriggered(command);
    }
}

public class ControlMapping
{
    // TODO will have to update these since keyboard or controller could be null
    public UnityEngine.InputSystem.Controls.KeyControl Key;
    public UnityEngine.InputSystem.Controls.ButtonControl Button;
    public Command PressedCommand;
    public Command HeldCommand;
    public Command ReleasedCommand;

    public ControlMapping(UnityEngine.InputSystem.Controls.KeyControl key,
                            UnityEngine.InputSystem.Controls.ButtonControl button,
                            Command pressedCommand,
                            Command heldCommand,
                            Command releasedCommand)
    {
        Key = key;
        Button = button;
        PressedCommand = pressedCommand;
        HeldCommand = heldCommand;
        ReleasedCommand = releasedCommand;
    }
}

public class PlayerControllerCommandSystem : CommandSystem
{
    public void Update(float dt)
    {
        UnityEngine.InputSystem.Gamepad gamepad = Gamepad.current;
        UnityEngine.InputSystem.Keyboard keyboard = Keyboard.current;

        if (keyboard != null)
        {
            if (keyboard.aKey.isPressed) { TriggerCommand(new LeftPressedCommand()); }
            if (keyboard.aKey.wasReleasedThisFrame) { TriggerCommand(new LeftReleasedCommand()); }
            if (keyboard.dKey.isPressed) { TriggerCommand(new RightPressedCommand()); }
            if (keyboard.dKey.wasReleasedThisFrame) { TriggerCommand(new RightReleasedCommand()); }
            if (keyboard.spaceKey.wasPressedThisFrame) { TriggerCommand(new JumpPressedCommand()); }
            if (keyboard.spaceKey.wasReleasedThisFrame) { TriggerCommand(new JumpReleasedCommand()); }
            if (keyboard.leftShiftKey.wasPressedThisFrame) { TriggerCommand(new DashPressedCommand()); }
            if (keyboard.leftShiftKey.isPressed) { TriggerCommand(new DashHeldCommand()); }
            if (keyboard.leftShiftKey.wasReleasedThisFrame) { TriggerCommand(new DashReleasedCommand()); }
            if (keyboard.eKey.wasPressedThisFrame) { TriggerCommand(new ShootStartCommand()); }
            if (keyboard.eKey.wasReleasedThisFrame) { TriggerCommand(new ShootEndCommand()); }
        }

        if (gamepad != null)
        {
            if (gamepad.dpad.left.isPressed) { TriggerCommand(new LeftPressedCommand()); }
            if (gamepad.dpad.left.wasReleasedThisFrame) { TriggerCommand(new LeftReleasedCommand()); }
            if (gamepad.dpad.right.isPressed) { TriggerCommand(new RightPressedCommand()); }
            if (gamepad.dpad.right.wasReleasedThisFrame) { TriggerCommand(new RightReleasedCommand()); }
            if (gamepad.buttonSouth.wasPressedThisFrame) { TriggerCommand(new JumpPressedCommand()); }
            if (gamepad.buttonSouth.wasReleasedThisFrame) { TriggerCommand(new JumpReleasedCommand()); }
            if (gamepad.buttonEast.wasPressedThisFrame) { TriggerCommand(new DashPressedCommand()); }
            if (gamepad.buttonEast.isPressed) { TriggerCommand(new DashHeldCommand()); }
            if (gamepad.buttonEast.wasReleasedThisFrame) { TriggerCommand(new DashReleasedCommand()); }
            if (gamepad.buttonWest.wasPressedThisFrame) { TriggerCommand(new ShootStartCommand()); }
            if (gamepad.buttonWest.wasReleasedThisFrame) { TriggerCommand(new ShootEndCommand()); }
            if (gamepad.dpad.up.wasPressedThisFrame) { }
            if (gamepad.dpad.down.wasPressedThisFrame) { }
            if (gamepad.rightTrigger.wasPressedThisFrame) { }
            if (gamepad.rightShoulder.wasPressedThisFrame) { }
            if (gamepad.rightStickButton.wasPressedThisFrame) { }
            if (gamepad.leftTrigger.wasPressedThisFrame) { }
            if (gamepad.leftShoulder.wasPressedThisFrame) { }
            if (gamepad.leftStickButton.wasPressedThisFrame) { }
        }
    }
}