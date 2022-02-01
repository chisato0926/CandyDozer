using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube2 : MonoBehaviour
{
    Vector3 axis = Vector3.zero;

    // Start is called before the first frame update
    void Start() {
        StartCoroutine(RotateCube());
    }

    // Update is called once per frame
    void Update() {
        transform.Rotate(axis);
    }

    IEnumerator RotateCube() {
        axis.y = 1f;
        yield return new WaitForSeconds(3f);
        axis.y = 0;
        axis.x = 1f;
        yield return new WaitForSeconds(2f);
        axis.x = 0;
        axis.z = 1f;
    }
}
