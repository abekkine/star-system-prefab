using UnityEngine;
using System.Collections;

public class Vicinity : MonoBehaviour {

    private GameObject system;

    void Awake()
    {
        system = GameObject.Find("/StarSystem");
        if (system == null)
        {
            Debug.Log("/StarSystem instance can not be found!");
            Debug.Break();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        EnterVicinity(other);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        ExitVicinity(other);
    }

    void EnterVicinity(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = transform;
        }
    }

    void ExitVicinity(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = system.transform;
        }
    }
}
