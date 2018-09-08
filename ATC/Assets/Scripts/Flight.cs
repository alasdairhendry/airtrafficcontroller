using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flight : MonoBehaviour {

    [SerializeField] protected string identifier = "A-837";
    public string Identifier { get { return identifier; } }

    [SerializeField] protected Vector2 position = new Vector2();

    [SerializeField] protected float startingRadius = 60.0f;  // Miles
    [SerializeField] protected float speed = 500.0f;  //MPH
    [SerializeField] protected float heading = 0.0f;  // Degrees 0 = North, 90 = East, 180 = South, 270 = West

    [SerializeField] protected float altitude = 11000;   // Metres
    public float Altitude { get { return altitude; } }

    [SerializeField] protected Airport targetDestination;
    protected Vector2 directionToCentre = new Vector2();
    protected Vector2 currentDirection = new Vector2();

    protected RectTransform rect;
    protected bool hasPingedThisRotation = false;
    protected RadarScreen radarScreen;

    // Use this for initialization
    protected virtual void Start () {
     
	}

    public virtual void Initialize(float startingRadius, string identifier)
    {
        rect = GetComponent<RectTransform>();
        radarScreen = GameObject.FindObjectOfType<RadarScreen>();
        radarScreen.OnPingRestart += () => { hasPingedThisRotation = false; };

        this.identifier = identifier;
        this.startingRadius = startingRadius;

        GetComponentInChildren<Text>().text = identifier;

        SetStartingPosition();
    }

    protected virtual void SetStartingPosition()
    {       
      
    }
	
	// Update is called once per frame
	protected virtual void Update () {
        UpdateHeading();
        UpdatePosition();
        CheckUpdateRadarPosition();        
    }

    protected virtual void UpdateHeading()
    {                
        if (heading < 0.0f) heading += 360.0f;
        currentDirection.x = Mathf.Sin(heading * Mathf.Deg2Rad);
        currentDirection.y = Mathf.Cos(heading * Mathf.Deg2Rad);
        currentDirection.Normalize();
    }

    protected virtual void UpdatePosition()
    {        
        position += currentDirection * ((speed * Time.deltaTime) / 60.0f / 60.0f);
    }

    protected virtual void CheckUpdateRadarPosition()
    {
        float angle = Mathf.Atan2(targetDestination.position.x - position.x, targetDestination.position.y - position.y) * Mathf.Rad2Deg;
        angle -= 180.0f;
        if (angle < 0.0f) angle += 360.0f;
        
        float secondsPerRotation = 60.0f / radarScreen.RotationsPerMinute;

        float pingInterval = Mathf.Lerp(0.0f, secondsPerRotation, angle / 360.0f);
        
        if(!hasPingedThisRotation)
        {
            if(radarScreen.CurrentTime >= pingInterval)
            {
                hasPingedThisRotation = true;
                UpdateRadarPosition();
                //Debug.Log("Ping");
            }            
        }
    }

    protected virtual void UpdateRadarPosition()
    {
        Vector2 newPosition = new Vector2();
        newPosition.x = Mathf.Lerp(-380.0f, 380.0f, Mathf.InverseLerp(-startingRadius + targetDestination.position.x, startingRadius + targetDestination.position.x, position.x));
        newPosition.y = Mathf.Lerp(-380.0f, 380.0f, Mathf.InverseLerp(-startingRadius + targetDestination.position.y, startingRadius + targetDestination.position.y, position.y));
        rect.anchoredPosition = newPosition;
    }

    public virtual void ModifyAltitude(float amount)
    {
        altitude += amount;
    }
}
