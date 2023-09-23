using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class Enemy : MonoBehaviour
{
    private Damagable _damagable;
    void Start()
    {
        _damagable = GetComponent<Damagable>();
        _damagable.OnDestroyed?.AddListener(Destroyed);

        var destSetter = GetComponent<AIDestinationSetter>();
        destSetter.target = CarController.Instance.transform;
    }

    private void Destroyed() {
        Destroy(gameObject);
    }
}
