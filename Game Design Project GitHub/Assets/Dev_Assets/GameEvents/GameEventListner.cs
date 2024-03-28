using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CustomGameEvents : UnityEvent<Component, object> { }

public class GameEventListner : MonoBehaviour
{
    [Tooltip("Event to register with.")]
    public GameEvent gameEvent;

    [Tooltip("Response to invoke when Event with GameData is raised.")]
    public CustomGameEvents response;

    private void OnEnable()
    {
        gameEvent.RegisterListner(this);
    }
    private void OnDisable()
    {
        gameEvent.UnRegisterListner(this);
    }

    public void OnEventRaised(Component sender,object data)
    {
        response.Invoke(sender,data);
    }


}
