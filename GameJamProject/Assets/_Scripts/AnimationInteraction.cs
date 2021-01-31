using UnityEngine;

public class AnimationInteraction : Interactible
{
    [SerializeField]
    private KeyCodeVariable interactButton;
    [SerializeField]
    private GameEvent toolTipEvent;
    [SerializeField]
    private bool motionInteract = true;
    [SerializeField]
    private bool isForever = true;

    [SerializeField]
    Animator[] animators;

    public override void Interact( GameObject interactor )
    {
        foreach(var anim in animators )
        {
            anim.SetTrigger( "Interact" );
        }
    }

    public override void ResetItem()
    {
        foreach(var anim in animators )
        {
            anim.SetTrigger( "Reset" );
        }
    }

    private void OnCollisionEnter( Collision collision )
    {
        if ( motionInteract && collision.transform.CompareTag("Interaction"))
        {
            Interact( collision.gameObject );
        }
    }

    private void OnTriggerEnter( Collider other )
    {
        if ( other.CompareTag( "Player" ) || other.CompareTag( "Interaction" ) )
        {
            if ( motionInteract )
            {
                Interact( other.gameObject );
                //Consider not showing tooltip
                //toolTipEvent.Raise( $"Stand or place block to activate." );
            }
            else if(other.CompareTag("Player"))
            {
                toolTipEvent.Raise( $"Press {interactButton.KeyCode} to activate." );
            }
        }
    }

    private void OnTriggerExit( Collider other )
    {
        if ( other.CompareTag( "Player" ) || other.CompareTag( "Interaction" ) )
        {
            toolTipEvent.Raise( "" );

            if(motionInteract && !isForever )
            {
                ResetItem(); 
            }

        }
    }

}
