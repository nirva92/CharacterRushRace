using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickHandler : MonoBehaviour
{
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = -Camera.main.transform.position.z;
            Vector2 rayPos = Camera.main.ScreenToWorldPoint(mousePos);
          
            RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject.CompareTag("Character"))
            {
                hit.collider.GetComponent<Charaster>().GetClick();
            }
        }
    }
   
}
