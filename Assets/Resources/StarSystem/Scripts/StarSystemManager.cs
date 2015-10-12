using UnityEngine;
using System.Collections;

public class StarSystemManager : MonoBehaviour {

    public struct BodyProperties
    {
        public float mass;
        public float size;
        public bool tidalLock;
        public float periodInMinutes;
        public Color color;
    }

    public struct OrbitProperties
    {
        public float semiMajorAxis;
        public float eccentricity;
        public float initialPosition;
        public float period;
    }

    GameObject planetPrefab;
    GameObject orbitPrefab;

    public void Test()
    {
        Debug.Log("Testing SolarSystemManager");
    }

    // Use for initialization
	void Awake() {
        Setup();
	}

    // This method will be used to create star system randomly.
    public void GenerateRandomSystem()
    {
        // TODO : Will be implemented in future.
    }

    void Setup()
    {
        // load planet prefab.
        planetPrefab = LoadPrefab("CelestialBodyPrefab");
        orbitPrefab = LoadPrefab("OrbitPrefab");
    }

    GameObject LoadPrefab(string name)
    {
        GameObject prefab = Resources.Load<GameObject>("StarSystem/Prefabs/" + name);
        if (prefab == null)
        {
            Debug.Log(name + " can not be loaded!");
            Debug.Break();
        } else
        {
            Debug.Log(name + " is loaded.");
        }
        return prefab;
    }

    // This method will be used to create star system manually, or to customize it.
    public void AddBodyTo(string parentBody, BodyProperties body, OrbitProperties orbit, string bodyName)
    {
        if (parentBody == "")
        {
            // This will be added to the root of the system,
            // Stars and main planets will belong to this level.
            GameObject newBody;
            newBody = (GameObject) Instantiate(planetPrefab, transform.position, transform.rotation);
            newBody.name = bodyName;
            newBody.transform.localScale = new Vector3(body.size, body.size, 1.0f);
            SpriteRenderer renderer = newBody.GetComponent<SpriteRenderer>();
            renderer.color = body.color;

            GameObject newOrbit;
            newOrbit = (GameObject)Instantiate(orbitPrefab, transform.position, transform.rotation);
            newOrbit.name = bodyName + "Orbit";
            newOrbit.transform.parent = transform;

            OrbitingBody orbitScript = newOrbit.GetComponent<OrbitingBody>();
            orbitScript.eccentricity = orbit.eccentricity;
            orbitScript.semiMajorAxis = orbit.semiMajorAxis;
            orbitScript.periodInMinutes = orbit.period;

            orbitScript.AddBody(newBody, body);

        } else
        {
            // Moons and other bodies orbiting around greater masses will be added here.
        }
    }

    // This method will be used to query gravitational acceleration in a given point in system.
    public Vector2 CalculateGravityAt(Vector3 position)
    {
        Vector2 gravityVector = new Vector2(0.0f, 0.0f);
        // TODO
        return gravityVector;
    }
}
