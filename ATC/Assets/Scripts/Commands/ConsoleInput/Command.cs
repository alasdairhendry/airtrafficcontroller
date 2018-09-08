using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Command {

    protected string identifier = "";
    protected bool identifierVaries = false;
    protected int sections = 1;

    public Command()
    {
        
    }

    public virtual bool CheckCommand(string command)
    {
        Debug.Log("Dont use this");
        return false;        
    }

    public virtual void InvokeCommand(string command)
    {

    }

    protected string[] SplitCommand(string command)
    {
        return command.Split(' ');
    }

    protected Flight FindFlight(string identifier)
    {
        foreach (Flight f in FlightsManager.singleton.CurrentFlights)
        {
            if (identifier == f.Identifier) return f;
        }

        return null;
    }
}
