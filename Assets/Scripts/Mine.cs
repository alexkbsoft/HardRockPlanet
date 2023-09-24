using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding.Util;

public class Mine : MonoBehaviour
{
    public string MineName;
    public float Radius = 1.0f;
    public int Income = 20;
    public int SecondsToDefend = 30;
    public float WaveMomentSeconds = 4;
    public List<Spawner> Spawners;
    // public EventBus EventBus;

    private bool _entered = false;


    void Start()
    {
        foreach (Spawner s in GetComponentsInChildren<Spawner>())
        {
            Spawners.Add(s);
        }
        StartCoroutine(CheckPlayer());
    }

    private IEnumerator CheckPlayer()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);

            var player = CarController.Instance;

            if (Vector3.Distance(player.transform.position, transform.position) <= Radius)
            {
                _entered = true;
                EventBus.Instance.EnterMine?.Invoke(this);
            }

            if (_entered && Vector3.Distance(player.transform.position, transform.position) > Radius)
            {
                _entered = false;
                EventBus.Instance.ExitMine?.Invoke(this);
            }
        }
    }

    private void OnDrawGizmos()
    {
        UnityEditor.Handles.color = Color.green;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.up, Radius);
    }
}
