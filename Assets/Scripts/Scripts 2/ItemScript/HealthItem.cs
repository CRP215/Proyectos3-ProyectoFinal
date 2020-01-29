using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : Item
{
    public int m_health;

    public override void EnterAction   (Player player)
    {
        base.EnterAction(player);

        m_player.GetLife(m_health);
    }

    public override void ExecuteAction ()
    {
        base.ExecuteAction();
    }

    public override void ExitAction    ()
    {
        base.ExitAction();
        Destroy(this.gameObject);
    }
}
