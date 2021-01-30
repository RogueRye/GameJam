﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GetInteractionTarget : MonoBehaviour
{
    [SerializeField]
    private InteractionController controller;

    List<Interactible> items = new List<Interactible>();

    private void OnTriggerEnter( Collider other )
    {
        if ( other.CompareTag( "Interaction" ) )
        {
            var newItem = other.GetComponent<Interactible>();
            items.Add( newItem );
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
