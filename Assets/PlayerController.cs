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
    // PortalTransfer portalTransfer;
    [Header("The player's movement speed")]
    [SerializeField] float speed = 5f;
    [SerializeField] float dashTime = 3f;
    [SerializeField] float dashDelay = 2f;
    [SerializeField] float dashForce = 5f;
    bool dashLocked = true;
    bool canTeleport;
    float dashTimer;
    float dashDeminsh;
    Vector3 lockDashDir;
    Rigidbody2D playerRb;
    PlayerState playerState = PlayerState.idle;
    PlayerState preState;


    void Start()
    {
        // portalTransfer = GetComponent<PortalTransfer>();
        playerRb = GetComponent<Rigidbody2D>();
        dashTimer = dashTime;
        dashDeminsh = dashForce;
        StartCoroutine(DashDelay());
    }

    void Update() {
        // canTeleport = portalTransfer.readyToTeleport;
        Debug.Log(dashLocked);
    }    
    void FixedUpdate()
    {
        HandleMovement();
    }

    // TODO 
    // correct prevState issue so that it gets saved and reverted after dash 
    void HandleMovement()
    {
        float xAxis = Input.GetAxisRaw("Horizontal");
        float yAxis = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector2(xAxis , yAxis).normalized * Time.deltaTime; 
    
        if(Input.GetKey(KeyCode.Space) && !canTeleport && !dashLocked || (playerState == PlayerState.dashing && !dashLocked))
        {
            Dash(direction);
        }
        else {
            Walk(direction);
        }
    }

    private void Dash(Vector3 direction)
    {
        if(direction == Vector3.zero) { return; }

        // prevState; TODO
        if (playerState != PlayerState.dashing)
        {
            lockDashDir = direction * dashDeminsh;
            playerState = PlayerState.dashing;
        }
        
        if (dashTimer >= 0)
        {
            // if player tries to double dash, prevent it
            if(Input.GetKeyDown(KeyCode.Space))
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
        Debug.Log("Exit Dash");
        dashLocked = true;
        playerState = PlayerState.idle;
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
        // set walking player state
        playerState = PlayerState.walking;
        // intended movement direction, corrected for frame rate and speed
        Vector3 walkdirection = direction * speed;
        Vector3 newPos = walkdirection + transform.position;
        // float x = Mathf.Round(newPos.x);
        // float y =  Mathf.Round(newPos.y);
        // newPos = new Vector3(x, y);
        playerRb.MovePosition(newPos);
    }

    void Jump()
    {
    }    

    // check if player is grounded for allowing a jump
    bool IsGrounded()
    {
        return false;
    }
}
