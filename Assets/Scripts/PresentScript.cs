using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresentScript : MonoBehaviour
{
    [SerializeField] private GameObject _scoreFloat;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            ScoreScript.Score += 5;
            GameObject floatingText = Instantiate(_scoreFloat);
            floatingText.transform.position = this.transform.position;
            this.gameObject.SetActive(false);  
        }
    }
}
