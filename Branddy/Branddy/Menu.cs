using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using SharpDX;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace Branddy
{
    internal class Bramenu
    {
        public static Menu Brand, ComboMenu, HarassMenu, Draw;

        public static void StartMenu()
        {
            Brand = MainMenu.AddMenu("Branddy", "branddy");
            Brand.AddLabel("Brand add-on by selluvia");
            Brand.AddLabel("Version 0.1 Beta");

            ComboMenu = Brand.AddSubMenu("Combo Settings", "combo");
            ComboMenu.Add("useQ", new CheckBox("Use Q"));
            ComboMenu.Add("useW", new CheckBox("Use W"));
            ComboMenu.Add("useE", new CheckBox("Use E"));
            ComboMenu.Add("useR", new CheckBox("Use R"));
            ComboMenu.AddSeparator();
            ComboMenu.Add("smartQ", new CheckBox("Use Smart Q"));

            HarassMenu = Brand.AddSubMenu("Harass Settings", "harass");
            HarassMenu.Add("UseWharass", new CheckBox("Use W"));
            HarassMenu.Add("UseEharass", new CheckBox("Use E"));
            HarassMenu.Add("manam", new Slider("Min. mana to harass", 40, 0, 100));
                        
        }
    }
}
