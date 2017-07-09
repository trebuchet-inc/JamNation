using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CommShader : MonoBehaviour {
	Material material;

	public Color _Color;
	[Range(0f, 1f)]
	public float Min;
	[Range(0f, 1f)]
	public float Moy;
	[Range(0f, 1f)]
	public float Max;
	[Range(0f, 0.01f)]
	public float Offset;
	
	void Awake()
	{
		material = new Material(Shader.Find("Custom/MerePatrie"));
	}

	void Update()
	{
		ApplyChange();
	}

	public void ApplyChange()
	{
		material.SetFloat("_Min", Min);
		material.SetFloat("_Max", Max);
		material.SetFloat("_Moy", Moy);
		material.SetFloat("_Offset", Offset);
		material.SetColor("_Color", _Color);
	}

	void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		Graphics.Blit(source, destination, material);
	}
}
