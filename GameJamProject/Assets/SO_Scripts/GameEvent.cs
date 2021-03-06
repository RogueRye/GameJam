﻿using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameEvent : ScriptableObject
{
    public string Name;
    public int Id;
    private List<GameEventListener> listeners = new List<GameEventListener>();

    public void Raise()
    {
        for ( int i = listeners.Count - 1; i >= 0; i-- )
            listeners[ i ].OnEventRaised();
    }

    public void Raise( int args )
    {
        for ( int i = listeners.Count - 1; i >= 0; i-- )
            listeners[ i ].OnEventRaised( args );
    }

    public void Raise( string args )
    {
        for ( int i = listeners.Count - 1; i >= 0; i-- )
            listeners[ i ].OnEventRaised( args );
    }

    public void RegisterListener( GameEventListener listener ) => listeners.Add( listener );

    public void UnregisterListener( GameEventListener listener ) => listeners.Remove( listener );
}


public class GameEventListener : MonoBehaviour
{
    public GameEvent MyEvent;

    protected virtual void OnEnable()
    {
        try
        {
            MyEvent.RegisterListener( this );
        }
        catch
        {
            Debug.Log( gameObject.name + "missing event" );
        }
    }

    protected virtual void OnDisable()
    {
        MyEvent.UnregisterListener( this );
    }

    public virtual void OnEventRaised()
    {

    }
    public virtual void OnEventRaised( int args ) 
    {

    }
    public virtual void OnEventRaised( string args )
    {

    }
}
