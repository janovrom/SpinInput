using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class SpiralInputEventSystem 
{

    public static SpiralInputEventSystem current = new SpiralInputEventSystem();

    private SpiralInputField _activeInputField;
    private Event _currentEvent = new Event();


    public void InputFieldDeselected(SpiralInputField field)
    {
        if (field == _activeInputField)
            _activeInputField = null;
    }

    public void InputFieldSelected(SpiralInputField field)
    {
        _activeInputField = field;
    }

    public void VirtualKeyPressed(char c)
    {
        _currentEvent.keyCode = (KeyCode)c;
        _currentEvent.type = EventType.KeyDown;
        _currentEvent.character = c;

        _activeInputField?.ProcessVirtualKey(_currentEvent);

        _currentEvent = new Event();
    }

    public void VirtualModifierPressed(EventModifiers mod)
    {
        _currentEvent.modifiers = mod;
    }
}
