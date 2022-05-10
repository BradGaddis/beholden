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

public class PlayerController : MonoBehaviour
{
    PortalTransfer portalTransfer;
    [Header("The player's movement speed")]
    [SerializeField] float speed = 5f;
    [SerializeField] float dashTime = 3f;
    [SerializeField] float dashDelay = 2f;
    [SerializeField] float dashForce = 5f;
    [SerializeField]AudioClip dashWoosh;
    bool dashLocked = true;
    bool canTeleport = false; 
    float dashTimer;
    float dashDeminsh; // this is presently not implemented and is a waste of code so far.
    Vector3 lockDashDir;
    Rigidbody2D playerRb;
    AudioSource playerAudio;
    PlayerState playerState = PlayerState.idle;
    PlayerState prevState;


    void Start()
    {
        portalTransfer = GetComponent<PortalTransfer>();
        playerRb = GetComponent<Rigidbody2D>();
        playerAudio = GetComponent<AudioSource>();
        dashTimer = dashTime;
        dashDeminsh = dashForce;
        StartCoroutine(DashDelay());
    }

    void Update() {
        // canTeleport = portalTransfer.readyToTeleport;
        Debug.Log("Player State: " + playerState);
    }    
    void FixedUpdate()
    {
        HandleMovement();
    }

    // TODO 
    // correct prevState issue so that it gets saved and reverted after dash 
    void HandleMovement()
    {
        Vector3 direction = GetDirection() * Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftShift) && !canTeleport && !dashLocked || (playerState == PlayerState.dashing && !dashLocked))
        {
            Dash(direction);
        }
        else
        {
            Walk(direction);
        }
    }

    public Vector3 GetDirection()
    {
        float xAxis = Input.GetAxisRaw("Horizontal");
        float yAxis = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector2(xAxis, yAxis).normalized;
        
        return direction;
    }

    private void Dash(Vector3 direction)
    {

        // prevState; TODO
        prevState = playerState;

        if (playerState != PlayerState.dashing)
        {
            lockDashDir = direction * dashDeminsh;
            playerState = PlayerState.dashing;
            playerAudio.PlayOneShot(dashWoosh);
        }

        if(lockDashDir == Vector3.zero) 
        {
            // return to previous state;
            playerState = prevState;
            return; 
        }
        
        if (dashTimer >= 0)
        {
            // if player tries to double dash, prevent it
            if(Input.GetKeyDown(KeyCode.LeftShift))
            {
                ExitDash();
            }
            dashTimer -= Time.deltaTime;
            Vector3 curPos = transform.position;
            Vector3 newPos = curPos + lockDashDir;
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

        Vector3 walkdirection = direction * speed;
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
        // this was an attempt a grid-based movement lol
        // float x = Mathf.Round(newPos.x);
        // float y =  Mathf.Round(newPos.y);
        // newPos = new Vector3(x, y);
        playerRb.MovePosition(newPos);
    }

    // TODO
    void Jump()
    {
    }    

    void Duck() {

    }

    // check if player is grounded for allowing a jump
    bool IsGrounded()
    {

        return false;
    }
}
