using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirportManager : MonoBehaviour {

    public static AirportManager singleton;

    private void Awake()
    {
        if (singleton == null)
            singleton = this;
        else if (singleton != this)
            Destroy(gameObject);
    }

    [SerializeField] private List<Airport> airports = new List<Airport>();
    public List<Airport> Airports { get { return airports; } }
}
