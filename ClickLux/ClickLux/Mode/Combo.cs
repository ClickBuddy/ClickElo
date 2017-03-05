using System;
using EloBuddy;
using EloBuddy.SDK;
using Settings = ClickLux.Config.Modes.Combo;

namespace ClickLux.Modes
{
    public sealed class Combo : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo);
        }

        public override void Execute()
        {
            if (Settings.UseQ)
            {
                var target = TargetSelector.GetTarget(Q.Range, DamageType.Magical);

                ModeManager.QLogic.Cast(target, Orbwalker.ActiveModes.Combo);                
            }

            if (Settings.UseW)
            {
                ModeManager.WLogic.Cast(Orbwalker.ActiveModes.Combo);
            }

            if (Settings.UseE)
            {
                var target = TargetSelector.GetTarget(E.Range, DamageType.Magical);
                ModeManager.ELogic.Cast(target, Orbwalker.ActiveModes.Combo);
            }

            if (Settings.UseR)
            {
                var target = TargetSelector.GetTarget(R.Range, DamageType.Magical);
                ModeManager.RLogic.Cast(target ,Orbwalker.ActiveModes.Combo);
            }
        }
    }
}
