using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    public float Radius = 1.0f;
    public List<Spawner> Spawners;
    public EventBus EventBus;
    void Start()
    {
        foreach(Spawner s in GetComponentsInChildren<Spawner>()) {
            Spawners.Add(s);
        }
        StartCoroutine(CheckPlayer());
    }

    private IEnumerator CheckPlayer() {
        while(true) {
            yield return new WaitForSeconds(0.5f);

            var player = CarController.Instance;

            if (Vector3.Distance(player.transform.position, transform.position) <= Radius) {
                EventBus.EnterMine?.Invoke(this);
            }
        }
    }

    private void OnDrawGizmos()
    {
        UnityEditor.Handles.color = Color.green;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.up, Radius);
    }
}
