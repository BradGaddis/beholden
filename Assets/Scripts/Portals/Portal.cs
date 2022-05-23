using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] Vector3 rotateOffset = Vector3.zero;
    [SerializeField] float rotateSpeed = 0f;
    // Start is called before the first frame update
        void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotateOffset * rotateSpeed * Time.deltaTime);
    }
}
