using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState {
    idle,
    walking,
    dashing,
    running,
    jumping
}

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        // PortalTransfer portalTransfer;
        [Header("The player's movement speed")]
        [SerializeField] float speed = 5f;
        [Header("The amount of time the player will be dashing")]
        [SerializeField] float dashTime = 3f;
        [Header("The amount of time before the player can dash again")]
        [SerializeField] float dashDelay = 2f;
        [Header("The amount of force (or speed) of a dash")]
        [SerializeField] float dashForce = 5f;
        [SerializeField]AudioClip dashWoosh;
        bool dashLocked = true;
        bool canTeleport = false; 
        float dashTimer;
        Vector3 lockDashDir;
        Vector3 currentDirection;
        Rigidbody2D playerRb;
        AudioSource playerAudio;
        PlayerState playerState = PlayerState.idle;
        PlayerState prevState;


        void Start()
        {
            // portalTransfer = GetComponent<PortalTransfer>();
            playerRb = GetComponent<Rigidbody2D>();
            playerAudio = GetComponent<AudioSource>();
            dashTimer = dashTime;
            StartCoroutine(DashDelay());
        }

        void Update() {
            Debug.Log("Player State: " + playerState);
            Debug.Log("dash is locked: " + dashLocked);
        }    
        void FixedUpdate()
        {
            HandleMovement();
        }

        // TODO 
        // correct prevState issue so that it gets saved and reverted after dash 
        void HandleMovement()
        {
            Vector3 direction = GetDirection();

            if (Input.GetKey(KeyCode.LeftShift) && !dashLocked && playerState == PlayerState.walking || playerState == PlayerState.dashing )
            {
                Dash(direction);
            }
            else if (direction.magnitude > 0)
            {
                Walk(direction);
            } else {
                playerState = PlayerState.idle;
            }
        }

        public Vector3 GetDirection()
        {
            float xAxis = Input.GetAxisRaw("Horizontal");
            float yAxis = Input.GetAxisRaw("Vertical");

            Vector3 direction = new Vector2(xAxis, yAxis).normalized;
            
            return direction;
        }

        public PlayerState GetPlayerState(){
            return playerState;
        }

        private void Dash(Vector3 direction)
        {
            
            // make sure that the player is dashing now
            if (playerState != PlayerState.dashing)
            {
                // store the previous state
                prevState = playerState;
                playerState = PlayerState.dashing;
                currentDirection = direction * dashForce;
                lockDashDir = currentDirection;
                playerAudio.PlayOneShot(dashWoosh);
            }


            
            if (dashTimer >= 0)
            {
                // if player tries to double dash, prevent it
                // if(Input.GetKeyDown(KeyCode.LeftShift))
                // {
                //     ExitDash();
                // }
                
                dashTimer -= Time.deltaTime;
                Vector3 curPos = transform.position;
                Vector3 newPos = curPos + (lockDashDir * Time.deltaTime);
                lockDashDir = currentDirection;
                playerRb.MovePosition(newPos); 
            }
            else
            {
                dashTimer = dashTime;
                ExitDash();
            }
        }

        private void ExitDash()
        {
            dashLocked = true;
            playerState = prevState;
            StartCoroutine(DashDelay());
        }

        IEnumerator DashDelay()
        {
            dashLocked = true;
            yield return new WaitForSeconds(dashDelay);
            dashLocked = false;
        }

        private void Walk(Vector3 direction)
        {
            prevState = playerState;
            // intended movement direction, corrected for frame rate and speed

            Vector3 walkdirection = direction * speed * Time.deltaTime;
            // set walking player state
            if(walkdirection != Vector3.zero)
            {
                playerState = PlayerState.walking;
            }
            else
            {
                playerState = prevState;
                return;
            }
            Vector3 newPos = walkdirection + transform.position;
            playerRb.MovePosition(newPos);
        }

        // TODO
        void Jump()
        {
        }    

        void Duck() {

        }

        void Swim() {

        }

        // check if player is grounded for allowing a jump
        bool IsGrounded()
        {
            return false;
        }
    }
}