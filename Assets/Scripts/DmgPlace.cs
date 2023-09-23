using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgPlace : MonoBehaviour
{
    public EventBus _eventBus;

    void Start() {
        _eventBus = EventBus.Instance;
    }
    void OnTriggerEnter (Collider coll) {
        if (coll.CompareTag("Player")) {
            var damagable = CarController.Instance.GetComponent<Damagable>();
            damagable.Damage(1);
            Destroy(transform.parent.gameObject);

            _eventBus.Damaged?.Invoke(damagable.CurrentLife);
        }
    }
}
