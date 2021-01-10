using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 0.5f;

    [SerializeField]
    private Rigidbody2D rigidbody2D;

    [SerializeField]
    private Animator animator;

    Vector2 movement;

    void Start()
    {
        StartCoroutine(RandMovement());
    }

    void Update()
    {
        
    }

    IEnumerator RandMovement()
    {
        while (true)
        {
            movement = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            yield return new WaitForSeconds(Random.Range(2f, 4f));
            movement = Vector2.zero;
            yield return new WaitForSeconds(Random.Range(0, 1.5f));

        }
    }

    private void FixedUpdate()
    {
        rigidbody2D.MovePosition(rigidbody2D.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
