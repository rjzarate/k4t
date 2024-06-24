using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteRandomFlip : MonoBehaviour
{
    // Will flip the sprite at the start of instantiation
    [SerializeField] private bool flipX = true;
    [SerializeField] private bool flipY = false;
    private SpriteRenderer spriteRenderer;
    
    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        System.Random random = new System.Random();
        bool willFlip;
        
        if (flipX) {
            willFlip = random.Next(0, 1) != 1;
            spriteRenderer.flipX = willFlip ? spriteRenderer.flipX : !spriteRenderer.flipX;
        }

        if (flipY) {
            willFlip = random.Next(0, 1) != 1;
            spriteRenderer.flipY = willFlip ? spriteRenderer.flipY : !spriteRenderer.flipY;
        }
    }
}
