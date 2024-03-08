using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float maxhealth;
    
    private void Start(){
        health = maxhealth;
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
        if (health > maxhealth)
        {
            health = maxhealth;
        }
    }

    public bool death(){
        if(health <= 0){
            return false;
        }
        return true;
    }
}   
