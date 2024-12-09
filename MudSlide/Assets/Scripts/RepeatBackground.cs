using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private Vector3 startPos;
    private float repeatWidth;
    public float repeatmagnitude;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        // repeatWidth = GetComponent<BoxCollider>().size.x / 2;
        repeatWidth = GetComponent<MeshCollider>().bounds.size.z / repeatmagnitude;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.back * 20 * Time.deltaTime, Space.World);

        if (transform.position.z < startPos.z - repeatWidth)
        {
            transform.position = startPos;
        }
    }
}

