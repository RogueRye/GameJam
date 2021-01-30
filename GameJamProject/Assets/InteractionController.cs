using UnityEngine;

[RequireComponent(typeof(ThirdPersonMovement))]
public class InteractionController : MonoBehaviour
{
    [SerializeField]
    private KeyCode interactKey = KeyCode.F;
    [SerializeField]
    private GameObject anchor;

    private ThirdPersonMovement movement;
    private Interactible lastInteractible;
    private bool hasInteractible;

    private void Start()
    {
        movement = GetComponent<ThirdPersonMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(hasInteractible && Input.GetKeyDown( interactKey ) )
        {
            lastInteractible.Interact( anchor );

            if(lastInteractible is Movable )
            {
                movement.ToggleRotationLock();
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
    }
}
