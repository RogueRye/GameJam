using UnityEngine;

public abstract class Interactible : MonoBehaviour
{
    public abstract void Interact(GameObject interactor);

    public abstract void ResetItem();

    private void OnDisable()
    {
        ResetItem();
    }
}
