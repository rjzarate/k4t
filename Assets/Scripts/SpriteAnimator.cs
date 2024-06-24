using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpriteAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private void Awake()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTrigger(string trigger)
    {
        animator.SetTrigger(trigger);
    }

    public void SetInteger(string parameter, int integer)
    {
        animator.SetInteger(parameter, integer);
    }

    public void SetBool(string parameter, bool state)
    {
        animator.SetBool(parameter, state);
    }
}
