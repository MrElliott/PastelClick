using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clickable : MonoBehaviour
{
    void OnClick()
    {
        Debug.Log("Object clicked!"); // Placeholder action (print message)
        
        FindObjectOfType<ParticleManager>().PlayParticle(transform.position);
        
        Destroy(transform.gameObject);
    }

    // Called when a collider attached to this object is clicked
    void OnMouseDown()
    {
        OnClick();
    }
}
