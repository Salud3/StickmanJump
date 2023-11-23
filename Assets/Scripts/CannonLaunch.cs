using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonLaunch : MonoBehaviour
{
    public Transform launchPoint;
    public GameObject projectile;
    public float launchVelocity = 10f;
    public Animator animator;

    private void Start()
    {
        Invoke("InvokeLaunch", 0.1f);
    }
    void Update()
    {
    }
    public void Dies()
    {
        StopAllCoroutines();
        animator.SetTrigger("Die");
        Destroy(gameObject,1.0f);
    }
    private void InvokeLaunch()
    {
        StartCoroutine("Launch");
    }

    private IEnumerator Launch()
    {
        var _projectile = Instantiate(projectile, launchPoint.position, launchPoint.rotation);
        _projectile.GetComponent<Rigidbody>().velocity = launchPoint.up * launchVelocity;
        yield return new WaitForSeconds(1.0f);
        Invoke("InvokeLaunch", 1.5f);
    }
}
