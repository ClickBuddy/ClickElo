using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;

namespace ClickLux
{
    public static class SpellManager
    {
        public static EloBuddy.SDK.Spell.Skillshot Q { get; private set; }
        public static EloBuddy.SDK.Spell.Skillshot W { get; private set; }
        public static EloBuddy.SDK.Spell.Skillshot E { get; private set; }
        public static EloBuddy.SDK.Spell.Skillshot R { get; private set; }

        static SpellManager()
        {
            // Initialize spells
            Q = new EloBuddy.SDK.Spell.Skillshot(SpellSlot.Q, 1175, SkillShotType.Linear, 250, 1200, 70) { AllowedCollisionCount = 1};
            W = new EloBuddy.SDK.Spell.Skillshot(SpellSlot.W, 1075, SkillShotType.Linear, 0, 1400, 85) { AllowedCollisionCount = int.MaxValue };
            E = new EloBuddy.SDK.Spell.Skillshot(SpellSlot.E, 1100, SkillShotType.Circular, 250, 1300, 275) { AllowedCollisionCount = int.MaxValue };
            R = new EloBuddy.SDK.Spell.Skillshot(SpellSlot.R, 3340, SkillShotType.Circular, 500, int.MaxValue, 250) { AllowedCollisionCount = int.MaxValue };
        }

        public static void Initialize()
        {
        }
    }
}
