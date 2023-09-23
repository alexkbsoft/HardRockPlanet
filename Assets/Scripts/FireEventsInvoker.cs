using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class FireEventsInvoker : MonoBehaviour
{
    public UnityEvent FireLeftEv;
    public UnityEvent FireRightEv;

    public void FireLeft()
    {
        FireLeftEv?.Invoke();
    }

    public void FireRight()
    {
        FireRightEv?.Invoke();
    }
}
