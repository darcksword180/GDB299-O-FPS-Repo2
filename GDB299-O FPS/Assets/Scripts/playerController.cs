using UnityEngine;

public class playerController : MonoBehaviour
{
    //Core items
    [SerializeField] CharacterController controller;
    [SerializeField] LayerMask ignoreLayer;

    //character variables
    [SerializeField] int speed;
    [SerializeField] int sprintModifier;
    [SerializeField] int jumpVelocity;
    [SerializeField] int jumpMax;
    [SerializeField] int gravity;

    //shoot variables
    [SerializeField] int shootDamage;
    [SerializeField] float shootRate;
    [SerializeField] int shootDistance;

    //vector3 variables
    Vector3 moveDirection;
    Vector3 playerVelocity;

    //mechanic variables
    int jumpCounter;
    float shootTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug function showing raycast shot in red line
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * shootDistance, Color.red);

        movement();
        sprint();
    }

    void movement()
    {
        //set timer based on frame rate
        shootTimer += Time.deltaTime;

        if ((controller.isGrounded)) //check if on ground, and reset velocity and jump counter
        {
            playerVelocity = Vector3.zero;
            jumpCounter = 0;
        }

        //sets movement for wasd based on direction where camera is facing
        moveDirection = (Input.GetAxis("Horizontal") * transform.right) + (Input.GetAxis("Vertical") * transform.forward);
        controller.Move(moveDirection * speed * Time.deltaTime);

        //lets player jump based on movement and brings them back down
        jump();
        controller.Move(playerVelocity * Time.deltaTime);
        playerVelocity.y -= gravity * Time.deltaTime;


        //Set fire action and if fire rate is larger than timer allow player to shoot
        if(Input.GetButton("Fire1") && shootTimer > shootRate)
        {
            shoot();
        }
    }

    void jump()
    {
        //checks for jump action key on press down and check against max allowed jumps before allowing jump action
        if(Input.GetButtonDown("Jump") && jumpCounter < jumpMax)
        {
            playerVelocity.y = jumpVelocity;
            jumpCounter++;
        }
    }

    void sprint()
    {
        //sprint effects speed as a modifier on press or release
        if(Input.GetButtonDown("Sprint"))
        {
            speed *= sprintModifier;
        }
        else if (Input.GetButtonUp("Sprint"))
        {
            speed /= sprintModifier;
        }
    }

    void shoot()
    {
        //set default timer
        shootTimer = 0;

        //track if shot hits anything using raycast and deals damage based on shootDamage
        RaycastHit hit;
        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, shootDistance, ~ignoreLayer))
        {
            //Debug.Log(hit.collider.name); //debug function to verify raycast does work
            IDamage dmg = hit.collider.GetComponent<IDamage>();

            //check if item can take damage, then damage if possible
            if (dmg != null)
            {
                dmg.takeDamage(shootDamage);
            }
        }
    }
}
