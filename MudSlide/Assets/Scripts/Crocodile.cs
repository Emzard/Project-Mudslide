using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crocodile : MonoBehaviour
{
    public SkinnedMeshRenderer skinnedMeshRenderer;
    public int Swimming = 0;
    public int Bite = 1;

    public float amplitude = 50f;
    public float frequency = 1f;

    // Update is called once per frame
    void Update()
    {
       if(skinnedMeshRenderer != null)
       {
        float weight = amplitude * (Mathf.Sin(Time.time*frequency*2*Mathf.PI)*0.5f+0.5f);
        skinnedMeshRenderer.SetBlendShapeWeight(Swimming, weight);
       } 
    }
}
