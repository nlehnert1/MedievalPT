using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class JesterMover : MonoBehaviour
{
    public float zMax = 2.0f, xMax = 1.5f;
    public float zOffset = 1.0f, xOffset = 3.5f;
    private System.Random random;
    private Transform GOTransform;
    MeshRenderer[] meshRenderers;
    Material[] materials;
    bool fadingIn;
    bool fadingOut;
    AudioSource audio;
    public AudioClip splatSound;
    public AudioClip sadSound;
    public AudioClip tauntSound;
    public AudioClip laughSound;
    public AudioClip mismatchSound;
    public AudioClip mismatchSound2;
    public bool hitByBanana;
    public bool hitByTomato;
    public bool shouldBeHitByTomato;
    public bool shouldBeHitByBanana;

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
        audio = GetComponent<AudioSource>();
        shouldBeHitByTomato = true;
        shouldBeHitByBanana = false;
        audio.PlayOneShot(tauntSound);
    }


    public void OnCollisionEnter(Collision other)
    {
        Debug.Log("Touched OnTriggerEnter");
        hitByTomato = other.gameObject.tag.Equals("tomato");
        hitByBanana = other.gameObject.tag.Equals("banana");
        var goodCollision = (hitByTomato && shouldBeHitByTomato) || (hitByBanana && shouldBeHitByBanana);
        if (hitByTomato || hitByBanana)
        {
            //TODO: Spawn food particles
            audio.PlayOneShot(splatSound);
            Destroy(other.gameObject);
            if(goodCollision)
            {
                StartCoroutine(TeleportToNewLocation());
            }
            else
            {
                //flip a coin to see which audio clip for mismatches to play.
                int randVal = random.Next(0, 2);
                switch(randVal % 2)
                {
                    case 0:
                        audio.PlayOneShot(mismatchSound);
                        break;
                    case 1:
                        audio.PlayOneShot(mismatchSound2);
                        break;
                }
            }
        }
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
        foreach (Material material in materials)
        {
            StartCoroutine(FadeTo(material, 1, 0, 1.0f));
        }
        while (fadingOut)
        {
            yield return null;
        }

        int randVal = random.Next(0, 2);

        //25% chance to say a voice line when hit
        int soundVal = random.Next(0, 4);
        if(soundVal < 1)
        {
            int whichSound = random.Next(0, 3);
            switch(whichSound)
            {
                case 0:
                    audio.PlayOneShot(tauntSound);
                    break;
                case 1:
                    audio.PlayOneShot(sadSound);
                    break;
                case 2:
                    audio.PlayOneShot(laughSound);
                    break;
            }
        }
        if(randVal == 0)
        {
            shouldBeHitByTomato = true;
            shouldBeHitByBanana = false;
        }
        else
        {
            shouldBeHitByBanana = true;
            shouldBeHitByTomato = false;
        }
        float newX = float.Parse(random.NextDouble().ToString());
        float newZ = float.Parse(random.NextDouble().ToString());
        newTransform = new Vector3(newX * xMax + xOffset, transform.position.y, newZ * zMax - zOffset);
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
        newTransform = new Vector3(newX * xMax + xOffset, transform.position.y, newZ * zMax - zOffset);
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

    private void ApplyDamage()
    {
        StartCoroutine(TeleportTargetToNewLocation());
    }


}
