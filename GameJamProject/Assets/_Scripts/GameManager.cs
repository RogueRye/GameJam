using UnityEngine;

public class GameManager : GameEventListener
{
    [SerializeField]
    private Stage[] allStages;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameEvent wonGameEvent;

    private Stage[] selectedStages = new Stage[ 3 ];

    int currentIndex;
    // Start is called before the first frame update
    void Start()
    {
        //First Stage is Fixed
        selectedStages[ 0 ] = allStages[ 0 ];
        currentIndex = 0;
        player.transform.position = selectedStages[ currentIndex ].StartPos.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Stage beat
    public override void OnEventRaised( int args )
    {
        base.OnEventRaised( args );
        currentIndex++;

        if ( currentIndex > selectedStages.Length )
        {
            wonGameEvent.Raise();
        }
        else
        {
            selectedStages[ currentIndex ].gameObject.SetActive( true );
            selectedStages[ currentIndex - 1 ].gameObject.SetActive( false );
            player.transform.position = selectedStages[ currentIndex ].StartPos.position;
        }
    }
}
