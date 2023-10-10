using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class healthManager : MonoBehaviour
{
         public float healthAmount = 100;
         public Image Health_Bar_Full; 
 
    private void Update()
    {
        if(healthAmount <= 0)
        {
            SceneManager.LoadScene("Demo");
        } 

    }

   public void TakeDamage(float Damage)
    {
        healthAmount -= Damage;
        Health_Bar_Full.fillAmount = healthAmount / 100;
    }

    public void GetHealPoints(float healPoints)
    {
        healthAmount += healPoints;
        healthAmount = Mathf.Clamp(healthAmount, 0 ,100);
        Health_Bar_Full.fillAmount = healthAmount /100;
    }
     
}
