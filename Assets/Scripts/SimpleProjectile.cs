using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using Lean.Pool;


public class SimpleProjectile : MonoBehaviour
{
    public float Speed = 50.0f;
    public float RaycastAdvance = 2f;
    public float Damage = 20.0f;
    public LayerMask layerMask;
    public List<string> TargetTags;

    [SerializeField] GameObject _flarePrefab;
    [SerializeField] GameObject _bloodPrefab;
    TrailRenderer _trail;

    void Awake()
    {
        // _trail = GetComponent<TrailRenderer>();
    }

    void Update()
    {
        transform.position += transform.forward * Speed * Time.deltaTime;
        Vector3 step = transform.forward * Time.deltaTime * Speed;

        if (Physics.SphereCast(transform.position - transform.forward,
            0.1f,
            transform.forward,
            out var hitPoint,
            step.magnitude * RaycastAdvance,
            layerMask))
        {
            Damagable damagable = hitPoint.collider.gameObject.GetComponent<Damagable>();
            if (damagable && TargetTags.Contains(hitPoint.collider.tag))
            {
                damagable.Damage(Damage);
            }

            if (hitPoint.collider.tag == "Enemy")
            {
                var blood = Instantiate(_bloodPrefab);

                if (blood != null)
                {
                    blood.transform.position = hitPoint.point;
                    blood.transform.rotation = Quaternion.LookRotation(hitPoint.normal);
                }
            }

            Destroy(gameObject);

            var flare = Instantiate(_flarePrefab);

            if (flare != null)
            {
                flare.transform.position = hitPoint.point;
                flare.transform.rotation = Quaternion.LookRotation(hitPoint.normal);
            }
        }
    }

    void OnEnable()
    {
        // _trail.Clear();
    }
}
