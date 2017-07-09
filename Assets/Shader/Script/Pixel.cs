using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Pixel : MonoBehaviour {
	private Material material;

	public float Param;

	void Awake()
	{
		material = new Material(Shader.Find("Hidden/PostProcessing"));
	}

	void Update()
	{
		material.SetFloat("_Param", Param);
	}

	void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		Graphics.Blit(source, destination, material);
	}
}
