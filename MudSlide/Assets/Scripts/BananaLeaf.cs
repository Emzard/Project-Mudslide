using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BananaLeaf : MonoBehaviour
{
    public SkinnedMeshRenderer skinnedMeshRenderer;
    public int LeafDown = 1;
    public int LeafUp = 0;
    public float amplitude = 50f;
    public float frequency  = 1f;
    private bool onGround;


    // Update is called once per frame
    void Update()
    {
        if(skinnedMeshRenderer != null)
        {
            if(onGround)
            {
                float weight = amplitude*(Mathf.Sin(Time.time*frequency*2*Mathf.PI)*0.5f + 0.5f);
                skinnedMeshRenderer.SetBlendShapeWeight(LeafDown, weight);
                skinnedMeshRenderer.SetBlendShapeWeight(LeafUp, 0f);
            }

            else
            {
                if(!onGround)
                {
                    skinnedMeshRenderer.SetBlendShapeWeight(LeafDown, 0f);
                    skinnedMeshRenderer.SetBlendShapeWeight(LeafUp, amplitude);
                }
            }
            
        }
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            onGround = false;
        }
    }

}
