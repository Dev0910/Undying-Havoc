using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="GameEvent")]
public class GameEvent : ScriptableObject
{
    public List<GameEventListner> listners = new List<GameEventListner>();

    public void Raise()
    {
        Raise(null, null);
    }

    public void Raise(object data)
    {
        Raise(null, data);
    }

    public void Raise(Component sender)
    {
        Raise(sender, null);
    }

    public void Raise(Component sender,object data)
    {
        foreach(GameEventListner listner  in listners)
        {
            listner.OnEventRaised(sender,data);
        }
    }
    public void RegisterListner(GameEventListner listner)
    {
        if(!listners.Contains(listner))
        {
            listners.Add(listner);
        }
    }
    public void UnRegisterListner(GameEventListner listner)
    {
        if(listners.Contains(listner))
        {
            listners.Remove(listner);
        }
    }
}
