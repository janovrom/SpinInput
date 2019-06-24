using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="New Character Set", menuName ="Character Set")]
public class CharacterSet : ScriptableObject
{

    public List<VirtualKey> actions;

    public VirtualKey this[int index]
    {
        get
        {
            return actions[index];
        }
    }

    public int Count
    {
        get
        {
            if (actions == null)
                return 0;

            return actions.Count;
        }
    }
    
}
