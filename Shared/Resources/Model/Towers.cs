using System;

using Foundation;
using SpriteKit;


namespace SpriteTest.Resources.Model
{
    public class Towers
    {
        public SKSpriteNode tower;
        int x_world;
        int y_world;
        byte lives;
        int numberofguards;
        public Bullets[] GuardMagazine;
        
        public Towers()
        {
            
            this.numberofguards = 10;
            this.lives = (byte)(this.numberofguards * 2);
            this.x_world = 0;
            this.y_world = 0;
            this.tower = new SKSpriteNode(
                SKTexture.FromImageNamed(NSBundle.MainBundle.PathForResource("TowerEnemies", "png")));
            this.GuardMagazine = new Bullets[8];
            for (int i = 0; i < this.GuardMagazine.Length; i += 1)
            {
                this.GuardMagazine[i] = new Bullets();
                this.GuardMagazine[i].Bullet.Name = "Tower_" + "Bullet_" + i.ToString();
            }
        }

        public Towers(int NumGuard){
            this.tower = new SKSpriteNode(
                SKTexture.FromImageNamed(NSBundle.MainBundle.PathForResource("TowerEnemies", "png")));
            this.x_world = (int)this.tower.Position.X;
            this.y_world = (int)this.tower.Position.Y;
            this.lives = (byte)(NumGuard * 2);
            this.GuardMagazine = new Bullets[NumGuard];
            for (int i = 0; i < this.GuardMagazine.Length; i += 1){
                this.GuardMagazine[i] = new Bullets();
            }

        }

        public int GetXposition(){
            this.x_world = (int)this.tower.Position.X;
            return this.x_world;
        }

        public int GetYposition(){
            this.y_world = (int)this.tower.Position.Y;
            return this.y_world;
        }

        public void FireAtRandom(){
            //UNFINISHED! NEED TO TEST AND DEBUG
            Random radians = (new Random((int)Math.PI));

            for (int i = 0; i < this.GuardMagazine.Length; i += 1)
            {
                this.GuardMagazine[i].FireBullet(radians.Next() % (int)(2* Math.PI), radians.Next() % (int)( Math.PI/2), this.GetXposition(), this.GetYposition());
            }
        }
        public void TowerGotHit(){
            this.lives -= 5;
        }

        public bool isDead(){
            //returns true if dead and false if still alive and kickin

            if(this.lives <= 0){
                return true;
            }

            return false;
        }
    }
}
