using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public float range;
    public float speed;
    public enum ElevatorType { Arriba, Derecha, Enfrente, izquierda };
    public ElevatorType movimiento;
    Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }


    void Update()
    {
        if (movimiento == ElevatorType.Arriba)
        {
            transform.position = new Vector3(initialPosition.x, initialPosition.y + Mathf.Sin(Time.fixedTime * speed) * range, initialPosition.z);
        }
        if (movimiento == ElevatorType.Derecha)
        {
            transform.position = new Vector3(initialPosition.x, initialPosition.y, initialPosition.z + Mathf.Sin(Time.fixedTime * speed) * range);
        }
        if (movimiento == ElevatorType.izquierda)
        {
            transform.position = new Vector3(initialPosition.x, initialPosition.y, initialPosition.z - Mathf.Sin(Time.fixedTime * speed) * range);  
        }
        if (movimiento == ElevatorType.Enfrente)
        {
            transform.position = new Vector3(initialPosition.x + Mathf.Sin(Time.fixedTime * speed) * range, initialPosition.y, initialPosition.z);
        }
    }
}
