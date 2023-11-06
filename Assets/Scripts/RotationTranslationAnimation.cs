using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationTranslationAnimation : MonoBehaviour
{

    //Solución por GameDevTraum

    private enum Axis {X,Y,Z};
    [SerializeField]
    private Axis movementAxis;

    [SerializeField]
    private Axis rotationAxis;

    [SerializeField]
    [Range(0f, 5f)]
    private float rotationSpeed;
    [SerializeField]
    [Range(0f, 5f)]
    private float movementAmplitude;
    
    [SerializeField]
    [Range(0f, 5f)]
    private float movementFrequency;
    float radIncrement;
    float rad=0;
    private float initialX,initialY,initialZ;

    void Start()
    {
        radIncrement = 2 * Mathf.PI*movementFrequency;
        initialX = transform.position.x;
        initialY = transform.position.y;
        initialZ = transform.position.z;
    }

    void FixedUpdate()
    {
        Movement();
        Rotation();       
         
    }

    private void Rotation()
    {
        switch (rotationAxis)
        {
            case Axis.X:
                transform.Rotate(rotationSpeed * Vector3.right);
                break;
            case Axis.Y:
                transform.Rotate(rotationSpeed * Vector3.up);
                break;
            case Axis.Z:
                transform.Rotate(rotationSpeed * Vector3.forward);
                break;
        }
    }

    private void Movement()
    {
#if UNITY_EDITOR
        radIncrement = 2 * Mathf.PI * movementFrequency;
#endif
        switch (movementAxis)
        {
            case Axis.X:
                transform.position = new Vector3(initialX + movementAmplitude * Mathf.Sin(rad),transform.position.y, transform.position.z);
                break;
            case Axis.Y:
                transform.position = new Vector3(transform.position.x, initialY + movementAmplitude * Mathf.Sin(rad), transform.position.z);
                break;
            case Axis.Z:
                transform.position = new Vector3(transform.position.x, transform.position.y, initialZ + movementAmplitude * Mathf.Sin(rad));
                break;
        }

        rad += radIncrement * Time.fixedDeltaTime;
        if (rad > 2 * Mathf.PI)
        {
            rad = 0;
        }
    }

}
