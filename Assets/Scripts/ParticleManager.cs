using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class ParticleManager : MonoBehaviour
{
    [SerializeField, Min(1)] 
    private int maxParticleSystems;
    [SerializeField]
    private Transform ParticlePrefab;

    [SerializeField]
    private AudioClip popSound;
    
    private Transform[] ParticleSystems;
    
    // Start is called before the first frame update
    void Start()
    {
        ParticleSystems = new Transform[maxParticleSystems];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayParticle(Vector2 pos)
    {
        Transform ps = GetFreeParticleSystem();
        if (ps == null)
            ps = GetOldestParticleSystem();
        
        ps.position = pos;
        ps.GetComponent<ParticleSystem>().Play();
        PlayClipAtPoint();
    }

    private Transform GetFreeParticleSystem()
    {
        for (int i = 0; i < maxParticleSystems; i++)
        {
            if (ParticleSystems[i] == null)
            {
                ParticleSystems[i] = Instantiate(ParticlePrefab, this.transform);
                return ParticleSystems[i];
            }

            ParticleSystem ps = ParticleSystems[i].GetComponent<ParticleSystem>();
            if(!ps.isPlaying)
                return ParticleSystems[i]; 
        }

        return null;
    }

    private Transform GetOldestParticleSystem()
    {
        Transform rtnT = null;
        float leastTime = float.MaxValue;

        foreach (Transform t in ParticleSystems)
        {
            ParticleSystem ps = t.GetComponent<ParticleSystem>();
            float remainingTime = ps.main.duration - ps.time;
            
            if (!(remainingTime < leastTime)) continue;
            
            leastTime = remainingTime;
            rtnT = t;
        }

        return rtnT;
    }
    
    // Method to play an audio clip at a specific position
    public void PlayClipAtPoint(float volume = 1.0f)
    {
        GameObject tempGO = new GameObject("TempAudio"); // create the temp object
        AudioSource aSource = tempGO.AddComponent<AudioSource>(); // add an audio source
        aSource.clip = popSound; // define the clip
        aSource.volume = volume;
        aSource.Play(); // start the sound
        Object.Destroy(tempGO, popSound.length); // destroy object after clip duration
    }
}
