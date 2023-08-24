using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyInteraction : MonoBehaviour
{
    public Button currentButton;
    public bool isTrigger = false;
    public bool isKeyClick = false;

    private void Update()
    {
        if (isTrigger && Input.GetKeyDown(KeyCode.Space))
        {
            isKeyClick = true;
            if (currentButton != null)
            {
                currentButton.onClick.Invoke();
            }
        }
        
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isKeyClick = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        currentButton = collision.GetComponentInChildren<Button>();
        isTrigger = true;
        Debug.Log(currentButton + " triggered");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isTrigger = false;
        currentButton = null;
        Debug.Log(currentButton + "Untriggered");
    }
}
