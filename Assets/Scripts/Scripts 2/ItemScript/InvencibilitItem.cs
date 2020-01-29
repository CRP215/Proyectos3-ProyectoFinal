using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvencibilitItem : Item
{
    public override void EnterAction   (Player player)
    {
        base.EnterAction(player);

        m_player.SetInvencibility(true);
    }

    public override void ExecuteAction ()
    {
        base.ExecuteAction();
    }

    public override void ExitAction    ()
    {
        base.ExitAction();
        m_player.SetInvencibility(false);
        Destroy(this.gameObject);
    }
}
