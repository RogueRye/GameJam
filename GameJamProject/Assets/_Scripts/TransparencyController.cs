using System.Collections;
using UnityEngine;

public class TransparencyController : GameEventListener
{
    [SerializeField]
    private IntVariable stagesCleared;

    private SkinnedMeshRenderer skinRenderer;

    private void Start()
    {
        skinRenderer = GetComponent<SkinnedMeshRenderer>();    
    }

    public override void OnEventRaised()
    {
        base.OnEventRaised();
        StartCoroutine( DelayedUpdateMaterialCr() );
    }

    private IEnumerator DelayedUpdateMaterialCr()
    {
        yield return new WaitForSeconds( 0.4f );
        foreach(var mat in skinRenderer.materials )
        {
            var newColor = mat.color;
            newColor.a = (0.33f * stagesCleared.Value) + .2f;
            mat.color = newColor;


        }
    }
}
