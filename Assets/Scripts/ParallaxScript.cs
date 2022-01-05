using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScript : MonoBehaviour
{
    [SerializeField] GameObject[] imagePara;
    [SerializeField] float moveSpeed;

    private void Update()
    {
        foreach(GameObject image in imagePara)
        {
            image.transform.position += Vector3.left * moveSpeed * Time.deltaTime;

            if(image.transform.position.x <= -9.0f)
            {
                image.transform.position = new Vector3(4.5f, 0, 0);
            }
        }
    }

}
