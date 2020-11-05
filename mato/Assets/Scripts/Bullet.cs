﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 shootDir;

    public void Setup(Vector3 shootDir)
    {
        this.shootDir = shootDir;
        Destroy(gameObject, 5f);
    }

    private void Update()
    {
        float moveSpeed = 100f;
        transform.position += shootDir * moveSpeed * Time.deltaTime;
    }
}
