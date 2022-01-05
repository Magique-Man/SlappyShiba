using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] GameManagerScript _gameManagerScript;

    [SerializeField] private float jumpVelocity;
    [SerializeField] private float gravityScale;
    [SerializeField] private float angleCoefficient;

    private Rigidbody2D _rigidbody2D;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.gravityScale = gravityScale;
        _rigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    private void Update()
    {
        _rigidbody2D.gravityScale = gravityScale;

        if(transform.position.y > 3.82)
        {
            transform.position = new Vector2(transform.position.x, 3.82f);
        }


        if(!_gameManagerScript.IsGameStarted)
        {
            transform.position = Vector2.zero;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if(Input.GetButtonDown("Jump") && !_gameManagerScript.IsGameOver && !_gameManagerScript.IsPaused)
        {
            if(!_gameManagerScript.IsGameStarted)
            {
                transform.position = Vector2.zero;
                transform.rotation = Quaternion.Euler(0, 0, 0);
                _gameManagerScript.IsGameStarted = true;
                _rigidbody2D.velocity = Vector2.up * jumpVelocity;
            }
            else
            {
                _rigidbody2D.velocity = Vector2.up * jumpVelocity;
            }
        }
        else if(Input.touchCount > 0 && !_gameManagerScript.IsGameOver && !_gameManagerScript.IsPaused)
        {
            for(int i = 0; i < Input.touchCount; ++i)
            {
                if(Input.GetTouch(i).phase == TouchPhase.Began && !_gameManagerScript.IsGameStarted)
                {
                    transform.position = Vector2.zero;
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    _gameManagerScript.IsGameStarted = true;
                    _rigidbody2D.velocity = Vector2.up * jumpVelocity;
                }
                else if(Input.GetTouch(i).phase == TouchPhase.Began && _gameManagerScript.IsGameStarted)
                {
                    _rigidbody2D.velocity = Vector2.up * jumpVelocity;
                }
            }
        }

        transform.eulerAngles = new Vector3(0, 0, _rigidbody2D.velocity.y * angleCoefficient);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _gameManagerScript.IsGameOver = true;
        _gameManagerScript.IsCleanupRequired = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*
        if(collision.CompareTag("Present"))
        {
            Debug.Log("You gost a present.");
            collision.gameObject.SetActive(false);
            ScoreScript.Score += 5;
        }*/
    }
}
