using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalHandler : MonoBehaviour
{
    // the sibling portal location
    Vector2 otherPortalLoc = Vector2.zero;

    // are we in another portal?
    bool isInPortal = false;
    int randPortalNum;
    string siblingName;

    private void OnTriggerEnter2D(Collider2D other) {
        PortalSystem PortalSystem = other.gameObject.transform.parent.GetComponent<PortalSystem>();
        if(PortalSystem)
        {
            Transform parentSystem = other.gameObject.transform.parent;
            int childCount = parentSystem.childCount;

            do {
                randPortalNum = Random.Range(1, childCount + 1);
                siblingName = $"Portal {randPortalNum}";
            } while(other.gameObject.tag == siblingName);

            // find where the sibling portal is located
            foreach (Transform child in parentSystem)
            {
                if(child.gameObject.tag == siblingName)
                {
                    isInPortal = true;
                    otherPortalLoc = child.transform.position;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(isInPortal && other.gameObject.tag == siblingName)
        {
            isInPortal = false;
        }
    }

    private void Update() {
         if(Input.GetKeyDown(KeyCode.Space) && isInPortal)
        {
            transform.position = otherPortalLoc;
        }
    }
}
