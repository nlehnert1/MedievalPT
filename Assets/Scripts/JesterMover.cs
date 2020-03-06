using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class JesterMover : MonoBehaviour
{
    public int test;
    public float zMax = 4.0f, xMax = 3.0f;
    public float zOffset = 2, xOffset = 7;
    private System.Random random;
    private Transform GOTransform;
    MeshRenderer meshRenderer;
    Material material;
    bool fadingIn;
    bool fadingOut;
    AudioSource audio;
    public AudioClip splatSound;
    public bool hitByBanana;
    public bool hitByTomato;

    private void Start()
    {
        random = new System.Random();
        fadingIn = false;
        fadingOut = false;
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        material = meshRenderer.material;
        audio = GetComponent<AudioSource>();

    }


    public void OnCollisionEnter(Collision other)
    {
        Debug.Log("Touched OnTriggerEnter");
        hitByTomato = other.gameObject.tag.Equals("tomato");
        hitByBanana = other.gameObject.tag.Equals("banana");
        if (hitByTomato || hitByBanana)
        {
            //TODO: Spawn food particles
            audio.PlayOneShot(splatSound);
            StartCoroutine(TeleportToNewLocation());
            Destroy(other.gameObject);
        }
    }

    /// <summary>
    /// 
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

        while(t < duration)
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

    IEnumerator TeleportToNewLocation()
    {
        Debug.Log("zmax: " + zMax + ", xmax: " + xMax + ", zOffset: " + zOffset + ", xOffset: " + xOffset);
        Vector3 newTransform = new Vector3();
        yield return new WaitForSeconds(1.0f);
        //StartCoroutine(FadeTo(material, 1, 0, 1.0f));            
        while (fadingOut)
        {
            yield return null;
        }

            float newX = float.Parse(random.NextDouble().ToString());
            float newZ = float.Parse(random.NextDouble().ToString());
            newTransform = new Vector3(newX * xMax + xOffset, transform.position.y, newZ * zMax - zOffset);
            transform.position = newTransform;


        //StartCoroutine(FadeTo(material, 0, 1, 1.0f));
        while (fadingIn)
        {
            yield return null;
        }
        yield return new WaitForSeconds(1.5f);
    }
}
