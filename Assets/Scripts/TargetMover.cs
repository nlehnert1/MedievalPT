using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMover : MonoBehaviour
{
    public float zMax = 2.0f, xMax = 1.5f;
    public float zOffset = 1.0f, xOffset = 3.5f;
    private System.Random random;
    public Transform Center;
    MeshRenderer[] meshRenderers;
    Material[] materials;
    bool fadingIn;
    bool fadingOut;

    private void Start()
    {
        random = new System.Random();
        fadingIn = false;
        fadingOut = false;
        meshRenderers = gameObject.GetComponentsInChildren<MeshRenderer>();
        materials = new Material[meshRenderers.Length];
        int i = 0;
        foreach (MeshRenderer renderer in meshRenderers)
        {
            materials[i] = renderer.material;
            i++;
        }
    }
    IEnumerator TeleportTargetToNewLocation()
    {
        Debug.Log("zmax: " + zMax + ", xmax: " + xMax + ", zOffset: " + zOffset + ", xOffset: " + xOffset);
        Vector3 newTransform = new Vector3();
        yield return new WaitForSeconds(1.0f);
        foreach (Material material in materials)
        {
            StartCoroutine(FadeTo(material, 1, 0, 1.0f));
        }
        while (fadingOut)
        {
            yield return null;
        }

        float newX = float.Parse(random.NextDouble().ToString());
        float newZ = float.Parse(random.NextDouble().ToString());
        newTransform = new Vector3(newX * (xMax + xOffset), transform.position.y, newZ * zMax - zOffset);
        transform.position = newTransform;

        foreach (Material material in materials)
        {
            StartCoroutine(FadeTo(material, 0, 1, 1.0f));
        }
        while (fadingIn)
        {
            yield return null;
        }
        yield return new WaitForSeconds(1.5f);
    }

    /// <summary>
    /// Makes a material change its opacity over the course of `duration` seconds. The material must have its rendering mode set to transparent for this to work.
    /// </summary>
    /// <param name="targetMaterial"></param>
    /// <param name="startingOpacity"></param>
    /// <param name="targetOpacity"></param>
    /// <param name="duration"></param>
    /// <returns></returns>
    IEnumerator FadeTo(Material targetMaterial, float startingOpacity, float targetOpacity, float duration)
    {
        fadingOut = targetOpacity == 0;
        fadingIn = targetOpacity == 0;

        float t = 0;

        while (t < duration)
        {
            t += Time.deltaTime;
            Color c = targetMaterial.color;
            float blendValue = Mathf.Clamp01(t / duration);
            c.a = Mathf.Lerp(startingOpacity, targetOpacity, blendValue);
            targetMaterial.color = c;
            yield return null;
        }
        if (fadingIn) fadingIn = !fadingIn;
        if (fadingOut) fadingOut = !fadingOut;
    }

    private void ApplyDamage()
    {
        StartCoroutine(TeleportTargetToNewLocation());
    }
}
