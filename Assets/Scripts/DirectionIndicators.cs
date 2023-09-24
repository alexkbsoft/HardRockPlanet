using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionIndicators : MonoBehaviour
{
    public List<GameObject> Arrows;
    public List<GameObject> Mines;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = CarController.Instance.transform.position + Vector3.up * 10;

        Mines.Sort(delegate (GameObject a, GameObject b) {
            var dist1 = Vector3.Distance(a.transform.position, transform.position);
            var dist2 = Vector3.Distance(b.transform.position, transform.position);

            return dist1.CompareTo(dist2);
        });

        for (int i = 0; i < 3; i++) {
            var arr = Arrows[i];
            var minteTrans = Mines[i].transform;

            var dir = (minteTrans.position - transform.position).normalized;
            arr.transform.LookAt(minteTrans);
            arr.transform.position = transform.position + dir * 10;
        }
    }
}
