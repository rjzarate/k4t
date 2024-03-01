using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// interface for boss essentials
public interface IBoss
{
    void Idle();
    void Attack();
    void Damaged();
    void Death();
}

// interface for bullet essentials
public interface IBullet
{
    void FireBullet();
}