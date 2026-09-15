using System;
using UnityEngine;

public class SpikeEmissionControl : MonoBehaviour
{
    private Transform spikeBone; // Assign the bone that controls the spike movement
    public float minZ = 0f; // Lowest Z position (retracted)
    public float maxZ = 0.01f; // Highest Z position (fully extended)
    public float emissionMin = 0.2f;
    public float emissionMax = 2.0f;
    public float emissionSmoothSpeed = 5f;

    [SerializeField] private Material spikeMaterial;
    private float currentEmission;

    private void Start()
    {
        spikeBone = GetComponent<Transform>();
    }

    void Update()
    {
        // Normalize Z position between 0 and 1
        float zPos = Mathf.Clamp01((spikeBone.localPosition.z - minZ) / (maxZ - minZ));

        // Map to emission range
        float targetEmission = Mathf.Lerp(emissionMin, emissionMax, zPos);

        // Smooth transition
        currentEmission = Mathf.Lerp(currentEmission, targetEmission, Time.deltaTime * emissionSmoothSpeed);

        // Apply emission
        Color baseColor = spikeMaterial.GetColor("_Color");
        Color finalEmissionColor = baseColor * currentEmission;

        spikeMaterial.SetColor("_EmissionColor", finalEmissionColor);
    }
}