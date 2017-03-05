using EloBuddy;
using EloBuddy.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickLux.Spell
{
    class ELogic : LogicBase
    {
        private bool _spellCasted = false;

        /// <summary>
        /// Shoulds the cast.
        /// </summary>
        /// <returns></returns>
        public override bool ShouldCast()
        {
            return E.IsReady();
        }

        /// <summary>
        /// Casts E Logic.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="activeMode">The active mode.</param>
        public override void Cast(Obj_AI_Base target, Orbwalker.ActiveModes activeMode)
        {
            if (!ShouldCast())
            {
                _spellCasted = false;
                return;
            }

            if (_spellCasted)
            {
                if (HitChanceCast(E, target, 70)) _spellCasted = false;
            }
            else if (!target.Valid(E.Range))
            {
                return;
            }

            switch (activeMode)
            {
                case Orbwalker.ActiveModes.Combo:
                    if (HitChanceCast(E, target)) _spellCasted = true;
                    break;
                case Orbwalker.ActiveModes.Harass:
                    if (HitChanceCast(E, target)) _spellCasted = true;
                    break;
            }
        }

        public override void Cast(Orbwalker.ActiveModes activeMode)
        {
            if (!ShouldCast())
            {
                _spellCasted = false;
                return;
            }

            if (_spellCasted)
            {
                if(E.Cast(Player.Instance.Position))
                        _spellCasted = false;
            }

            switch (activeMode)
            {
                case Orbwalker.ActiveModes.JungleClear:
                    if (E.Cast(FarmCastCircular(E, EntityManager.MinionsAndMonsters.Monsters.Where(m => m.IsValidTarget(E.Range))))) _spellCasted = true;
                    break;
                case Orbwalker.ActiveModes.LaneClear:
                    if (E.Cast(FarmCastCircular(E, EntityManager.MinionsAndMonsters.EnemyMinions.Where(m => m.IsValidTarget(E.Range))))) _spellCasted = true;
                    break;
            }
        }
    }
}
