using UnityEngine;
using System.Collections;

public class RotatingBody : MonoBehaviour {

    public float periodInMinutes = 1.0f;
    public bool tidalLock = true;
    float rotationSpeed;
    float speedCorrection;
    bool rotationFlag;

    // Use this for initialization
    void Start() {
        Setup();
    }

    void Setup() {
        rotationFlag = false;

        if (tidalLock == true)
        {
            speedCorrection = 0.0f;
        }
        else
        {
            rotationFlag = true;
            if (periodInMinutes > 0.0f)
            {
                rotationSpeed = 6.0f / periodInMinutes;
            }

            // Get orbital rotation period
            OrbitingBody script = transform.parent.gameObject.GetComponent<OrbitingBody>();
            if (script != null)
            {
                float orbitalPeriod = script.periodInMinutes;
                if (orbitalPeriod > 0.0f)
                {
                    speedCorrection = -6.0f / orbitalPeriod;
                } else
                {
                    speedCorrection = 0.0f;
                }
                rotationSpeed += speedCorrection;
            } else
            {
                rotationFlag = false;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate () {
        UpdateRotation();
	}

    void UpdateRotation()
    {
        if (rotationFlag == true)
        {
            transform.Rotate(Vector3.forward, Time.deltaTime * rotationSpeed);
        }
    }

    public void SetBodyProperties(StarSystemManager.BodyProperties props)
    {
        tidalLock = props.tidalLock;
        periodInMinutes = props.periodInMinutes;
        Setup();
    }
}
