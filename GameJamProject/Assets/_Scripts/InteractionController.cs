using UnityEngine;

[RequireComponent(typeof(ThirdPersonMovement))]
public class InteractionController : MonoBehaviour
{
    [SerializeField]
    private KeyCodeVariable interactKey;
    [SerializeField]
    private GameObject anchor;

    private ThirdPersonMovement movement;
    private Interactible lastInteractible;

    private bool hasInteractible;

    [HideInInspector]
    public bool HoldingItem;


    private void Start()
    {
        movement = GetComponent<ThirdPersonMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(hasInteractible && Input.GetKeyDown( interactKey.KeyCode ) )
        {
            lastInteractible.Interact( anchor );

            if(lastInteractible is Movable )
            {
                movement.ToggleRotationLock();
                HoldingItem = !HoldingItem;
            }
        }

    }

    public void SetLastKnownInteractible(Interactible newItem)
    {
        lastInteractible = newItem;
        hasInteractible = true;
    }
    public void UnassignInteractible()
    {
        hasInteractible = false;
        lastInteractible = null;
    }
}
