using UnityEngine;

public class Stage : MonoBehaviour
{
    public Transform StartPos;

    public Transform GoalPos;

    public Transform BackConnector;

    public Transform FrontConnector;

    [HideInInspector]
    public Interactible[] interactibles;

    // Start is called before the first frame update
    void Start()
    {
        interactibles = GetComponentsInChildren<Interactible>(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
