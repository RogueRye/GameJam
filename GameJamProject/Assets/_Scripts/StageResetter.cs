using UnityEngine;

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
            item.ResetItem();
        }
    }

}
