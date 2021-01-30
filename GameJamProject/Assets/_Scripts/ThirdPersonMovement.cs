using UnityEngine;

[RequireComponent( typeof( CharacterController ) )]
public class ThirdPersonMovement : MonoBehaviour
{
    [SerializeField]
    private CharacterController controller;
    [SerializeField]
    private Transform cam;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private float speed = 6;
    [SerializeField]
    private float jumpForce = 3;
    [SerializeField]
    private float mass = 1.5f;

    [SerializeField]
    private float smoothDamping = 0.1f;

    private float turnSmoother;
    private bool lockRotation = false;
    private float gravityValue = -9.81f;
    private Vector3 playerVelocity;
    private float speedMod = 1;

    // Update is called once per frame
    void Update()
    {
        var groundedPlayer = controller.isGrounded;
        if ( groundedPlayer && playerVelocity.y < 0 )
        {
            playerVelocity.y = 0f;
        }

        float h = Input.GetAxisRaw( "Horizontal" );
        float v = Input.GetAxisRaw( "Vertical" );

        Vector3 dir = new Vector3( h , 0 , v ).normalized;


        animator.SetFloat( "Forward" , dir.magnitude );


        if ( dir.magnitude >= 0.1f )
        {
            float target = Mathf.Atan2( dir.x , dir.z ) * Mathf.Rad2Deg + cam.eulerAngles.y;

            if ( !lockRotation )
            {
                float angle = Mathf.SmoothDampAngle( transform.eulerAngles.y , target , ref turnSmoother , smoothDamping );
                transform.rotation = Quaternion.Euler( 0 , angle , 0 );
            }

            Vector3 moveDir = Quaternion.Euler( 0f , target , 0f ) * Vector3.forward;

            if ( lockRotation )
            {
                var velocityDir = (Quaternion.Euler( 0f , target , 0f ) * transform.forward).normalized;

                if ( Mathf.Abs( moveDir.z ) > 0 )
                    animator.SetFloat( "Forward" , velocityDir.z );
                else
                    animator.SetFloat( "Forward" , velocityDir.x );
            }

            controller.Move( moveDir.normalized * speed * speedMod * Time.deltaTime );
        }

        if ( Input.GetButtonDown( "Jump" ) && ( groundedPlayer || Mathf.Abs( playerVelocity.y ) < 0.2f ) )
        {
            playerVelocity.y += Mathf.Sqrt( jumpForce * -3.0f * gravityValue );
            animator.SetTrigger( "Jump" );
        }

        playerVelocity.y += gravityValue * mass * Time.deltaTime;

        controller.Move( playerVelocity * Time.deltaTime );

    }

    public void ToggleRotationLock()
    {
        lockRotation = !lockRotation;

        speedMod = lockRotation ? 0.35f : 1f;

        animator.SetBool( "Push" , lockRotation );
    }

}
