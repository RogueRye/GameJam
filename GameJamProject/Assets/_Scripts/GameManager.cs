using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : GameEventListener
{
    [SerializeField]
    private Stage[] allStages;

    private Stage[] selectedStages = new Stage[3];

    // Start is called before the first frame update
    void Start()
    {
        //First Stage is Fixed
        selectedStages[ 0 ] = allStages[ 0 ];

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
