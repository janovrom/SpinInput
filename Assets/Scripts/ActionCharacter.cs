using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionCharacter : MonoBehaviour
{

    public KeyCode code;
    public EventModifiers modifier;


    public void DoAction()
    {
        SpiralInputEventSystem.current.VirtualModifierPressed(modifier);
        SpiralInputEventSystem.current.VirtualKeyPressed((char)code);
    }

}
