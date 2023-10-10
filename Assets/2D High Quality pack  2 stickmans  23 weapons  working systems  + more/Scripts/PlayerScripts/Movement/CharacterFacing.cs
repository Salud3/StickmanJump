using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterFacing : MonoBehaviour
{

    public Transform Arm;
    Vector2 direction;
    

    void Update()
    {
       characterFaceing();
    } 

    void characterFaceing()
    {
        Vector2 charPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Character position.
        //direction = charPos - (Vector2)transform.position;
		//Arm.transform.right = direction;

        if (charPos.x < transform.position.x) 
        {

            transform.eulerAngles = new Vector2(transform.rotation.x, 180f);

        }
        else
        {

            transform.eulerAngles = new Vector2(transform.rotation.x, 0f);

        }
    }

}