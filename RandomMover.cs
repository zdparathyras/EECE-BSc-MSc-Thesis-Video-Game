using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

public class RandomMover : MonoBehaviour
{
    public Transform[] dots;
    private Rigidbody2D rb;
    private int i = 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2 direction = Vector2.zero;
        if (i == 0)
        {
            direction = Vector2.left;
        }
        else if (i == 1)
        {
            direction = Vector2.up;
        }
        else if (i == 2)
        {
            direction = Vector2.right;
        }
        else if (i == 3)
        {
            direction = Vector2.down;
        }
        
        rb.velocity = direction * 250f;
        Debug.Log(Vector2.Distance(transform.localPosition, dots[i].localPosition));
        if (Vector2.Distance(transform.localPosition, dots[i].localPosition) < 100f)
        {
            i++;
            if (i == 4)
            {
                i = 0;
            }
        }
    }
}