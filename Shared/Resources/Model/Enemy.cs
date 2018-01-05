using System;

using Foundation;
using SpriteKit;
namespace SpriteTest
{
    public class Enemy
    {

        public SKSpriteNode enemySprite;
        public int x_world;
        public int y_world;
        public byte life_meter;

        public Enemy()
        {
            this.x_world = 0;
            this.y_world = 0;
            this.life_meter = 127;
            
        }

        public Enemy(byte life_value){
            this.life_meter = life_value;
        }

        public int GetXposition()
        {
            this.x_world = (int)this.enemySprite.Position.X;
            return this.x_world;
        }

        public int GetYposition()
        {
            this.y_world = (int)this.enemySprite.Position.Y;
            return this.y_world;
        }

        public void TowerGotHit()
        {
            this.life_meter -= 5;
        }

        public bool isDead()
        {
            //returns true if dead and false if still alive and kickin

            if (this.life_meter <= 0)
            {
                return true;
            }

            return false;
        }
    }
}
