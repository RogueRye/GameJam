using UnityEngine;

public class EventInteractible : Interactible
{
    [SerializeField]
    private GameEvent EventToFire;
    [SerializeField]
    private GameEvent tooltipEvent;
    [SerializeField]
    private KeyCodeVariable interactButton;

    public override void Interact( GameObject interactor )
    {
        EventToFire.Raise();
    }

    public override void ResetItem()
    {
    }

    private void OnTriggerEnter( Collider other )
    {
        if ( other.CompareTag( "Player" ) )
        {
            tooltipEvent.Raise( $"Press {interactButton.KeyCode} to {EventToFire.Name}" );
        }
    }

    private void OnTriggerExit( Collider other )
    {
        if ( other.CompareTag( "Player" ) )
        {
            tooltipEvent.Raise( "" );
        }
    }


}
