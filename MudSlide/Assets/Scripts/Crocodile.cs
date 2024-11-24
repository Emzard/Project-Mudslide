using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crocodile : MonoBehaviour
{
    public SkinnedMeshRenderer skinnedMeshRenderer;
    public Transform player;
    public int Swimming = 0;
    public int Bite = 1;

    public float amplitude = 50f;
    public float frequency = 3f;
    public float biteDistance = 15f;
    private bool isBiting = false;

    // Update is called once per frame
    void Update()
    {
       if(skinnedMeshRenderer != null)
       {
        float weight = amplitude * (Mathf.Sin(Time.time*frequency*2*Mathf.PI)*0.5f+0.5f);
        skinnedMeshRenderer.SetBlendShapeWeight(Swimming, weight);

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if(distanceToPlayer <= biteDistance && !isBiting)
        {
            TriggerBite();
        }
        else if(distanceToPlayer > biteDistance && isBiting)
        {
            StopBite();
        }
       } 
    }

    private void TriggerBite()
    {
        isBiting = true;
        skinnedMeshRenderer.SetBlendShapeWeight(Bite, 100f);
    }

    private void StopBite()
    {
        isBiting = false;
        skinnedMeshRenderer.SetBlendShapeWeight(Bite, 0f);
    }
}
