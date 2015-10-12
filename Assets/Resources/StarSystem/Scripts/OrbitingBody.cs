using UnityEngine;
using System.Collections;

public class OrbitingBody : MonoBehaviour {

    public float periodInMinutes = 60.0f;
    public float semiMajorAxis = 100.0f;
    public float eccentricity = 0.0f;

    bool keplerOrbit;
    float theta;
    float distance;
    float speedFactor;
    Vector3 orbitalPosition;

    float orbitalSpeed;
    bool orbitingFlag;
    Transform bodyContainer;
    RotatingBody bodyScript;

    void Awake()
    {
        bodyContainer = transform.Find("BodyContainer");
        bodyScript = bodyContainer.gameObject.GetComponent<RotatingBody>();
        // DEBUG
        Debug.Log(bodyScript.tidalLock.ToString());
        Debug.Log(bodyScript.periodInMinutes.ToString());
    }

	// Use this for initialization
	void Start () {
        orbitingFlag = false;
        keplerOrbit = false;

        if (periodInMinutes > 0.0f)
        {
            orbitingFlag = true;
            orbitalSpeed = 6.0f / periodInMinutes;

            if (eccentricity > 0.0f)
            {
                keplerOrbit = true;
                if (eccentricity >= 1.0f)
                {
                    // Do not handle parabolic (1.0) or hyperbolic (>1.0) orbits yet.
                    eccentricity = 0.9f;
                }
            } else
            {
                speedFactor = 1.0f;
            }
        }

        orbitalPosition = new Vector3(semiMajorAxis, 0.0f, 0.0f);
        bodyContainer.localPosition = orbitalPosition;
    }

    // Update is called once per frame
    void FixedUpdate () {
        UpdateOrbit();
	}

    public void AddBody(GameObject body, StarSystemManager.BodyProperties props)
    {
        bodyScript.SetBodyProperties(props);
        body.transform.parent = bodyContainer;
    }

    void UpdateOrbit()
    {
        if (orbitingFlag == true)
        {
            if (keplerOrbit == true)
            {
                theta = transform.eulerAngles.z * Mathf.Deg2Rad;
                distance = semiMajorAxis * (1.0f - eccentricity * eccentricity) / (1.0f + eccentricity * Mathf.Cos(theta));
                speedFactor = Mathf.Sqrt(1.0f + eccentricity * eccentricity + 2.0f * eccentricity * Mathf.Cos(theta));
                orbitalPosition.x = distance;
                bodyContainer.localPosition = orbitalPosition;
            }

            transform.Rotate(Vector3.forward, Time.deltaTime * orbitalSpeed * speedFactor);
        }
    }
}
