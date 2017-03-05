using EloBuddy.SDK;

namespace ClickLux.Modes
{
    public abstract class ModeBase
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

        public abstract bool ShouldBeExecuted();

        public abstract void Execute();
    }
}
