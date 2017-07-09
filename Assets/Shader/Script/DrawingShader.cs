using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DrawingShader : MonoBehaviour
{
    private Material material;

    public Color _Color;
    [Range(0f, 0.01f)]
    public float Offset;
    [Range(0f, 1f)]
    public float Silhouette;
    [Range(0f, 0.1f)]
    public float lignSensitivity;

    [Range(0f, 3f)]
    public float Palier1;
    [Range(0f, 3f)]
    public float Palier2;
    [Range(0f, 3f)]
    public float Palier3;
    [Range(0f, 3f)]
    public float Palier4;

    [Range(0f, 1f)]
    public float Mix;

    public float Saturation;

    [Range(0f, 1f)]
    public float Threshold;

    void Awake()
    {
        material = new Material(Shader.Find("Custom/Drawing"));
    }

    void Update()
    {
        ApplyChange();
    }

    public void ApplyChange()
    {
        material.SetFloat("_Offset", Offset);
        material.SetFloat("_Silhouette", Silhouette);
        material.SetFloat("_LignSensitivity", lignSensitivity);
        material.SetColor("_Color", _Color);
        material.SetFloat("_Palier1", Palier1);
        material.SetFloat("_Palier2", Palier2);
        material.SetFloat("_Palier3", Palier3);
        material.SetFloat("_Palier4", Palier4);
        material.SetFloat("_Mix", Mix);
        material.SetFloat("_Saturation", Saturation);
        material.SetFloat("_Threshold", Threshold);
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, material);
    }
}