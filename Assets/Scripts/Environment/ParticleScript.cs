using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : MonoBehaviour
{

    private ParticleSystem particleSystem;

    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        Invoke("TurnLoopOff", 10f);
    }

    // De loop property van het particle systeem uitzetten en dan na x secondes het particle object verwijderen
    private void TurnLoopOff()
    {
        particleSystem.loop = false;
        Invoke("DestroyParticle", 11f);
    }

    // Methoden om het particle object te verwijderen
    private void DestroyParticle()
    {
        if (gameObject.name != "WaterFall")
            FindObjectOfType<EnvironementalEvents>().ResetTimer();

        Destroy(gameObject);
    }

    public void Reset()
    {
        Destroy(gameObject);
    }
}