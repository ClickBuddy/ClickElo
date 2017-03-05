using System.Linq;
using EloBuddy.SDK;
using Settings = ClickLux.Config.Modes.Misc;
namespace ClickLux.Modes
{
    public sealed class PermaActive : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return true;
        }

        public override void Execute()
        {
            if (Settings.AutoQImmobile)
            {
                var nearEnemies = EntityManager.Heroes.Enemies.Where(e => e != null && e.IsValidTarget(Q.Range));

                if (nearEnemies.Where(aiHeroClient => !aiHeroClient.CanMove).Any(aiHeroClient => ModeManager.QLogic.HitChanceCast(Q, aiHeroClient)))
                {
                    return;
                }
            }
        }
    }
}
