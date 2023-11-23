using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private Vector3 velmov;
    private Vector2 offset;
    public Material material;
    public Rigidbody rb;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        offset = (rb.velocity.x*0.1f)*velmov* Time.deltaTime;
        material.mainTextureOffset = offset;
    }
}
