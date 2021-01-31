using UnityEngine;

public class DeathBox : MonoBehaviour
{
    [SerializeField]
    private GameEvent deathEvent;

    private void OnTriggerEnter( Collider other )
    {
        deathEvent.Raise( deathEvent.Id );
    }

}
