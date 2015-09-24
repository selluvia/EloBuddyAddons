using EloBuddy;
using EloBuddy.SDK;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Branddy
{
    class Damage
    {
        private static AIHeroClient source { get { return ObjectManager.Player; } }

        public static double Qdmg(Obj_AI_Base target)
        {
            return source.CalculateDamageOnUnit(target, DamageType.Magical,
                (float)(new double[] { 80, 120, 160, 200, 240 }[Configs.Q.Level - 1]
                        + 0.65 * source.FlatMagicDamageMod));
        }

        public static double Wdmg(Obj_AI_Base target)
        {
            return source.CalculateDamageOnUnit(target, DamageType.Magical,
                (float)(new double[] { 75, 120, 165, 210, 265 }[Configs.W.Level - 1]
                         + 0.6 * source.FlatMagicDamageMod));
        }

        public static double Edmg(Obj_AI_Base target)
        {
            return source.CalculateDamageOnUnit(target, DamageType.Physical,
                (float)(new double[] { 70, 105, 140, 175, 210 }[Configs.E.Level - 1]
                         + 0.55 * source.FlatMagicDamageMod));
        }

        public static double Rdmg(Obj_AI_Base target)
        {
            return source.CalculateDamageOnUnit(target, DamageType.Magical,
                (float)(new double[] { 150, 250, 350 }[Configs.R.Level - 1]
                        + 0.6 * source.FlatMagicDamageMod));

        }
    }
}
