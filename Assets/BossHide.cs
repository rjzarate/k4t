using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHide : BossAction
{
    [SerializeField] bool isHiding = false;
    float transparency = 1f;
    [SerializeField] float transparentColor;

    [SerializeField] float transitionSpeed;
    [SerializeField] SpriteRenderer[] bossColors;
    [SerializeField] GameObject targetColorObj;

    [SerializeField] Transform playerPos;
    [SerializeField] float dodgeSpeed;
    [SerializeField] float dodgeDistance;
    [SerializeField] bool isBounded;
    [SerializeField] float checkBoundRadius;
    [SerializeField] LayerMask boundLayer;

    float hideDuration;


    public enum gizmosColors { red, yellow, pink, cyan, blue }

    [Header("Gizmos:")]
    [SerializeField] gizmosColors checkColor;

    // stop hide, this will be called when player interrupts the Boss.
    public void stopHide()
    {
        isHiding = false;
        hideDuration = 0;
    }

    // unique action for hide script
    public override void Action()
    {
        hideMode();
        dodgePlayer();
        if (hideDuration >= 0)
        {
            hideDuration -= Time.deltaTime;
        }
    }

    // constantly aware of dodging player while invisible
    void dodgePlayer()
    {
        isBounded = Physics2D.OverlapCircle(transform.position, checkBoundRadius, boundLayer);
        if (isHiding && GameObject.FindGameObjectWithTag("Player"))
        {
            // dodge the player when they get too close
            if (Mathf.Abs(playerPos.position.x - transform.position.x) <= dodgeDistance)
            {
                // go the other way if forced to a bounded corner
                if (isBounded)
                {
                    transform.Translate(Vector2.right * -Mathf.Sign(transform.position.x) * dodgeSpeed * Time.deltaTime);
                }

                // dodge player normally
                else
                {
                    transform.Translate(Vector2.right * Mathf.Sign(transform.position.x - playerPos.position.x) * Time.deltaTime);
                }
            }
        }
    }

    // invisible
    void hideMode()
    {
        // boss will no longer be hiding once hide duration is up
        if (hideDuration <= 0)
        {
            isHiding = false;
        }

        // boss hiding
        if (isHiding)
        {
            if (transparency > transparentColor)
            {
                transparency -= Time.deltaTime * (transitionSpeed);
            }
        }

        // boss no longer hiding
        else {
            if (transparency < 1f)
            {
                transparency += Time.deltaTime * (transitionSpeed);
            }

            else {
                duration = hideDuration;
            }
        }

        // during the transitioning of color, change color transparency
        if (isHiding && transparency > 0 || !isHiding && transparency < 1f)
        {
            foreach (SpriteRenderer bossColor in bossColors)
            {
                Color inVis = bossColor.color;
                inVis.a = transparency;
                bossColor.color = inVis;
            }
        }
    }

    // start of this script
    public override void BeginAction()
    {
        duration = time;
        hideDuration = duration;

        isHiding = true;
        transparency = 1f;

        if (transitionSpeed <= 0)
        {
            transitionSpeed = 1f;
        }

        // get available colors
        bossColors = new SpriteRenderer[GetComponentsInChildren<SpriteRenderer>().Length];

        if (targetColorObj)
        {
            bossColors = targetColorObj.GetComponentsInChildren<SpriteRenderer>();
        }

        else {
            bossColors = GetComponentsInChildren<SpriteRenderer>();
        }

        if (GameObject.FindGameObjectWithTag("Player"))
        {
            playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    // gizmos for wire sphere
    private void OnDrawGizmos()
    {
        if (!showGizmos)
        {
            return;
        }

        switch (checkColor)
        {
            case gizmosColors.red:
                Gizmos.color = Color.red;
                break;
            case gizmosColors.yellow:
                Gizmos.color = Color.yellow;
                break;
            case gizmosColors.pink:
                Gizmos.color = Color.magenta;
                break;
            case gizmosColors.cyan:
                Gizmos.color = Color.cyan;
                break;
            case gizmosColors.blue:
                Gizmos.color = Color.blue;
                break;
        }

        Gizmos.DrawWireSphere(transform.position, checkBoundRadius);
    }
}