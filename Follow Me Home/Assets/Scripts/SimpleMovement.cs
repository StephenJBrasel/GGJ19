using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    private void Update()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(Mathf.Abs(Input.GetAxis("Horizontal")*10), 0, Mathf.Abs(Input.GetAxis("Vertical")*10));
    }

}
