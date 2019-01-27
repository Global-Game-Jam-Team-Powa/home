using UnityEngine;
using System.Collections;

public class ParticleController : MonoBehaviour
{

    public ParticleSystem particles;
    private void Start()
    {
        particles = this.GetComponent<ParticleSystem>();
    }
    // Update is called once per frame
    void Update()
    {
        //After 4 seconds, pause particles
        if (particles.particleCount<=2)
        {
            particles.Pause();
        }
    }
}