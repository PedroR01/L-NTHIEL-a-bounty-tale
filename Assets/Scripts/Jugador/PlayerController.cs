using System.Collections;
using System.Collections.Generic; //Ver si este y el de arriba hacen falta o los puedo borrar
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float maximumSpeed;

    [SerializeField] private float rotationSpeed;

    [SerializeField] private float jumpSpeed;

    [SerializeField] private float jumpButtonGracePeriod;

    [SerializeField] private float cameraSensivity;

    [SerializeField] private Transform cameraTransform;

    [SerializeField] private GameObject followTransform;

    //[SerializeField] private Animator animator;
    public CharacterController characterController;

    private Player_Stats stats;
    private Skill_Tree_Powers abilities;

    private Vector3 nextPosition;
    private Quaternion nextRotation;

    [HideInInspector] public float speed;
    private float ySpeed;
    private float originalStepOffset;
    private float? lastGroundedTime;
    private float? jumpButtonPressedTime;

    // Relacionado a la ejecucion del sonido

    private bool notWalkSound;
    private float walkDistance = 0.3f;
    private float sprintDistance = 0.1f;
    private float crouchDistance = 0.8f;
    // private float sprintVolume = 1f;
    // private float crouchVolume = 0.1f;
    // private float walkVolume; //random 0.3f 0.7f

    private Player_Audio playerFootsteps;

    private void Start()
    {
        //animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        stats = GetComponent<Player_Stats>();
        abilities = GetComponent<Skill_Tree_Powers>();
        originalStepOffset = characterController.stepOffset;

        playerFootsteps = GetComponent<Player_Audio>();
        notWalkSound = false;
    }

    private void Update()
    {
        Movement();
        SpecialMoves();
    }

    private void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        float lookX = Input.GetAxisRaw("Mouse X");
        float lookY = Input.GetAxisRaw("Mouse Y");

        #region Camera rotation based on follow object rotation

        followTransform.transform.rotation *= Quaternion.AngleAxis(lookX * cameraSensivity, Vector3.down); //Up // Hacer booleano para invertir los ejes de la camara
        followTransform.transform.rotation *= Quaternion.AngleAxis(lookY * cameraSensivity, Vector3.left); //Right

        var angles = followTransform.transform.localEulerAngles;
        angles.z = 0;

        var angle = followTransform.transform.localEulerAngles.x;

        if (angle > 180 && angle < 340)
        {
            angles.x = 340;
        }
        else if (angle < 180 && angle > 40)
        {
            angles.x = 40;
        }

        followTransform.transform.localEulerAngles = angles;

        #endregion Camera rotation based on follow object rotation

        #region player movement and rotation based on the camera axis

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);

        if (Input.GetKey(KeyCode.LeftShift) && stats.canUseStamina || Input.GetKey(KeyCode.RightShift) && stats.canUseStamina)
        {
            inputMagnitude *= 2f;
            stats.timeRecovering = false;
            stats.actualStamina -= 10f * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            inputMagnitude /= 2;
        }

        float bonusSpeed;
        stats.GetAgilityStats(out bonusSpeed, speed); // Falta hacer condicional para detectar arbol de habilidades y en base a eso modificar stats
        speed = inputMagnitude * maximumSpeed * bonusSpeed;
        movementDirection = Quaternion.AngleAxis(followTransform.transform.rotation.eulerAngles.y, Vector3.up) * movementDirection;
        movementDirection.Normalize();

        ySpeed += Physics.gravity.y * Time.deltaTime;

        if (characterController.isGrounded)
        {
            lastGroundedTime = Time.time;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpButtonPressedTime = Time.time;
        }

        if (Time.time - lastGroundedTime <= jumpButtonGracePeriod)
        {
            characterController.stepOffset = originalStepOffset;
            ySpeed = -0.5f;

            if (Time.time - jumpButtonPressedTime <= jumpButtonGracePeriod)
            {
                ySpeed = jumpSpeed;
                jumpButtonPressedTime = null;
                lastGroundedTime = null;
            }
        }
        else
        {
            characterController.stepOffset = 0;
        }

        Vector3 velocity = movementDirection * speed;
        velocity.y = ySpeed;

        characterController.Move(velocity * Time.deltaTime);

        #endregion player movement and rotation based on the camera axis

        #region Sound related

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            notWalkSound = true;
            playerFootsteps.stepDistance = sprintDistance;
        }
        else if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            notWalkSound = true;
            playerFootsteps.stepDistance = crouchDistance;
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyUp(KeyCode.LeftShift))
            notWalkSound = false;
        else if (characterController.velocity != Vector3.zero && !notWalkSound)
            playerFootsteps.stepDistance = walkDistance;

        #endregion Sound related

        nextRotation = Quaternion.Lerp(followTransform.transform.rotation, nextRotation, Time.deltaTime * cameraSensivity);

        if (horizontalInput == 0 && verticalInput == 0)
        {
            nextPosition = transform.position;

            if (Input.GetMouseButton(1))
            {
                //Set the player rotation based on the look transform
                transform.rotation = Quaternion.Euler(0, followTransform.transform.rotation.eulerAngles.y, 0);
                //reset the y rotation of the look transform
                followTransform.transform.localEulerAngles = new Vector3(angles.x, 0, 0);
            }

            return;
        }
        nextPosition = transform.position;
        //Set the player rotation based on the look transform
        transform.rotation = Quaternion.Euler(0, followTransform.transform.rotation.eulerAngles.y, 0);
        //reset the y rotation of the look transform
        followTransform.transform.localEulerAngles = new Vector3(angles.x, 0, 0);
    }

    private void SpecialMoves()
    {
        if (Input.GetKey(KeyCode.W) && Input.GetKeyDown(KeyCode.LeftAlt) && stats.actualStamina >= 20f)
        {
            abilities.DashAbility(1);
            stats.actualStamina -= 20f;
        }
        if (Input.GetKey(KeyCode.S) && Input.GetKeyDown(KeyCode.LeftAlt) && stats.actualStamina >= 20f)
        {
            abilities.DashAbility(-1);
            stats.actualStamina -= 20f;
        }
    }
}