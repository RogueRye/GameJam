using UnityEngine;

public class Movable : Interactible
{
    [SerializeField]
    private float dragSpeed = 3;
    [SerializeField]
    private KeyCodeVariable interactButton;
    [SerializeField]
    private GameEvent toolTipEvent;

    private bool beingDragged;
    private Transform anchor;
    private Vector3 startPos;

    public override void Interact( GameObject other ) 
    {
        beingDragged = !beingDragged;
        anchor = other.transform;
        toolTipEvent.Raise( "" );
    }

    public override void Reset()
    {
        transform.position = startPos;
    }

    private void Start()
    {
        startPos = transform.position;    
    }

    private void Update()
    {
        if ( beingDragged )
        {
            var targetPos = new Vector3(anchor.position.x, transform.position.y, anchor.position.z);
            var step = dragSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPos, step);            
        }
    }

    private void OnTriggerEnter( Collider other )
    {
        if ( other.CompareTag( "Player" ) && !beingDragged)
        {
            toolTipEvent.Raise( $"Press {interactButton.KeyCode} to grab and move.\nPress {interactButton.KeyCode} again to let go." );
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
