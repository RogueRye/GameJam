using UnityEngine;

public class Movable : Interactible
{
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

    [SerializeField]
    private bool sticky;

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
        if ( toolTipEvent != null )
            toolTipEvent.Raise( "" );
    }

    public override void ResetItem()
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

    private void OnCollisionEnter( Collision collision )
    {
        if(collision.transform.TryGetComponent<Movable>(out var other ))
        {
            //Debug.Log( "collision" );
            if ( sticky )
            {
                transform.parent = other.transform;
                gameObject.tag = "Untagged";
                
            }
        }
    }

    private void OnCollisionExit( Collision collision )
    {
        if ( collision.transform.TryGetComponent<Movable>( out var other ) )
        {
            if ( sticky )
            {
                transform.parent = originalParent;
                gameObject.tag = "Interaction";
            }
        }
    }

    private void OnTriggerEnter( Collider other )
    {
        if ( other.CompareTag( "Player" ) && !beingDragged)
        {
            if(toolTipEvent != null)
                toolTipEvent.Raise( $"Press {interactButton.KeyCode} to grab Press {interactButton.KeyCode} again to let go.\nPress {rotateLeft.KeyCode} / {rotateRight.KeyCode} to rotate." );
        }
        if ( other.CompareTag("Interaction") || other.transform.TryGetComponent<Movable>( out var collided ) )
        {
            //Debug.Log( "collision" );
            if ( sticky )
            {
                transform.parent = other.transform;
                gameObject.tag = "Untagged";
            }
        }
    }

    private void OnTriggerExit( Collider other )
    {
        if ( other.CompareTag( "Player" ) )
        {
            if ( toolTipEvent != null )
                toolTipEvent.Raise( "" );
        }

        if ( other.CompareTag( "Interaction" ) || other.transform.TryGetComponent<Movable>( out var collided ) )
        {
            //Debug.Log( "collision" );
            if ( sticky )
            {
                transform.parent = originalParent;
                gameObject.tag = "Interaction";

            }
        }
    }

}
