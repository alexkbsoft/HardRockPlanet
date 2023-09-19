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

    void Start()
    { 
        
    }

    void Update()
    {
        Aim();

        Animator.SetBool("Fire", Input.GetMouseButton(0));
    }

    private void Aim()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("Ground")))
        {
            Vector3 target = hit.point;

            Quaternion wantedRotation = Quaternion.LookRotation(target - transform.position, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, wantedRotation, Time.deltaTime);
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
        var newBullet = Instantiate(BulletPrefab, start, transform.rotation);
        Instantiate(MuzzlePrefab, start, transform.rotation * Quaternion.Euler(0, -180, 0));

        Destroy(newBullet, 1.0f);
    }
}
