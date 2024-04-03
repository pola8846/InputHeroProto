using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestShoot : MonoBehaviour
{
    public BulletShooter bulletShooter1;
    public BulletShooter bulletShooter2;

    public float time = 1.5f;

    private void Start()
    {
        StartCoroutine(testShoot());
    }

    private IEnumerator testShoot()
    {
        while (true)
        {
            bulletShooter1.triger = true;
            yield return new WaitForSeconds(time);
            bulletShooter2.triger = true;
            yield return new WaitForSeconds(time);
        }
    }
}
