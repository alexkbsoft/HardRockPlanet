using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventBus : MonoBehaviour
{
    public UnityEvent<Mine> EnterMine;
    public UnityEvent<Mine> ExitMine;
    public UnityEvent<float> Damaged;
    public UnityEvent<string> PopupText;

    public static EventBus Instance;

    void Awake() {
        Instance = this;
    }
    
}
