using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class ThirdPersonMovement : MonoBehaviour
{
    [SerializeField]
    private CharacterController controller;
    [SerializeField]
    private Transform cam;

    [SerializeField]
    private float speed = 6;

    [SerializeField]
    private float smoothDamping = 0.1f;

    private float turnSmoother;
    private bool lockRotation = false;
    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw( "Horizontal" );
        float v = Input.GetAxisRaw( "Vertical" );
        

        Vector3 dir = new Vector3( h , 0 , v ).normalized;

        if(dir.magnitude >= 0.1f )
        {
            float target = Mathf.Atan2( dir.x , dir.z ) * Mathf.Rad2Deg + cam.eulerAngles.y;

            if ( !lockRotation )
            {
                float angle = Mathf.SmoothDampAngle( transform.eulerAngles.y , target , ref turnSmoother , smoothDamping );
                transform.rotation = Quaternion.Euler( 0 , angle , 0 );
            }

            Vector3 moveDir = Quaternion.Euler( 0f , target , 0f ) * Vector3.forward;
            controller.Move( moveDir.normalized * speed * Time.deltaTime );
        }

    }

    public void ToggleRotationLock()
    {
        lockRotation = !lockRotation;
    }


}
