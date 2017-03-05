using EloBuddy;
using EloBuddy.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickLux.Spell
{
    class QLogic : LogicBase
    {
        /// <summary>
        /// Shoulds the cast.
        /// </summary>
        /// <returns></returns>
        public override bool ShouldCast()
        {
            return Q.IsReady();
        }

        /// <summary>
        /// Casts Q Logic.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="activeMode">The active mode.</param>
        public override void Cast(Obj_AI_Base target, Orbwalker.ActiveModes activeMode)
        {
            if (!ShouldCast() || !target.Valid(Q.Range)) return;

            switch (activeMode)
            {
                case Orbwalker.ActiveModes.Combo:
                    HitChanceCast(Q, target, 89);
                    break;
                case Orbwalker.ActiveModes.Harass:
                    HitChanceCast(Q, target);
                    break;
            }
        }

        public override void Cast(Orbwalker.ActiveModes activeMode)
        {
            if (!ShouldCast()) return;

            Obj_AI_Minion target;
            switch (activeMode)
            {
                case Orbwalker.ActiveModes.LaneClear:
                    target = EntityManager.MinionsAndMonsters.EnemyMinions.FirstOrDefault(m => !m.IsDead && m.IsValidTarget(Q.Range));
                    if (target != null)
                        HitChanceCast(Q, target);
                    break;
                case Orbwalker.ActiveModes.JungleClear:
                    target = EntityManager.MinionsAndMonsters.Monsters.FirstOrDefault(m => !m.IsDead && m.IsValidTarget(Q.Range));
                    if(target != null)
                        HitChanceCast(Q, target);
                    break;
            }
        }
    }
}
