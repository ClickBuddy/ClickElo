using EloBuddy;
using EloBuddy.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickLux.Spell
{
    class RLogic : LogicBase
    {
        /// <summary>
        /// Shoulds the cast.
        /// </summary>
        /// <returns></returns>
        public override bool ShouldCast()
        {
            return R.IsReady();
        }

        /// <summary>
        /// Casts R Logic.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="activeMode">The active mode.</param>
        public override void Cast(Obj_AI_Base target, Orbwalker.ActiveModes activeMode)
        {
            if (!ShouldCast() || !target.Valid(R.Range)) return;

            if (Player.Instance.IsInAutoAttackRange(target) && Player.Instance.GetAutoAttackDamage(target, true) > target.Health && Orbwalker.CanAutoAttack)
            {
                return;
            }

            if (Q.IsReady() && Player.Instance.GetSpellDamage(target, SpellSlot.Q) > target.Health && Q.IsInRange(target))
            {
                return;
            }

            if (E.IsReady() && Player.Instance.GetSpellDamage(target, SpellSlot.E) > target.Health && E.IsInRange(target))
            {
                return;
            }

            switch (activeMode)
            {
                case Orbwalker.ActiveModes.Combo:
                    if (target.HasBuff("LuxIlluminatingFraulein"))
                    {
                        if (Killable(target, (10 + 8 * Player.Instance.Level)))
                        {
                            if (target.HasBuffOfType(BuffType.Snare))
                                R.Cast(target.Position);
                            else if (target.HasBuffOfType(BuffType.Slow))
                                HitChanceCast(R, target);
                            else
                                R.Cast(Prediction.Position.PredictUnitPosition(target, 500).To3D());
                        }
                    }
                    else
                    {
                        if (Killable(target))
                        {
                            if (target.IsRooted)
                                R.Cast(target.Position);
                            else if (target.HasBuffOfType(BuffType.Slow))
                                HitChanceCast(R, target);
                            else
                                R.Cast(Prediction.Position.PredictUnitPosition(target, 500).To3D());
                        }
                    }
                    break;
            }
        }

        public bool Killable(Obj_AI_Base target, float bonusDamage = 0)
        {
            return target.DamageOnUnit(DamageType.Magical, (new [] { 0, 300f, 400f, 500f})[R.Level] + bonusDamage, (new[] { 0, 0.75f, 0.75f, 0.75f })[R.Level]) > target.Health;
        }
    }
}
