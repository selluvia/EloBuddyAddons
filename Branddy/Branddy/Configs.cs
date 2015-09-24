using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using SharpDX;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;

namespace Branddy
{
    class Configs
    {
        public static Spell.Skillshot Q;
        public static Spell.Skillshot W;
        public static Spell.Targeted E;
        public static Spell.Targeted R;

        public static AIHeroClient _Player
        {
            get { return ObjectManager.Player; }
        }

        public static void SpellInitiation()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 1000, SkillShotType.Linear, 0, 1500, 60);
            W = new Spell.Skillshot(SpellSlot.W, 900, SkillShotType.Linear, 1, 4000, 240);
            E = new Spell.Targeted(SpellSlot.E, 625);
            R = new Spell.Targeted(SpellSlot.R, 750);
        }

        public static void Game_OnTick(EventArgs args)
        {
            switch (Orbwalker.ActiveModesFlags)
            {
                case Orbwalker.ActiveModes.Combo:
                    Combo();
                    return;
                case Orbwalker.ActiveModes.Harass:
                    Harass();
                    return;
                case Orbwalker.ActiveModes.LaneClear:
                    LaneClear();
                    return;
            }
        }

        public static void Combo()
        {
            var target = TargetSelector.GetTarget(Q.Range, DamageType.Physical);
            if (target == null) return;

            if (Bramenu.ComboMenu["useW"].Cast<CheckBox>().CurrentValue &&
                W.IsReady())
            {
                var wpred = Prediction.Position.PredictCircularMissile(target, W.Range, 240, 1, 5000, target.Position);
                if (wpred.HitChance >= HitChance.High)
                {
                    W.Cast(wpred.CastPosition);
                }
            }

            if (Bramenu.ComboMenu["useQ"].Cast<CheckBox>().CurrentValue &&
                Q.IsReady())
            {
                var qpred = Prediction.Position.PredictLinearMissile(target, Q.Range, Q.Width, Q.CastDelay, Q.Speed, 0);
                if (qpred.HitChance >= HitChance.High)
                {
                    Q.Cast(qpred.CastPosition);
                }
            }
            
            if (Bramenu.ComboMenu["useE"].Cast<CheckBox>().CurrentValue && E.IsReady())
            {
                E.Cast(target);
            }
            if (Bramenu.ComboMenu["useR"].Cast<CheckBox>().CurrentValue && R.IsReady())
            {
                R.Cast(target);
            }
        }

        public static void Harass()
        {
            var target = TargetSelector.GetTarget(Q.Range, DamageType.Physical);
            if (target == null) return;
            if (Bramenu.HarassMenu["useWharass"].Cast<CheckBox>().CurrentValue &&
                W.IsReady() &&
                Bramenu.HarassMenu["manam"].Cast<Slider>().CurrentValue <= _Player.ManaPercent)
            {
                var wpred = Prediction.Position.PredictCircularMissile(target, W.Range, 240, 1, 5000, target.Position);
                if (wpred.HitChance >= HitChance.Medium)
                {
                    W.Cast(wpred.CastPosition);
                }
            }
            if (Bramenu.HarassMenu["useEharass"].Cast<CheckBox>().CurrentValue &&
                E.IsReady() &&
                Bramenu.HarassMenu["manam"].Cast<Slider>().CurrentValue <= _Player.ManaPercent)
            {
                E.Cast();
            }
        }

        public static void LaneClear()
        {
            foreach (
                var m in
                    ObjectManager.Get<Obj_AI_Minion>()
                        .Where(n => n.CountEnemiesInRange(1500) >= _Player.CountEnemiesInRange(1500) && n.IsEnemy))
            {
                if (!m.IsValidTarget(2500))
                    continue;
                if (m.Health <= _Player.GetAutoAttackDamage(m, true))
                {
                    W.Cast(m.Position);
                }

            }
        }
        // Auto Stun still not working - Need to identify Ablaze buff.
        public static void AutoStun()
        {
          
            if (Bramenu.ComboMenu["smartQ"].Cast<CheckBox>().CurrentValue)
            {
                foreach (
                    var target in
                        HeroManager.Enemies.Where(
                            tgt => tgt.IsValidTarget(Q.Range) && tgt.HasBuff("dot")))
                {
                    if (Q.GetPrediction(target).HitChance >= HitChance.High)
                        Q.Cast(target);
                }
            }
        }

       
    }
}
