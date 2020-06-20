﻿using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEditor.UIElements;

class DamageSender : MonoBehaviour
{

    [SerializeField]
    private float RPM = 10;

    private float nextShoot;
    private float fireDelay;

    public GameObject bulletPrefab;

    private GameObject target;


    private void Awake()
    {
        fireDelay = 60.0f / RPM;

    }

    private void Start()
    {
        transform.GetComponent<TargetSearch>().TargetFoundEvent.AddListener(delegate (object target)
        {
            this.target = (GameObject)target;
        });
    }

    private void Update()
    {
        if(target!=null)
            PerformDamage();
    }


    public void PerformDamage()
    {
        if (nextShoot > Time.time) return;

        GameObject newBullet = Instantiate(bulletPrefab);
        var newDamageTransmitter = newBullet.GetComponent<DamageTransmitter>();
        newDamageTransmitter.Init((GameObject)target, transform.position);

        ResetAttackTimer();
    }

    private void ResetAttackTimer()
    {
        nextShoot = Time.time + fireDelay;
    }
}