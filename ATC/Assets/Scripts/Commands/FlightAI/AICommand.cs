using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICommand : MonoBehaviour {

    protected string status;
    public string GetStatus { get { return status; } }

    protected string commandAcceptedOutput;
    protected string commandFinishedOutput;

    protected Flight flight;

    protected virtual void Start()
    {
        flight = GetComponent<Flight>();
        OnBegin();
    }

	protected virtual void OnBegin()
    {
        ControlPanel.singleton.Output(commandAcceptedOutput, CustomHelper.NewColor(91, 226, 106));
    }

    protected virtual void Update()
    {

    }

    protected virtual void OnEnd()
    {
        ControlPanel.singleton.Output(commandFinishedOutput, CustomHelper.NewColor(91, 226, 106));
        Destroy(this);
    }
}