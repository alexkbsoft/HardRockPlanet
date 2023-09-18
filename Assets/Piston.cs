using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piston : MonoBehaviour
{
    public int pistonForce;
    private float currentTime;
    private Vector3 pistonVector = new Vector3(0,1,0);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > 3)
        {
            currentTime = 0;
            GetComponent<Rigidbody>().AddForce(pistonVector * pistonForce, ForceMode.Impulse);
        }
    }
}
