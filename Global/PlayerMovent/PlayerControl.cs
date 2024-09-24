/*
    Name: Laxmi Timsina
    File Name: PlayerControl.cs
*/


using UnityEngine;

public class PlayerControl : MonoBehaviour{
    public enum MovementState{
        IDLE,
        WALKING,
        SPRINT,
        AIR,
        CROUCH
    }


    [Header("Components / Scripts")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private CameraManager cameraManager;

    [Header("Movement Speed Values")]
    
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float sprintSpeed = 10f;
    [SerializeField] private float crouchSpeed = 2f;
    [SerializeField] private float playerSpeed = 0;

    [Header("Player Crouch")]
    [SerializeField] private float crouchScaleY;
    [SerializeField] private float normalScaleY;
    [SerializeField] private float extraCrouchingForce = 5f;
    [Header("Player Crouch state")]
    [SerializeField] private bool isCrouching = false;

    
    [Space(15)]
    [Header("Extra Force")]
    [SerializeField] private float extraSpeedForce = 10f;
    [SerializeField]  private float extraSlopeSpeedForce = 20f;
    [SerializeField] private float extraSlopeDownwardForce = 80f;

    [Header("Player State")]
    [SerializeField] private MovementState currentPlayerState;

    [Space(15)]
    [Header("Jumping")]
    [SerializeField] private float jumpForce = 15f;
    [SerializeField] private float jumpCoolDown = 2f;
    [SerializeField] private float airMultiplier = 0.4f;
    
    [Header("Jumping State")]
    [SerializeField] private bool isJumping = false;
    [SerializeField] private bool isReadyToJump = true;
    [SerializeField] private bool resetJump = true;

    [Space(15)]
    [Header("Slope Movement")]
    [SerializeField] private bool isOnSlope;
    [SerializeField] private bool exitSlop;
    [SerializeField] private float maxSlopAngle = 45f;
    private RaycastHit slopHit;

    [Space(15)]
    [Header("Ground")]
    [SerializeField] private bool isGrounded = true;
    [SerializeField] private float playerHeight;
    [SerializeField] private LayerMask groundMask;



    [Space(15)]
    [Header("Player")]
    [SerializeField] private Transform player;
    

    [Header("Keys")]
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode crouchKey = KeyCode.LeftControl;

    [Space(15)]
    [Header("Other Values")]
    [SerializeField] private float drag = 10f;


    private float horizontalInput;
    private float verticalInput;
    [SerializeField] private Vector3 movementDirection;
    

    private void Awake() {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        normalScaleY = transform.localScale.y;
    }

    private void Start() {
        inputManager = InputManager.Instance;
        cameraManager = CameraManager.Instance;
    }

    private void Update() {
        isGrounded = GroundCheck();
        
        ChangePlayerCurrentState();
        HandleCrouch();
        
        PlayerSpeedControl();
        
        GetMovementInput();


        if(!isCrouching){
            PlayerJump();
        }


        rb.drag = isGrounded ? drag : 0;
    }

    private void FixedUpdate() {
        MovePlayer();
    }


    private void ChangePlayerCurrentState(){
        
        if(isGrounded){
            if(movementDirection == Vector3.zero && !isCrouching){
                currentPlayerState = MovementState.IDLE;
                playerSpeed = 0;
            }else if(Input.GetKey(sprintKey) && !isCrouching){
                currentPlayerState = MovementState.SPRINT;
                playerSpeed = sprintSpeed;

            }else if(Input.GetKey(crouchKey) && isCrouching){
                currentPlayerState = MovementState.CROUCH;
                playerSpeed = crouchSpeed;                
            }else{
                currentPlayerState = MovementState.WALKING;
                playerSpeed = moveSpeed;
            }
        }else{
            currentPlayerState = MovementState.AIR;
            playerSpeed = 0;
        }

    }



    private void MovePlayer(){
        movementDirection = cameraManager.MainCameraTransform.forward * verticalInput + cameraManager.MainCameraTransform.right * horizontalInput;


        if(OnSlope() && !exitSlop){
            rb.AddForce(extraSlopeSpeedForce * playerSpeed * GetSlopMoveDirection(), ForceMode.Force);

            if(rb.velocity.y > 0){
                rb.AddForce(Vector3.down * extraSlopeDownwardForce, ForceMode.Force);
            }

        }


        if(isGrounded){
            rb.AddForce(extraSpeedForce * playerSpeed * movementDirection.normalized, ForceMode.Force);
        }else if(!isGrounded){
            rb.AddForce(airMultiplier * extraSpeedForce * playerSpeed * movementDirection.normalized, ForceMode.Force);

        }


        
        rb.useGravity = !OnSlope();

    }

    private void GetMovementInput(){
        horizontalInput = inputManager.HorizontalInput;
        verticalInput = inputManager.VerticalInput;
        
    }


    // Ground Input

    private bool GroundCheck(){
        return Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, groundMask);
    }


    // Player Control
    private void PlayerSpeedControl(){
        if(OnSlope() && !exitSlop){
            if(rb.velocity.magnitude > playerSpeed){
                rb.velocity = rb.velocity.normalized * playerSpeed;
            }
        }else{
            Vector3 flatVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            // Limit velocity if needed
            if(flatVelocity.magnitude > playerSpeed){
                Vector3 limited = flatVelocity.normalized * playerSpeed;
                rb.velocity = new Vector3(limited.x, rb.velocity.y, limited.z);
            }

        }

    }


    // Jump
    private void JumpControl(){
        exitSlop = true;
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void PlayerJump(){
        if(Input.GetKeyDown(jumpKey) && isReadyToJump && !isJumping){
            isJumping = true;
            isReadyToJump = false;
            JumpControl();
            Invoke(nameof(ResetJump), jumpCoolDown);
        }
    }

    private void ResetJump(){
        isReadyToJump = true;
        isJumping = false;
        exitSlop = false;
    }

    // Handle Crouch
    private void HandleCrouch(){
        if(Input.GetKeyDown(crouchKey) && !isCrouching){
            transform.localScale = new Vector3(transform.localScale.x, crouchScaleY, transform.localScale.z);
            rb.AddForce(Vector3.down * extraCrouchingForce, ForceMode.Impulse);
            isCrouching = true;
        }


        if(Input.GetKeyUp(crouchKey) && isCrouching){
            transform.localScale = new Vector3(transform.localScale.x, normalScaleY, transform.localScale.z);
            isCrouching = false;
        }


    }


    // Slope
    private bool OnSlope(){
        if(Physics.Raycast(transform.position, Vector3.down, out slopHit,  playerHeight * 0.5f + 0.3f)){
            float angle = Vector3.Angle(Vector3.up, slopHit.normal);
            return angle < maxSlopAngle && angle != 0;
        }
        return false;   
    }

    private Vector3 GetSlopMoveDirection(){
        return Vector3.ProjectOnPlane(movementDirection, slopHit.normal).normalized;
    }

    public MovementState GetCurrentState(){
        return currentPlayerState;
    }

}
