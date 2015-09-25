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
        public static SpellSlot Ignite;

        public static AIHeroClient _Player
        {
            get { return ObjectManager.Player; }
        }


        #region Spells

        public static void SpellInitiation()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 1000, SkillShotType.Linear, 0, 1500, 60);
            W = new Spell.Skillshot(SpellSlot.W, 900, SkillShotType.Linear, 1, 4000, 240);
            E = new Spell.Targeted(SpellSlot.E, 625);
            R = new Spell.Targeted(SpellSlot.R, 750);
            Ignite = Player.Spells.FirstOrDefault(sp => sp.SData.Name.ToLower().Contains("summonerdot")).Slot;
        }

        #endregion

        #region OnTick

        public static void Game_OnTick(EventArgs args)
        {
            KillSteal();
            AutoStun();
            switch (Orbwalker.ActiveModesFlags)
            {
                case Orbwalker.ActiveModes.Combo:
                    Combo();
                    KillSteal();
                    return;
                case Orbwalker.ActiveModes.Harass:
                    Harass();
                    KillSteal();
                    return;
                case Orbwalker.ActiveModes.LaneClear:
                    LaneClear();
                    return;
            }
        }

        #endregion

        #region Combo

        public static void Combo()
        {
            var target = TargetSelector.GetTarget(Q.Range, DamageType.Magical);
            if (target == null) return;

            AutoStun();

            if (Bramenu.ComboMenu["useW"].Cast<CheckBox>().CurrentValue &&
                W.IsReady())
            {
                var wpred = Prediction.Position.PredictCircularMissile(target, W.Range, 240, 1, 5000, target.Position);
                if (wpred.HitChance >= HitChance.High)
                {
                    W.Cast(wpred.CastPosition);
                }
            }

            if (Bramenu.ComboMenu["useE"].Cast<CheckBox>().CurrentValue && E.IsReady())
            {
                E.Cast(target);
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

            

            if (Bramenu.ComboMenu["useR"].Cast<CheckBox>().CurrentValue 
                && R.IsReady() && target.Health <= Damage.Rdmg(target))
            {
                R.Cast(target);
            }
        }

        #endregion

        #region Harass

        public static void Harass()
        {
            var target = TargetSelector.GetTarget(Q.Range, DamageType.Magical);
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

        #endregion

        #region LaneClear

        public static void LaneClear()
        {
        }

        #endregion

        //Stun still not working - Need to identify Ablaze buff.
        #region Auto Stun

        public static void AutoStun()
        {
          
            if (Bramenu.ComboMenu["smartQ"].Cast<CheckBox>().CurrentValue)
            {
                foreach (
                    var target in
                        HeroManager.Enemies.Where(
                            tgt => tgt.IsValidTarget(Q.Range) && tgt.HasBuff("summonerdot")))
                {
                    if (Q.GetPrediction(target).HitChance >= HitChance.High)
                        Q.Cast(target);
                }
            }
        }

        #endregion

        #region Kill Steal

        public static void KillSteal()
        {
            var target = TargetSelector.GetTarget(Q.Range, DamageType.Magical);
            foreach (AIHeroClient champ in HeroManager.Enemies)
            {
                if (target.IsValidTarget(E.Range) && target.HealthPercent <= 40)
                {
                    if (Damage.Qdmg(target) + Damage.Wdmg(target) + Damage.Edmg(target) >= champ.Health)
                    {
                        if (Bramenu.KS["useigniteks"].Cast<CheckBox>().CurrentValue && _Player.GetSummonerSpellDamage(target, DamageLibrary.SummonerSpells.Ignite) >= champ.Health)
                        {
                            Player.CastSpell(Ignite, champ);
                        }
                        if (Bramenu.KS["useqks"].Cast<CheckBox>().CurrentValue && Q.IsInRange(target) && Q.IsReady())
                        {
                            var qpred = Prediction.Position.PredictLinearMissile(target, Q.Range, Q.Width, Q.CastDelay, Q.Speed, 0);
                            if (qpred.HitChance >= HitChance.Medium)
                            {
                                Q.Cast(qpred.CastPosition);
                            }
                        }
                        if (Bramenu.KS["usewks"].Cast<CheckBox>().CurrentValue && W.IsInRange(target) && W.IsReady())
                        {
                            var wpred = Prediction.Position.PredictCircularMissile(target, W.Range, 240, 1, 5000, target.Position);
                            if (wpred.HitChance >= HitChance.Medium)
                            {
                                W.Cast(wpred.CastPosition);
                            } 
                        }
                        if (Bramenu.KS["userks"].Cast<CheckBox>().CurrentValue && E.IsInRange(target) && E.IsReady())
                        { 
                            R.Cast(target); 
                        }
                    }
                }
            }
        }

        #endregion

        // End of Configs
    }
}
