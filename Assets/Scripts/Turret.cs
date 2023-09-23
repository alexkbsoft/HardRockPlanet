using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject BulletPrefab;
    public GameObject MuzzlePrefab;
    public Transform BarrelL;
    public Transform BarrelR;
    public Animator Animator;
    public float RotationSpeed = 1.0f;

    void Start()
    {

    }

    void Update()
    {
        Aim();

        Animator.SetBool("Fire", Input.GetMouseButton(0) && Input.GetMouseButton(1));
    }

    private void Aim()
    {
        if (!Input.GetMouseButton(1))
        {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("Ground")))
        {
            Vector3 target = hit.point;

            Quaternion wantedRotation = Quaternion.LookRotation(target - transform.position, Vector3.up);
            var eulerAngles = wantedRotation.eulerAngles;
            eulerAngles.x = 0;

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.Euler(eulerAngles),
                Time.deltaTime * RotationSpeed);
        }
    }

    public void FireLeft()
    {
        Fire(BarrelL.position);
    }

    public void FireRight()
    {
        Fire(BarrelR.position);
    }

    private void Fire(Vector3 start)
    {

        var eulerAngles = transform.rotation.eulerAngles;
        eulerAngles.x = 0;

        var newBullet = Instantiate(BulletPrefab, start, Quaternion.Euler(eulerAngles));
        Instantiate(MuzzlePrefab, start, transform.rotation * Quaternion.Euler(0, -180, 0), transform);

        Destroy(newBullet, 0.7f);
    }
}
