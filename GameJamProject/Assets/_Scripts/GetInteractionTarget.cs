using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GetInteractionTarget : MonoBehaviour
{
    [SerializeField]
    private InteractionController controller;

    List<Interactible> items = new List<Interactible>();
    private Collider col;

    private void Start()
    {
        col = GetComponent<Collider>();
    }

    private void Update()
    {
        col.enabled = !controller.HoldingItem; 
    }

    private void OnTriggerEnter( Collider other )
    {
        if ( other.CompareTag( "Interaction" ) )
        {
            Debug.Log( "hi" );
            var newItem = other.GetComponent<Interactible>();
            if ( !items.Contains( newItem ) )
            {
                items.Add( newItem );
            }

            controller.SetLastKnownInteractible( newItem );
        }
    }

    private void OnTriggerExit( Collider other )
    {
        if ( !other.CompareTag( "Interaction" ) )
        {
            return;
        }
        var itemThatLeft = other.GetComponent<Interactible>();
        if ( !items.Contains( itemThatLeft ) )
        {
            return;
        }

        items.Remove( itemThatLeft );

        if ( items.Count > 0 )
        {
            controller.SetLastKnownInteractible( items.Last() );
        }
        else
        {
            controller.UnassignInteractible();
        }
    }

}
