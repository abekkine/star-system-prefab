﻿using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    StarSystemManager systemManager;

	// Use this for initialization
	void Awake () {

        GameObject system = CreateStarSystemFromPrefab();
        systemManager = system.GetComponent<StarSystemManager>();
        //systemManager.Test();

        StarSystemManager.BodyProperties body;
        StarSystemManager.OrbitProperties orbit;

        // Sun of system
        body.color = new Color(1.0f, 1.0f, 0.0f);
        body.mass = 5000.0f;
        body.size = 1.0f;
        body.tidalLock = false;
        body.periodInMinutes = 1.0f;

        orbit.semiMajorAxis = 0.0f;
        orbit.eccentricity = 0.0f;
        orbit.initialPosition = 0.0f;
        orbit.period = 1.0f;

        systemManager.AddBodyTo("", body, orbit, "sun");

        // A single planet
        body.color = new Color(0.0f, 1.0f, 0.0f);
        body.mass = 1000.0f;
        body.size = 1.0f;
        body.tidalLock = true;
        body.periodInMinutes = 1.0f;

        orbit.semiMajorAxis = 100.0f;
        orbit.eccentricity = 0.0f;
        orbit.initialPosition = 0.0f;
        orbit.period = 5.0f;

        systemManager.AddBodyTo("", body, orbit, "earth");

    }

    // Update is called once per frame
    void Update () {
	
	}

    GameObject CreateStarSystemFromPrefab()
    {
        GameObject starSystem = null;
        GameObject prefab = Resources.Load<GameObject>("StarSystem/Prefabs/StarSystemPrefab");
        if (prefab == null)
        {
            Debug.Log("StarSystemPrefab can not be loaded!");
            Debug.Break();
        } else
        {
            Debug.Log("StarSystemPrefab is loaded.");

            starSystem = (GameObject)Instantiate(prefab, transform.position, transform.rotation);
            starSystem.name = "StarSystem";
        }
        return starSystem;
    }
}