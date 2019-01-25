using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : MonoBehaviour {

    private ParticleSystem particleSystem;
    void Start() {
        particleSystem = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update() {
        Invoke("TurnOffSmokes", 10f);
    }

    private void TurnOffSmokes()
    {
        particleSystem.loop = false;
        Invoke("DestroySmoke", 11f);
    }

    private void DestroySmoke()
    {
        if (gameObject.name != "WaterFall")
            FindObjectOfType<EnvironementalEvents>().ResetTimer();

        Destroy(gameObject);
    }

    public void Reset()
    {
        if (gameObject.name != "WaterFall")
            FindObjectOfType<EnvironementalEvents>().ResetTimer();

        Destroy(gameObject);
    }
}
