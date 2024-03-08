using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    private float health;

    private void Start(){
        health = maxHealth;
    }

    public float GetHealth(){
        return health;
    }

    public void SetHealth(int newHealth){
        health = newHealth;
    }

    public void Damage(float damage)
    {
        health -= damage;
        if (health <= 0f)
        {
            Destroy(gameObject);
        }
    }

    public void Heal(float heal)
    {
        health += heal;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public bool death(){
        if(health <= 0){
            return false;
        }
        return true;
    }
}   
