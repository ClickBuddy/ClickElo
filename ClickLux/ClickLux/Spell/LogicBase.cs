using System.Collections.Generic;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using SharpDX;

namespace ClickLux.Spell
{
    public abstract class LogicBase
    {
        protected EloBuddy.SDK.Spell.Skillshot Q
        {
            get { return SpellManager.Q; }
        }
        protected EloBuddy.SDK.Spell.Skillshot W
        {
            get { return SpellManager.W; }
        }
        protected EloBuddy.SDK.Spell.Skillshot E
        {
            get { return SpellManager.E; }
        }
        protected EloBuddy.SDK.Spell.Skillshot R
        {
            get { return SpellManager.R; }
        }

        /// <summary>
        /// Casts the specified active mode.
        /// </summary>
        /// <param name="activeMode">The active mode.</param>
        public virtual void Cast(Orbwalker.ActiveModes activeMode) { }

        /// <summary>
        /// Casts the specified enemy.
        /// </summary>
        /// <param name="enemy">The enemy.</param>
        /// <param name="activeMode">The active mode.</param>
        public virtual void Cast(Obj_AI_Base enemy, Orbwalker.ActiveModes activeMode) { }

        /// <summary>
        /// Casts the specified position.
        /// </summary>
        /// <param name="pos">The position.</param>
        /// <param name="activeMode">The active mode.</param>
        public virtual void Cast(Vector3 pos, Orbwalker.ActiveModes activeMode) { }

        public virtual bool HitChanceCast(EloBuddy.SDK.Spell.Skillshot spell, Obj_AI_Base target, float chance = 85)
        {
            var pred = spell.GetPrediction(target);

            if (pred.HitChancePercent >= chance)
                if (spell.Cast(pred.CastPosition))
                    return true;

            return false;
        }

        public virtual Vector3 FarmCastCircular(EloBuddy.SDK.Spell.Skillshot spell, IEnumerable<Obj_AI_Minion> mobList, int minHit = 2)
        {
            var bestPos = Vector3.Zero;
            var countBestPos = 0;

            foreach (var aiBase in mobList)
            {
                if (countBestPos == 0)
                {
                    bestPos = aiBase.ServerPosition;
                    countBestPos =
                        EntityManager.MinionsAndMonsters.CombinedAttackable.Count(
                            t => t != null && t.IsValidTarget(spell.Radius));
                }
                else
                {
                    var newBestPos = EntityManager.MinionsAndMonsters.CombinedAttackable.Count(
                            t => t != null && t.IsValidTarget(spell.Radius));

                    if (newBestPos <= countBestPos) continue;

                    countBestPos = newBestPos;
                    bestPos = aiBase.ServerPosition;
                }
            }

            return countBestPos >= minHit ? bestPos : Vector3.Zero;
        }

        /// <summary>
        /// Shoulds the cast.
        /// </summary>
        /// <returns></returns>
        public virtual bool ShouldCast() { return false; }
    }
}
