using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalHandler : MonoBehaviour
{
    // the sibling portal location
    Vector2 otherPortalLoc = Vector2.zero;

    private void OnTriggerEnter2D(Collider2D other) {
        // the tag of the other portal
        string siblingName = other.gameObject.tag == "Portal 1" ? "Portal 2" : "Portal 1";

        // find the portal system that we are standing in now 
        Transform parentSystem = other.gameObject.transform.parent;

        // figure out which pair is the sibling
        foreach (Transform child in parentSystem)
        {
            if(child.gameObject.tag == siblingName)
            {
                otherPortalLoc = child.transform.position;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        // move to the other locaton when you press the correct key
        if(Input.GetKeyDown(KeyCode.Space))
        {
            transform.position = otherPortalLoc;
        }
    }
}
