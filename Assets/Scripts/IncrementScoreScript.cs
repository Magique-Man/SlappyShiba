using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncrementScoreScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ScoreScript.Score++;
    }

}
