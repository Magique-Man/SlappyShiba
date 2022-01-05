using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawnerScript : MonoBehaviour
{
    [SerializeField] private float spawnTimer;
    private float timeCountdown;
    [SerializeField] private int score;
    [SerializeField] private static float moveSpeed;
    [SerializeField] private float moveSpeedVisible;
    public float moveSpeeder;

    [SerializeField] private GameObject _pipes;
    [SerializeField] private float pipeOffset;
    [SerializeField] static bool isNewPipeSpawn;

    public static float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public static bool IsNewPipeSpawn { get => isNewPipeSpawn; set => isNewPipeSpawn = value; }

    private void Start()
    {
        moveSpeed = 1.0f;
       
    }

    private void Update()
    {
        moveSpeeder = moveSpeed;
        moveSpeedVisible = moveSpeed;

        if(Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            MoveSpeed += 1.0f;
        }

        if(Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            MoveSpeed -= 1.0f;
        }


        if(timeCountdown <= 0.0f)
        {
            IsNewPipeSpawn = true;

            timeCountdown = spawnTimer;
        }

        if(IsNewPipeSpawn)
        {
            PipeSpeed();
            GameObject pipe = Instantiate(_pipes);
            pipe.transform.position = this.transform.position + new Vector3(0, Random.Range(-pipeOffset, pipeOffset));
            IsNewPipeSpawn = false;
        }

        //timeCountdown -= Time.deltaTime;
    }

    public void PipeSpeed()
    {
        score = ScoreScript.Score;

        if(score != 0 && score % 5 == 0)
        {
            MoveSpeed += 0.1f;
            Debug.Log("Current Move Speed: " + MoveSpeed);
        }
    }
}
