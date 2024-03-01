using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdle : BossAction
{
    public override void Action()
    {
        duration -= Time.deltaTime;
    }

    public override void BeginAction()
    {
        duration = time;
    }
}
