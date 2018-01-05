using System;
using SpriteTest.Resources.Model;

namespace SpriteTest
{
    public class Missiles : Bullets
    {
        public Missiles()
        {
            this.damageCaused = 50;
            this.isFired = false;
        }
    }
}
