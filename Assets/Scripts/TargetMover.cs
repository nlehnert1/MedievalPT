using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMover : MonoBehaviour
{
    //Range for this one should be lots of z, moderate amount of x
    int maxZ = 7, maxX = 3;
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

        int posX = random.Next(0, 2);
        int posZ = random.Next(0, 2);
        if(posX == 0)
        {
            posX = -1;
        }
        if(posZ == 0)
        {
            posZ = -1;
        }
        float newX = float.Parse(random.NextDouble().ToString());
        float newZ = float.Parse(random.NextDouble().ToString());
        newTransform = new Vector3(Center.position.x + (posX * newX * maxX), Center.position.y, Center.position.z + (posZ * newZ * maxZ));
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
