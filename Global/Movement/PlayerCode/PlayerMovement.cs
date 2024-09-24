// /*
//     Name: Laxmi Timsina
//     File Name: PlayerMovement.cs
// */


// using UnityEngine;


// public class PlayerMovement : MonoBehaviour{
//     public enum MovementState{
//         IDLE,
//         WALKING,
//         SPRINT,
//         AIR,
//     }


//     [Header("Components / Scripts")]
//     [SerializeField] private Rigidbody rb;

//     [Header("Movement Speed Values")]
//     [SerializeField] private float moveSpeed = 5f;
//     [SerializeField] private float sprintSpeed = 10f;

//     [SerializeField] private float playerSpeed = 0;
//     [SerializeField] private float extraSpeedForce = 10f;
//     [SerializeField]  private float extraSlopeSpeedForce = 20f;
//     [SerializeField] private float extraSlopeDownwardForce = 80f;

//     [Header("Player State")]
//     [SerializeField] private MovementState playerState;

//     [Space(15)]
//     [Header("Jumping")]
//     [SerializeField] private float jumpForce = 15f;
//     [SerializeField] private float jumpCoolDown = 2f;
//     [SerializeField] private float airMultiplier;
    
//     [Header("Jumping State")]
//     [SerializeField] private bool isJumping = false;
//     [SerializeField] private bool readyToJump = true;
//     [SerializeField] private bool resetJump = true;

//     [Space(15)]
//     [Header("Slope Movement")]
//     [SerializeField] private bool isOnSlope;
//     [SerializeField] private bool exitSlop;
//     [SerializeField] private float maxSlopAngle = 45f;
//     private RaycastHit slopHit;

//     [Space(15)]
//     [Header("Ground")]
//     [SerializeField] private bool isGrounded = true;
//     [SerializeField] private float playerHeight;
//     [SerializeField] private LayerMask groundMask;



//     [Space(15)]
//     [Header("Player")]
//     [SerializeField] private Transform player;
    
//     [Space(15)]
//     [Header("Others")]
//     [SerializeField] float groundDrag;

//     [Header("Keys")]
//     [SerializeField] private KeyCode jumpKey = KeyCode.Space;
//     [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;


//     private float horizontalInput;
//     private float verticalInput;
//     private Vector3 movementDirection;
    

//     private void Awake() {
//         rb = GetComponent<Rigidbody>();
//         rb.freezeRotation = true;
//         player = transform;
//     }



//     private void Update(){
//         isGrounded = GroundCheck();
//         isJumping = isGrounded ? false : true;
        
//         GetMovementInput();
//         SpeedControl();
//         PlayerJump();
//         StateHandler();

//         rb.drag = isGrounded ? groundDrag : 0;

//     }

//     private void FixedUpdate() {
//         MovePlayer();
//     }


//     private void StateHandler(){
//         if(isGrounded){
//             if(!GotInputMovement()){
//                 SetState(MovementState.IDLE, 0);
//             }else if(Input.GetKey(sprintKey)){
//                 SetState(MovementState.SPRINT, sprintSpeed);
//             }else{
//                 SetState(MovementState.WALKING, moveSpeed);
//             }
//         }else{
//             SetState(MovementState.AIR, playerSpeed);
//         }
//     }


//     private void SetState(MovementState state, float speed){
//         playerState = state;
//         playerSpeed = speed;
//     }


//     private bool GotInputMovement(){
//         // Debug.LogWarning($"{verticalInput != 0 || horizontalInput != 0} - GotInputMovement()");
//         return verticalInput != 0 || horizontalInput != 0;

//     }


//     private bool GroundCheck(){
//         return Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, groundMask);
//     }

//     private void GetMovementInput(){
//         horizontalInput = Input.GetAxisRaw("Horizontal");
//         verticalInput = Input.GetAxisRaw("Vertical");
//     }


//     private void MovePlayer(){
//         movementDirection = (player.forward * verticalInput) + (player.right * horizontalInput).normalized;
//         movementDirection = Vector3.ProjectOnPlane(movementDirection, Vector3.up).normalized;

//         if(IsPlayerOnSlope()){
//             rb.AddForce(extraSlopeSpeedForce * playerSpeed * GetSlopeMovement(), ForceMode.Force);
//             if(rb.velocity.y > 0){
//                 rb.AddForce(Vector3.down * extraSlopeDownwardForce, ForceMode.Force);
//             }
//         }


//         if(isGrounded){
//             rb.AddForce(extraSpeedForce * playerSpeed * movementDirection, ForceMode.Force);
//         }else if(!isGrounded){
//             // in air
//             rb.AddForce(airMultiplier * extraSpeedForce * playerSpeed * movementDirection, ForceMode.Force);
//         }

//         rb.useGravity = !IsPlayerOnSlope();

//     }


//     private bool IsPlayerOnSlope(){
//         if(Physics.Raycast(transform.position, Vector3.down, out slopHit, playerHeight * 0.5f + .3f)){
//             float angle = Vector3.Angle(Vector3.up, slopHit.normal);
//             return angle < maxSlopAngle && angle != 0;
//         }
//         return false;
//     }

//     private Vector3 GetSlopeMovement(){
//         return Vector3.ProjectOnPlane(movementDirection, slopHit.normal).normalized;
//     }


//     private void SpeedControl(){

//         // On Slope
//         if(IsPlayerOnSlope() && !exitSlop){
//             if(rb.velocity.magnitude > playerSpeed){
//                 rb.velocity = rb.velocity.normalized * playerSpeed;
//             }
//         }else{
//             Vector3 flatVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
//             if(flatVelocity.magnitude > playerSpeed){
//                 Vector3 limitedVelocity = flatVelocity.normalized * playerSpeed;
//                 rb.velocity = new Vector3(limitedVelocity.x, 0, limitedVelocity.z);
//             }
//         }
//     }

//     private void PlayerJump(){
//         if(Input.GetKey(jumpKey) && readyToJump && isGrounded){
//             isJumping = true;
//             readyToJump = false;
//             Jump();
//             resetJump = false;
//             if(!resetJump){
//                 // Debug.LogWarning("Rest Jump");
//                 Invoke(nameof(ResetJump), jumpCoolDown);
//             }
//         }
//     }

//     private void Jump(){
//         exitSlop = true;
//         rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
//         rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
//     }

//     private void ResetJump(){
//         readyToJump = true;
//         resetJump = true;
//         exitSlop = false;
//     }



//     public MovementState GetPlayerState(){
//         return playerState;
//     }

// }
