using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootComponent : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform turretTransform;
    [SerializeField] private int dmg;

    public ShootComponent(GameObject _bullet, Transform _turretTransform)
    {
        bullet = _bullet;
        turretTransform = _turretTransform;
    }

    public void Shoot()
    {
        Debug.Log("Fire");
        GameObject _bullet = Instantiate(bullet, turretTransform.position, turretTransform.rotation);
        _bullet.GetComponent<Bullet>().Init(dmg, this.gameObject); 
        _bullet.GetComponent<Rigidbody>().AddForce(turretTransform.transform.forward * 500);
    }

    public void StartFiring()
    {
        InvokeRepeating("Shoot", 0.5f, 0.5f);
    }

    public void StopFiring()
    {
        CancelInvoke("Shoot");
    }
}
