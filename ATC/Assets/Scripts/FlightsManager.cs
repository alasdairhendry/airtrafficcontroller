using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightsManager : MonoBehaviour {

    public static FlightsManager singleton;

    private void Awake()
    {
        if (singleton == null)
            singleton = this;
        else if (singleton != this)
            Destroy(gameObject);
    }

    [SerializeField] private List<GameObject> flightPrefabs = new List<GameObject>();
    [SerializeField] private float outerRingDistance = 60.0f;

    private List<string> letters = new List<string> { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

    private List<Flight> currentFlights = new List<Flight>();
    public List<Flight> CurrentFlights { get { return currentFlights; } }

	// Use this for initialization
	void Start () {
        CreateFlightRandom();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void CreateFlight()
    {

    }

    private void CreateFlightRandom()
    {
        GameObject flight = Instantiate(flightPrefabs[Random.Range(0, flightPrefabs.Count)]);
        RectTransform flightRect = flight.GetComponent<RectTransform>();
        flight.transform.SetParent(GameObject.Find("Flights").transform);
        flightRect.localScale = Vector3.one;
        flightRect.anchoredPosition3D = Vector3.zero;

        string identifier = letters[Random.Range(0, letters.Count)] + "-" + (Random.Range(101, 1000));

        bool isTaken = false;

        do
        {
            foreach (Flight f in currentFlights)
            {
                if (f.Identifier == identifier)
                {
                    Debug.LogError("Flight identifer " + identifier + " is already taken");

                    isTaken = true;
                    identifier = "A" + (Random.Range(101, 1000));
                    break;
                }
            }

            if (!isTaken)
                break;
        } while (isTaken);

        flight.GetComponent<Flight>().Initialize(outerRingDistance, identifier);
        currentFlights.Add(flight.GetComponent<Flight>());
    }
}
