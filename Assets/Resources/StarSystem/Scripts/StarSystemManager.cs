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
        // This will be added to the root of the system,
        // Stars and main planets will belong to this level.
        GameObject newBody = CreateNewBody(bodyName, body);

        OrbitingBody orbitScript = CreateNewOrbit(bodyName, parentBody, orbit);

        orbitScript.AddBody(newBody, body);
    }

    OrbitingBody CreateNewOrbit(string name, string parent, OrbitProperties props)
    {
        OrbitingBody script;
        GameObject newOrbit;
        newOrbit = (GameObject)Instantiate(orbitPrefab, transform.position, transform.rotation);
        newOrbit.name = name + "Orbit";

        if (parent == "")
        {
            newOrbit.transform.parent = transform;
        }
        else
        {
            // Moons and other bodies orbiting around greater masses will be added here.
            GameObject newParent = GameObject.Find(parent + "Orbit/BodyContainer");
            newOrbit.transform.parent = newParent.transform;

        }

        script = newOrbit.GetComponent<OrbitingBody>();
        script.eccentricity = props.eccentricity;
        script.semiMajorAxis = props.semiMajorAxis;
        script.periodInMinutes = props.period;
        
        return script;
    }

    GameObject CreateNewBody(string bodyName, BodyProperties props)
    {
        GameObject newBody;
        newBody = (GameObject)Instantiate(planetPrefab, transform.position, transform.rotation);
        newBody.name = bodyName;
        newBody.transform.localScale = new Vector3(props.size, props.size, 1.0f);
        SpriteRenderer renderer = newBody.GetComponent<SpriteRenderer>();
        renderer.color = props.color;
        return newBody;
    }

    // This method will be used to query gravitational acceleration in a given point in system.
    public Vector2 CalculateGravityAt(Vector3 position)
    {
        Vector2 gravityVector = new Vector2(0.0f, 0.0f);
        // TODO
        return gravityVector;
    }
}
