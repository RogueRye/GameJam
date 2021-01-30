using UnityEngine;

public class Movable : Interactible
{
    [SerializeField]
    private float dragSpeed = 3;

    private bool beingDragged;
    private Transform anchor;
    public override void Interact( GameObject other ) 
    {
        beingDragged = !beingDragged;
        anchor = other.transform;
        Debug.Log( "being dragged: " + beingDragged );
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

}
