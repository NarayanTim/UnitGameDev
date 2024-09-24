/*
    Name: Laxmi Timsina
    File Name: PlayerMovement.cs
*/


using UnityEngine;

public class PlayerMovement : MonoBehaviour{

    [Header("References")]
    [SerializeField] private CharacterController controller;
    [SerializeField] private InputManager inputManager;

    [Header("Movement Speed Values")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float sprintSpeed = 10f;

    [SerializeField] private float playerSpeed = 0;
    public float PlayerSpeed{get{return playerSpeed;}set{playerSpeed = value;}}
    [SerializeField] private float extraSpeedForce = 10f;

    [Header("Ground")]
    [SerializeField] private Transform ground;
    [SerializeField] private bool isGrounded;
    [SerializeField] private float groundRadius = 0.4f;
    [SerializeField] private LayerMask groundMask;
    public bool IsGrounded { get{return isGrounded;} set{isGrounded = value;} }

    [Header("Jump")]
    [SerializeField] private float jumpHeight = 8f;
    [SerializeField] private float jumpSpeed = 5.5f;

    [Header("Gravity")]
    [SerializeField] private float gravity = -9.81f;
    private Vector3 velocity;


    [Header("Keys")]
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;


    private float horizontalInput;
    private float verticalInput;
    private Vector3 movementDirection;

    private void Awake() {
        controller = GetComponent<CharacterController>();
        ground = transform.GetChild(1);
    }

    private void Start() {
        inputManager = InputManager.Instance;
    }



    private void Update(){
        GetMovementInput();
        StateHandler();
    }


    private void StateHandler(){
        if(isGrounded){
            if(Input.GetKey(sprintKey)){
                playerSpeed = sprintSpeed;
            }else{
                playerSpeed = moveSpeed;
            }
        }

    }


    private void GetMovementInput(){
        CheckGrounded();

        if(isGrounded && velocity.y < 0){
            velocity.y = -2f;
        }

        horizontalInput = inputManager.HorizontalInput;
        verticalInput = inputManager.VerticalInput;

        movementDirection = transform.right * horizontalInput + transform.forward * verticalInput;
        controller.Move(playerSpeed * Time.deltaTime * movementDirection);
        
        Jump();

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void CheckGrounded(){
        isGrounded = Physics.CheckSphere(ground.position, groundRadius, groundMask);
    }

    private void Jump(){
        if(isGrounded && Input.GetKeyDown(jumpKey)){
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity) * jumpSpeed;
        }
    }

}
