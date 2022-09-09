using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChest : MonoBehaviour, IInteractable
{
    [SerializeField] List<Sprite> sprites = new List<Sprite>();
    bool chestIsOpen;
    bool inRange;

    GameObject contents;

    private void Update()
    {
        Interact();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        inRange = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        inRange = false;
    }

   
    public void Interact()
    {
        if(inRange && Input.GetKeyDown(KeyCode.Space) && !chestIsOpen)
        {
            GetComponent<SpriteRenderer>().sprite = sprites[1];
            chestIsOpen = true;
            // give the player some $#!^ exaclty once
        } else if(inRange && Input.GetKeyDown(KeyCode.Space) && chestIsOpen)
        {
            GetComponent<SpriteRenderer>().sprite = sprites[0];
            chestIsOpen = false;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // whenever it wants to
    }
}
