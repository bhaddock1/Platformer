using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/**************************************************
* attached to player: moves player in direction of player input W/A/S/D 
* allows player to jump on jump input (spacebar)
* launches player upon collision with launchpad tagged game object
*
*   Bryce Haddock 1.0 September 25, 2023
**************************************************/
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;

    public float jumpForce;
    public float launchForce;
    public float boostForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [Header("Stamina Costs")]
    public float jumpStaminaCost;
    public float moveStaminaCost;
    public float maxStamina;
    public float currentStamina;

    public Image staminaBar;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode forwardKey = KeyCode.W; // key to move player forward (W)
    public KeyCode backwardKey = KeyCode.S; // key to move player backward (S)
    public KeyCode leftKey = KeyCode.A; // key to move player left (A)
    public KeyCode rightKey = KeyCode.D; // key to move player right (D)

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    public Transform startPosition;

    float horizontalInput;
    float verticalInput;

    public LaunchControl launchControl;
    public BoostControl boostControl;

    Vector3 moveDirection;

    Rigidbody rb;

    // Initializes ready to jump and player rigidbody on start
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        currentStamina = maxStamina; // sets current stamina to max stamina on start
        readyToJump = true;
    }

    //Updates method every frame 
    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround); //Searches for ground on frame update if ground is detected player = grounded

        MyInput();
        SpeedControl();
        Boost();

        if (grounded)  //eliminates drag when not touching ground
            rb.drag = groundDrag;
        else
            rb.drag = 0;

    }

    //countinuesly checks the move player field 
    private void FixedUpdate()
    {
        MovePlayer();
    }

    // executes method on specified player input
    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal"); // moves player horizontal on horizantal input A/D
        verticalInput = Input.GetAxisRaw("Vertical");   // moves player vertical on vertical input W/S
        if (Input.GetKey(forwardKey)) // if the player presses the forward key (W) the following is executed
        {
             currentStamina -= moveStaminaCost * Time.deltaTime; // subtracts stamina from player when moving forward
             staminaBar.fillAmount = currentStamina / maxStamina; // updates stamina bar to reflect current stamina
        }
        if (Input.GetKey(backwardKey)) // if the player presses the backward key (S) the following is executed
        {
            currentStamina -= moveStaminaCost * Time.deltaTime; // subtracts stamina from player when moving backward
            staminaBar.fillAmount = currentStamina / maxStamina; // updates stamina bar to reflect current stamina
        }
        if (Input.GetKey(leftKey)) // if the player presses the left key (A) the following is executed
        {
            currentStamina -= moveStaminaCost * Time.deltaTime; // subtracts stamina from player when moving left
            staminaBar.fillAmount = currentStamina / maxStamina; // updates stamina bar to reflect current stamina
        }
        if (Input.GetKey(rightKey))
        { 
            currentStamina -= moveStaminaCost * Time.deltaTime; // subtracts stamina from player when moving right
            staminaBar.fillAmount = currentStamina / maxStamina; // updates stamina bar to reflect current stamina
        }
        if (Input.GetKey(jumpKey) && readyToJump && grounded) //if the player is ready to jump and grounded while jumpkey(spacebar) is pressed the following is executed
        {
            readyToJump = false;    //player is no longer ready to jump

            Jump(); // jump action is performed

            Invoke(nameof(ResetJump), jumpCooldown);    //jump cooldown begins 
        }
    }
    
    

    // when the player collides with the launch pad tagged object they will perform the launch action
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("LaunchPad"))
        {
                     
           launchForce = launchControl.launchMultiplier;
     
           Launch();
        }

        if (collision.gameObject.CompareTag("Death"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            
        }
        
       
    }
    // moves the player in the relative direction of player input
    // if the player is grounded they move at normal move speed
    //if the player is not grounded they move at normal speed times air multiplier
    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
            
        }
        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    //limits player movement speed to player movement speed 
    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    // when the jump action is performed the player will launch into the air at jump speed
    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        currentStamina -= jumpStaminaCost; // subtracts stamina from player when jump is performed  
        staminaBar.fillAmount = currentStamina / maxStamina; // updates stamina bar to reflect current stamina

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    // when the launch action is invoked the player will launch into the air at launch speed
    private void Launch()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * launchForce, ForceMode.Impulse);
    }

    private void Boost()
    {
        

        if(boostControl.boosted)
        {
            
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(transform.up * boostForce, ForceMode.Impulse);
        }
        boostControl.boosted = false;
    }
    
    // resets ready to jump 
    private void ResetJump()
    {
        readyToJump = true;
    }

    private void checkStamina()
    {
        if(currentStamina <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void checkClock()
    {
        if (GameObject.Find("Clock").GetComponent<Clock>().isClockRunning == false)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}