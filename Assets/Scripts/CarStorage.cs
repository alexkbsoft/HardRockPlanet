using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Car storage")]
public class CarStorage : ScriptableObject
{
    public float Resources = 0;
    public float Fuel = 1000;
    public int SpeedLevel = 1;
    public int DamageLevel = 1;

    public void Reset()
    {
        Resources = 0;
        Fuel = 5000;
        SpeedLevel = 1;
        DamageLevel = 1;
    }
}
