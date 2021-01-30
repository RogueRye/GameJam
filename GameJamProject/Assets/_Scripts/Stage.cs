using UnityEngine;

public class Stage : GameEventListener
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
        interactibles = GetComponentsInChildren<Interactible>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[RequireComponent(typeof(Stage))]
public class StageResetter : GameEventListener
{
    Stage stage;

    private void Start()
    {
        stage = GetComponent<Stage>();
    }

    public override void OnEventRaised()
    {
        base.OnEventRaised();
        foreach ( var item in stage.interactibles )
        {
            item.Reset();
        }
    }

}
