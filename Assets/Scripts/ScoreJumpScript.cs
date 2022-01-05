using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreJumpScript : MonoBehaviour
{
    [SerializeField] private float scaleSpeed;
    [SerializeField] private float moveSpeed;
    private Vector2 scorePos = new Vector2(0, 3.5f);

    private void Update()
    {
        //transform.Translate(Vector2.up * Time.deltaTime * moveSpeed);
        //transform.Translate(Vector2.left * Time.deltaTime * moveSpeed);
        //transform.Translate(Vector3.MoveTowards(transform.position, new Vector2(0, 3.25f), Time.deltaTime * moveSpeed));
        transform.position = Vector2.Lerp(transform.position, scorePos, Time.deltaTime * moveSpeed);

        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, scaleSpeed * Time.deltaTime);

        if(transform.position.y >= 3)
        {
            Destroy(gameObject);
        }
    }
}
