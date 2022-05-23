using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
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

        // Update is called once per frames
        void Update()
        {
            HandleMovement();
        }

        private void HandleMovement()
        {
            Vector3 direction = playerController.GetDirection();
            float x, y, walk_x, walk_y;

            WalkAnimations(direction, out x, out y, out walk_x, out walk_y);
            SetIdle(x, y, walk_x, walk_y);
        }

        private void SetIdle(float x, float y, float walk_x, float walk_y)
        {
            if (x > 0 || x < 0)
            {
                animator.SetFloat("idle_x", walk_x);

                if (y == 0)
                {
                    animator.SetFloat("idle_y", 0);
                }
            }

            if (y > 0 || y < 0)
            {
                animator.SetFloat("idle_y", walk_y);

                if (x == 0)
                {
                    animator.SetFloat("idle_x", 0);
                }
            }
        }

        private void WalkAnimations(Vector3 direction, out float x, out float y, out float walk_x, out float walk_y)
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