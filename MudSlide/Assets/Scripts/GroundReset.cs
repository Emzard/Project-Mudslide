using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundReset : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed = 5f;
    public float resetDistance = 20f;

    private Vector3 startPosition;
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.back*speed*Time.deltaTime);

        if(Vector3.Distance(startPosition, transform.position) >= resetDistance)
        {
            transform.position = startPosition;
        }
    }
}
