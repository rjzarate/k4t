using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int hp;
    [SerializeField] private int maxHp;
    [SerializeField] private int damage;
    
    private void Start(){
        hp = maxHp;
    }

    public int GetHealth(){
        return hp;
    }

    public void SetHealth(int newHealth){
        hp = newHealth;
    }

    public bool death(){
        if(hp <= 0){
            return false;
        }
        return true;
    }
}   
