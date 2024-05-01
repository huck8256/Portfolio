using UnityEngine;
using UnityEngine.Events;

public class InputKeyController : MonoBehaviour
{
    public KeyCode KeyCode;
    public UnityEvent KeyDownEvent;
    public UnityEvent KeyUpEvent;
    public UnityEvent OddEvent;
    public UnityEvent EvenEvent;

    bool isOddNumber; // È¦¼ö

    void Update()
    {
        if(Input.GetKeyDown(KeyCode))
        {
            ReverseOddNumber();
            KeyDownEvent.Invoke();
        }
        if(Input.GetKeyUp(KeyCode))
        {
            KeyUpEvent.Invoke();
        }
    }
    public void ReverseOddNumber()
    {
        if (isOddNumber)
        {
            isOddNumber = false;
            EvenEvent.Invoke();
        }
        else
        {
            isOddNumber = true;
            OddEvent.Invoke();
        }
    }
}
