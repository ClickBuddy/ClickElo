using EloBuddy;
using EloBuddy.SDK;
using Settings = ClickLux.Config.Modes.Harass;

namespace ClickLux.Modes
{
    public sealed class Harass : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass) || Settings.AutoHarass) && Settings.Mana;
        }

        public override void Execute()
        {
            if (Settings.UseQ)
            {
                var target = TargetSelector.GetTarget(Q.Range, DamageType.Magical);
                ModeManager.QLogic.Cast(target, Orbwalker.ActiveModes.Harass);
            }
            if (Settings.UseE)
            {
                var target = TargetSelector.GetTarget(E.Range, DamageType.Magical);
                ModeManager.ELogic.Cast(target, Orbwalker.ActiveModes.Harass);
            }
        }
    }
}
