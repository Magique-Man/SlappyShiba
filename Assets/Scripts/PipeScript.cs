using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeScript : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private int score;
    [SerializeField] private GameObject _present;

    private void Start()
    {
        moveSpeed = PipeSpawnerScript.MoveSpeed;

        //
        if(Random.Range(0, 100) > 85 && ScoreScript.Score > 3)
        {
            _present.SetActive(true);
        }
        else
        {
            _present.SetActive(false);
        }
    }

    private void Update()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        if(transform.position.x < -5)
        {
            Destroy(this.gameObject);
        }
    }
}
