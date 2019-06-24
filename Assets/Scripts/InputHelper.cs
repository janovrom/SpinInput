using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputHelper : MonoBehaviour
{

    public bool press;
    public Button button;


    void Update()
    {
        if (press)
        {
            button.onClick.Invoke();
            press = false;
        }        
    }
}
