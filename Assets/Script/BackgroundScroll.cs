using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public float scrollSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * scrollSpeed * Time.deltaTime);
        if (transform.position.y < -40f)
        {
            transform.position = new Vector3(transform.position.x, 40f, transform.position.z);
        }
    }
}
