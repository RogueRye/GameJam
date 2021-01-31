using UnityEngine;

public class GameManager : GameEventListener
{
    [SerializeField]
    private GameEvent deathEvent;

    [SerializeField]
    private Stage[] allStages;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameEvent wonGameEvent;

    [SerializeField]
    private GameEvent resetEvent;

    [SerializeField]
    private IntVariable stagesCleared;

    [SerializeField]
    private GameObject[] stagePrizesPrefabs;

    private Stage[] selectedStages = new Stage[ 3 ];

    int currentIndex;
    // Start is called before the first frame update
    void Start()
    {
        //First Stage is Fixed
        selectedStages[ 0 ] = allStages[ 0 ];
        selectedStages[ 1 ] = allStages[ 1 ];
        currentIndex = 0;
        stagesCleared.Value = 0;
        PrepStage();
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected override void OnEnable()
    {
        base.OnEnable();
        deathEvent.RegisterListener( this );
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        deathEvent.UnregisterListener( this );
    }

    private void PrepStage()
    {
        player.GetComponent<CharacterController>().enabled = false;
        player.transform.position = selectedStages[ currentIndex ].StartPos.position;
        player.GetComponent<CharacterController>().enabled = true;

        var prizeGo = Instantiate( stagePrizesPrefabs[ currentIndex ] , selectedStages[ currentIndex ].GoalPos );
        prizeGo.transform.localPosition = Vector3.zero;
    }

    //Stage beat
    public override void OnEventRaised()
    {
        currentIndex++;
        stagesCleared.Value++;

        if ( currentIndex > selectedStages.Length )
        {
            wonGameEvent.Raise();
        }
        else
        {
            selectedStages[ currentIndex ].gameObject.SetActive( true );
            selectedStages[ currentIndex - 1 ].gameObject.SetActive( false );
            PrepStage();
        }
    }

    public override void OnEventRaised( int args )
    {
        if(args == deathEvent.Id )
        {
            player.GetComponent<CharacterController>().enabled = false;
            player.transform.position = selectedStages[ currentIndex ].StartPos.position;
            player.GetComponent<CharacterController>().enabled = true;
            resetEvent.Raise();
        }
    }
}
