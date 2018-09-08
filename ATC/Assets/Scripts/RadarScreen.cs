using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarScreen : MonoBehaviour {

    [SerializeField] private float rotationsPerMinute = 5.0f;
    public float RotationsPerMinute { get { return rotationsPerMinute; } }
    [SerializeField] private RectTransform targetPin;

    private float currentRotation = 0.0f;
    private float currentTime = 0.0f;
    public float CurrentTime { get { return currentTime; } }

    public Action OnPingRestart;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        currentRotation += (360.0f * (rotationsPerMinute / 60.0f)) * Time.deltaTime;
        currentTime += Time.deltaTime;

        if (currentRotation >= 360.0f)
        {
            currentRotation = 0.0f;
            currentTime = 0.0f;
            if (OnPingRestart != null) OnPingRestart();
        }

        targetPin.eulerAngles = new Vector3(0.0f, 0.0f, -currentRotation);
	}
}
