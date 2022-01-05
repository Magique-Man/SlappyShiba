using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawnScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PipeSpawnerScript.IsNewPipeSpawn = true;
    }
}
