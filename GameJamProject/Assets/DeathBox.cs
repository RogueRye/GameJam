using UnityEngine;

public class DeathBox : MonoBehaviour
{
    [SerializeField]
    private GameEvent deathEvent;

    private void OnTriggerEnter( Collider other )
    {
        if ( other.CompareTag( "Player" ) )
        {
            deathEvent.Raise(deathEvent.Id);
        }
    }
}
