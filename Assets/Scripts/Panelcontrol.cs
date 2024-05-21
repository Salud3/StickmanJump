using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Panelcontrol : MonoBehaviour
{
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField, TextArea(4, 6)] private string Text;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            dialoguePanel.SetActive(true);
            dialogueText.text = Text;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            dialoguePanel.SetActive(false);
        }
    }
}
