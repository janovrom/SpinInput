using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[System.Serializable]
public class VirtualKey
{

    public string displayName;

    public void DoVirtualKeyAction()
    {
        SpiralInputEventSystem.current.VirtualKeyPressed(displayName[0]);
    }
}

