using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICommand_ChangeAltitude : AICommand {

    public float altitudeChange;
    private float startingAltitude;

    private float maximumAltitudeChangePer = 1.0f;

    protected override void Start()
    {
        base.Start();
    }

    protected override void OnBegin()
    {
        if (altitudeChange < 0)
            status = "Reducing altitude by " + altitudeChange * -1.0f;
        else status = "Increasing altitude by " + altitudeChange;

        commandAcceptedOutput = "[" + flight.Identifier + "] alt mod of " + altitudeChange + " ack.";
        commandFinishedOutput = "Altitude change complete";

        startingAltitude = flight.Altitude;

        base.OnBegin();
    }

    protected override void Update()
    {
        float modifyAmount = Mathf.Clamp(altitudeChange * 0.10f * Time.deltaTime, -maximumAltitudeChangePer, maximumAltitudeChangePer);        
        Debug.Log(modifyAmount);

        flight.ModifyAltitude(modifyAmount);

        if (altitudeChange < 0)
        {
            if(flight.Altitude <= startingAltitude + altitudeChange)
            {
                OnEnd();
            }
        }
        else
        {            
            if(flight.Altitude >= startingAltitude + altitudeChange)
            {
                OnEnd();
            }
        }
    }

    protected override void OnEnd()
    {
        base.OnEnd();
    }
}
