using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//jme
public abstract class BossStates 
{
    protected Boss boss;

    public BossStates(Boss boss) 
    {
        this.boss = boss;
    }
    public abstract void DoActionUpdate(float dTime);
}

public class BossChase : BossStates
{
    public BossChase(Boss boss): base(boss)
    {

    }

    public override void DoActionUpdate(float dTime)
    {
        if (boss.canMove)
        {
            boss.MoveToPlayer();
        }
    }
}

public class BossCharge : BossStates
{
    public BossCharge(Boss boss) : base(boss)
    {

    }

    public override void DoActionUpdate(float dTime)
    {

    }
}

public class BossAttack : BossStates
{
    public BossAttack(Boss boss) : base(boss)
    {

    }

    public override void DoActionUpdate(float dTime)
    {

    }
}

public class BossInterrupt : BossStates
{
    public BossInterrupt(Boss boss) : base(boss)
    {

    }

    public override void DoActionUpdate(float dTime)
    {

    }
}