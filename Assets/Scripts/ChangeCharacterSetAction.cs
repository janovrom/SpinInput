using UnityEngine;

public class ChangeCharacterSetAction : MonoBehaviour
{

    public CharacterSet characterSet;
    public SpiralInput targetSpiralInput;


    public void ChangeCharacterSet()
    {
        targetSpiralInput.ChangeCharacterSet(characterSet);
    }
}
