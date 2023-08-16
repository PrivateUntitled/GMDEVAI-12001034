using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RenderVignette : MonoBehaviour
{
    public Shader currentShader;
    public float grayScaleAmount;
    public float vignetteAmount;
    private Material currentMaterial;

    Material material
    {
        get
        {
            if (currentMaterial == null)
            {
                currentMaterial = new Material(currentShader);
                currentMaterial.hideFlags = HideFlags.HideAndDontSave;
            }
            return currentMaterial;
        }
    }

    public void SetVignetteAmount(int amount)
    {

    }


    // Use this for initialization
    void Start()
    {

    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (currentShader != null)
        {
            material.SetFloat("_LuminosityAmount", grayScaleAmount);
            material.SetFloat("_VignetteAmount", vignetteAmount);
            Graphics.Blit(source, destination, material);
        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }

    // Update is called once per frame
    void Update()
    {
        grayScaleAmount = Mathf.Clamp(grayScaleAmount, 0.0f, 1.0f);
        vignetteAmount = Mathf.Clamp(vignetteAmount, 0.0f, 1.0f);
    }

    private void OnDisable()
    {
        if (currentMaterial)
        {
            DestroyImmediate(currentMaterial);
        }
    }
}
