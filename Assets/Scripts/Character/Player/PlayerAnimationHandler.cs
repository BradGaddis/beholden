using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Animations
{ 
       public class PlayerAnimationHandler : MonoBehaviour
    {
        PlayerController playerController;
        Animator animator;

        // Start is called before the first frame update
        void Start()
        {
            playerController = GetComponent<PlayerController>();
            animator = GetComponent<Animator>();
        }

            float x, y, walk_x, walk_y;
        // Update is called once per frames
        void Update()
        {
            HandleWalkingAnimations();
            if(playerController.GetPlayerState() == PlayerState.dashing)
            {
                animator.SetBool("isDashing", true);
                animator.SetFloat("dash_x", walk_x);
                animator.SetFloat("dash_y", walk_y);
            } else {
                animator.SetBool("isDashing", false);
            }
        }

        private void HandleWalkingAnimations()
        {
            Vector3 direction = playerController.GetDirection();

            SetWalkAnimations(direction, out x, out y, out walk_x, out walk_y);
            SetIdleAnimations(x, y, walk_x, walk_y);
        }

        private void SetIdleAnimations(float x, float y, float walk_x, float walk_y)
        {
            if (x != 0)
            {
                animator.SetFloat("idle_x", walk_x);
                if (y == 0)
                {
                    animator.SetFloat("idle_y", 0);
                }
            }

            if (y != 0)
            {
                animator.SetFloat("idle_y", walk_y);
                if (x == 0)
                {
                    animator.SetFloat("idle_x", 0);
                }
            }
        }

        private void SetWalkAnimations(Vector3 direction, out float x, out float y, out float walk_x, out float walk_y)
        {
            x = direction.x;
            y = direction.y;
            walk_x = animator.GetFloat("walk_x");
            walk_y = animator.GetFloat("walk_y");
            animator.SetFloat("walk_x", x);
            animator.SetFloat("walk_y", y);
        }

    }
}