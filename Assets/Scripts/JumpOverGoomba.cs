using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JumpOverGoomba : MonoBehaviour
{
    public Transform enemyLocation;
    public TextMeshProUGUI scoreText;
    private bool onGroundState;
    public GameManager GameManager;

    [System.NonSerialized]
    public int score = 0;

    private bool countScoreState = false;
    public Vector3 boxSize;
    public float maxDistance;
    public LayerMask layerMask;

    void FixedUpdate()
    {
        if (Input.GetKeyDown("space") && onGroundCheck())
        {
            onGroundState = false;
            countScoreState = true;
        }

        if (!onGroundState && countScoreState)
        {
            if (Mathf.Abs(transform.position.x - enemyLocation.position.x) < 0.5f)
            {
                countScoreState = false;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground")) onGroundState = true;
    }


    private bool onGroundCheck()
    {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, maxDistance, layerMask))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
