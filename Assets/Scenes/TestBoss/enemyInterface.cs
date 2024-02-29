using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// interface for boss essentials
public interface IBoss
{
    void idle();
    void attack();
    void damaged();
    void death();
}

// interface for bullet essentials
public interface IBullet
{
    void fireBullet();
}

// interace for health essentials
public interface IHealth
{
    void takeDamage();
    void heal();
}