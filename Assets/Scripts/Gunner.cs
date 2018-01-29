using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunner : MonoBehaviour
{

    public GameObject BulletGameObject;
    public GameObject Source;

    void Start()
    {
        Source = transform.parent.gameObject;
    }
    public void Shoot(int player)
    {
        var bullet = Instantiate(BulletGameObject, transform.position, transform.rotation) as GameObject;
        var bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.Ignore = Source;
    }
}
