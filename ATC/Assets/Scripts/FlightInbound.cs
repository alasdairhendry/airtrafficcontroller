using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightInbound : Flight {

	protected override void Start()
    {
        base.Start();
    }

    public override void Initialize(float startingRadius, string identifier)
    {
        base.Initialize(startingRadius, identifier);
    }

    protected override void SetStartingPosition()
    {
        Debug.Log("SetStartingPosition");
        targetDestination = AirportManager.singleton.Airports[1];

        Vector2 direction = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
        //Vector2 direction = new Vector2(0.0f, 1.0f).normalized;
        Debug.Log(startingRadius);
        position = startingRadius * direction;
        position += targetDestination.position;

        heading = Mathf.Atan2(targetDestination.position.x - position.x, targetDestination.position.y - position.y) * Mathf.Rad2Deg;
        altitude = Random.Range(altitude * 0.90f, altitude * 1.10f);
        speed = Random.Range(speed * 0.90f, speed * 1.10f);

        UpdateRadarPosition();
    }

    protected override void Update()
    {
        base.Update();
    }


}
