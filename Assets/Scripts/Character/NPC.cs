using System;
using UnityEngine;

namespace CharacterModel {
    public class NPC : Character, IInteractable
    {
        // [SerializeField] GameObject globalMetrics;
        [SerializeField] private float radius;

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        private void Update()
        {
            Interact();
        }

        private bool CheckInRange()
        {
            // TODO revisit this when head isn't hurting
            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
            if (radius >= distanceToPlayer)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Interact()
        {
            // if player in range, trigger dialouge
            TriggerDialouge();
        }

        private void TriggerDialouge()
        {
            if (CheckInRange() && Input.GetKeyDown(KeyCode.Space))
            {
                StartDialogue();
                // freeze the game so the player can't take damage
            }
        }

        private void StartDialogue()
        {
            foreach(string dialouge in dialogues)
            {
                print(dialouge);
            }
        }

        public bool HasInteracted()
        {
            return false;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}