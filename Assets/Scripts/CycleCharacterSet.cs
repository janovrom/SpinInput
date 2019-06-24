using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CycleCharacterSet : MonoBehaviour
{

    public SpiralInput targetSpiralInput;
    public List<CharacterSet> sets;
    public List<string> displayTexts;

    private int _currentSet = 0;


    private void Start()
    {
        targetSpiralInput.ChangeCharacterSet(sets[0]);
        var t = GetComponentInChildren<TMPro.TextMeshProUGUI>(); 
        t.SetText(displayTexts[0]);
    }

    public void ChangeCharacterSet()
    {
        ++_currentSet;
        _currentSet = _currentSet % sets.Count;

        var t = GetComponentInChildren<TMPro.TextMeshProUGUI>();
        t.SetText(displayTexts[_currentSet]);
        targetSpiralInput.ChangeCharacterSet(sets[_currentSet]);
    }

}
