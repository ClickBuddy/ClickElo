using EloBuddy.SDK;

using Settings = ClickLux.Config.Modes.LaneClear;
namespace ClickLux.Modes
{
    public sealed class LaneClear : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear) && Settings.Mana;
        }

        public override void Execute()
        {
            if (Settings.UseE)
            {
                ModeManager.ELogic.Cast(Orbwalker.ActiveModes.LaneClear);
            }
            if (Settings.UseW)
            {
                ModeManager.WLogic.Cast(Orbwalker.ActiveModes.LaneClear);
            }

            if (Settings.UseQ)
            {
                ModeManager.QLogic.Cast(Orbwalker.ActiveModes.LaneClear);
            }
        }
    }
}
