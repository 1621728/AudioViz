﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioVizualizerParticle : MonoBehaviour
{
    ParticleSystem ps;
    public float force = 10.0f;
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    public AudioSource audioSource;
    public float updateStep = 0.1f;
    public int sampleDataLength = 1024;

    private float currentUpdateTime = 0f;

    public float clipLoudness;
    private float[] clipSampleData;
    public GameObject gO;

    public float sizeFactor = 1;

    public float minSize = 0;
    public float maxSize = 500;
    

    private void Awake()
    {
        clipSampleData = new float[sampleDataLength];
    }

    private void Update()
    {
        

        currentUpdateTime += Time.deltaTime;
        if (currentUpdateTime >= updateStep)
        {


            currentUpdateTime = 0f;
            audioSource.clip.GetData(clipSampleData, audioSource.timeSamples);
            clipLoudness = 0f;
            foreach (var sample in clipSampleData)
            {
                clipLoudness += Mathf.Abs(sample);
            }
            clipLoudness /= sampleDataLength;

            clipLoudness *= sizeFactor;
            clipLoudness = Mathf.Clamp(clipLoudness, minSize, maxSize);
            
            //To do : Change the return value into a variable.
            //ps.startLifetime = clipLoudness;
            ps.startSize = clipLoudness;
        }
    }

    //void LateUpdate()


    //{
    //    ParticleSystem.Particle[] particles = new ParticleSystem.Particle[ps.particleCount];

    //    ps.GetParticles(particles);

    //    for (int i = 0; i < particles.Length; i++)

    //    {
    //        ParticleSystem.Particle p = particles[i];
    //        Vector3 particleWorldPosition;
    //        if (ps.main.simulationSpace == ParticleSystemSimulationSpace.Local)

    //        {
    //            particleWorldPosition = transform.TransformPoint(p.position);
    //        }
    //        else if (ps.main.simulationSpace == ParticleSystemSimulationSpace.Custom)
    //        {
    //            particleWorldPosition = ps.main.customSimulationSpace.TransformPoint(p.position);
    //        }
    //        else
    //        {
    //            particleWorldPosition = p.position;
    //        }

    //        Vector3 directionToTarget = (target.position - particleWorldPosition).normalized;
    //        Vector3 seekForce = (directionToTarget * force) * Time.deltaTime;
    //        p.velocity += seekForce;
    //        particles[i] = p;
    //    }

    //    ps.SetParticles(particles, particles.Length);
    //}

}