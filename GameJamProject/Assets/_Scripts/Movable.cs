using UnityEngine;

public class Movable : Interactible
{
    [SerializeField]
    private float dragSpeed = 3;
    [SerializeField]
    private float turnSpeed = 30;
    [SerializeField]
    private KeyCodeVariable interactButton;
    [SerializeField]
    private GameEvent toolTipEvent;
    [SerializeField]
    private KeyCodeVariable rotateLeft;

    [SerializeField]
    private KeyCodeVariable rotateRight;

    private bool beingDragged;
    private Transform anchor;
    private Vector3 startPos;
    private Transform originalParent;
    public override void Interact( GameObject other ) 
    {
        beingDragged = !beingDragged;
        anchor = other.transform;

        if ( beingDragged )
        {
            transform.parent = anchor;
        }
        else
        {
            transform.parent = originalParent;
        }

        toolTipEvent.Raise( "" );
    }

    public override void Reset()
    {
        transform.position = startPos;
    }

    private void Start()
    {
        startPos = transform.position;
        originalParent = transform.parent;
    }

    private void Update()
    {
        if ( beingDragged )
        {
            if ( Input.GetKey( rotateLeft.KeyCode ) )
                transform.Rotate( Vector3.up * turnSpeed * Time.deltaTime );

            if ( Input.GetKey( rotateRight.KeyCode ) )
                transform.Rotate( -Vector3.up * turnSpeed * Time.deltaTime );
        }
    }

    private void OnTriggerEnter( Collider other )
    {
        if ( other.CompareTag( "Player" ) && !beingDragged)
        {
            toolTipEvent.Raise( $"Press {interactButton.KeyCode} to grab Press {interactButton.KeyCode} again to let go.\nPress {rotateLeft.KeyCode} / {rotateRight.KeyCode} to rotate." );
        }
    }

    private void OnTriggerExit( Collider other )
    {
        if ( other.CompareTag( "Player" ) )
        {
            toolTipEvent.Raise( "" );
        }
    }

}
