using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(0, 0, 0.1f);
        transform.Rotate(0, 2f, 0);
        transform.position = new Vector3(3f * Mathf.Cos(Time.time * 3f), 3f * Mathf.Sin(Time.time * 3f), 5f * Time.time);
    }
}
