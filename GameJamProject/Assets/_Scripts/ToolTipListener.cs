using TMPro;
using UnityEngine;

[RequireComponent(typeof( TextMeshProUGUI ) )]
public class ToolTipListener : GameEventListener
{
    private TextMeshProUGUI text;
    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    public override void OnEventRaised( string args )
    {
        text.text = args;
    }
}
