using EloBuddy;
using EloBuddy.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickLux
{
    static class Extensions
    {

        public static bool Valid(this Obj_AI_Base target, float range = 1000)
        {
            return target != null && target.IsValidTarget(range) && !target.IsInvulnerable;
        }

        public static float DamageOnUnit(this Obj_AI_Base target, DamageType damageType, float damage, float damageModifier, float damageIncrease = 0)
        {
            return (Player.Instance.CalculateDamageOnUnit(target, damageType, (damage + damageModifier * Player.Instance.FlatMagicDamageMod)) + damageIncrease);
        }
    }
}
