using System;

using Foundation;
using SpriteKit;

namespace SpriteTest.Resources.Model
{
    public class Bullets
    {
        public SKSpriteNode Bullet;
        int x_world;
        int y_world;
        public bool isFired;
        public int damageCaused;

        public Bullets()
        {
            this.x_world = 0;
            this.y_world = 0;
            this.Bullet = new SKSpriteNode(
                SKTexture.FromImageNamed(NSBundle.MainBundle.PathForResource("bullet", "png")));
            this.isFired = false;
            this.damageCaused = 5;
        }


        public void FireBullet(int pitch, int heading, nfloat startingx, nfloat startingy){
            this.isFired = true;
            //logic for bullet
            //need a starting point
            this.x_world = (int)startingx;
            this.y_world = (int)startingy;
            int signx = 0;
            int signy = 0;
            int dir = 0;
            int headingcpy = heading;
            this.Bullet.Position = new CoreGraphics.CGPoint(startingx, 
                                                            startingy);
            double pitchradians = (pitch * (Math.PI / 180)) - (Math.PI / 2);
            double heaindingradians = heading * (Math.PI / 180);
           
            /*while(pitchradians < 0){
                pitchradians += (2 * Math.PI);
            } */

            while(headingcpy < 0){
                headingcpy += 360;
            }

            if ((headingcpy >= 0) && (headingcpy <= 90))
            {
                dir = 1;
                signx = 1;
                signy = 1;
            }

            else if ((headingcpy > 90) && (headingcpy < 180))
            {
                dir = 2;
                signx = -1;
                signy = 1;
            }

            else if ((headingcpy >= 180) && (headingcpy < 270)){
                dir = 3;
                signx = -1;
                signy = -1;
            }

            else if ((headingcpy > 270) && (headingcpy <= 360))
            {
                dir = 4;
                signx = 1;
                signy = -1;
            } 

            var action = SKAction.MoveBy((float)(signx * 5000 * Math.Cos((pitch*Math.PI/180))), 
                                         (float)(signy * 5000 * Math.Sin((pitch*Math.PI/180))), 5.0);
            if ((dir == 1) || (dir == 2))
            {
                var action1 = SKAction.RotateToAngle((nfloat)pitchradians, .25);

                this.Bullet.RunAction(action1);//rotate the missile first
                this.Bullet.RunAction(action);//launch the missile!
            }

            else if((dir == 3) || (dir == 4))
            {
                var action1 = SKAction.RotateToAngle((nfloat)(pitchradians + Math.PI), .25);

                this.Bullet.RunAction(action1);//rotate the missile first
                this.Bullet.RunAction(action);//launch the missile!
            }

        }

        public int getXposition(){
            this.x_world = (int)Bullet.Position.X;
            return this.x_world;
        }

        public int getYposition(){
            this.y_world = (int)Bullet.Position.Y;
            return this.y_world;
        }

        public void setXposition(int x){
            this.x_world = x;
            this.Bullet.Position = new CoreGraphics.CGPoint((float)x, (float)this.y_world);
        }

        public void setYposition(int y){
            this.y_world = y;
            this.Bullet.Position = new CoreGraphics.CGPoint((float)this.x_world, (float)y);
        }

        public int[] BulletCollided(Bullets[] PlayerMagazine, Towers[] EnemyTowers){
            int[] result = new int[2];

            result[0] = 0;//this will hold the player bullet collision number
            result[1] = 0;//this will hold the tower number that was hit       
            for (int i = 0; i < PlayerMagazine.Length; i += 1)
            {
                if (PlayerMagazine[i].isFired)
                {
                    //need to detect collision between Players bullets and towers

                    if (PlayerMagazine[i].getYposition() != 0 && PlayerMagazine[i].getXposition() != 0)
                    {


                        for (int j = 0; j < EnemyTowers.Length; j += 1)
                        {
                            if ((EnemyTowers[j].GetXposition() / 25) == (PlayerMagazine[i].getXposition() / 25))
                            {
                                if ((EnemyTowers[j].GetYposition() / 31) == (PlayerMagazine[i].getYposition() / 31))
                                {
                                    //we have detected a collision with a 25 pixel buffer
                                    //remove the missle once it's been hit and set for animation
                                    // PlayerMagazine[i].isFired = false;
                                    //PlayerMagazine[i].Bullet.Speed = 0;
                                    PlayerMagazine[i].isFired = false;//RELOAD!
                                    PlayerMagazine[i].Bullet.RemoveFromParent();
                                    result[0] = i;
                                    result[1] = j;
                                    return result;

                                }
                            }
                        }
                    }
                }
            }
            result[0] = -1;//indicates no one was hit
            result[1] = -1;// indicates no one was hit
            return result;
        }


        public bool BulletCollided(Bullets[] PlayerMagazine, Towers EnemyTowers)
        {
            for (int i = 0; i < PlayerMagazine.Length; i += 1)
            {
                if (PlayerMagazine[i].isFired)
                {
                    //need to detect collision between Players bullets and towers

                    if (PlayerMagazine[i].getYposition() != 0 && PlayerMagazine[i].getXposition() != 0)
                    {

                           if ((EnemyTowers.GetXposition() / 25) == (PlayerMagazine[i].getXposition() / 25))
                            {
                                if ((EnemyTowers.GetYposition() / 31) == (PlayerMagazine[i].getYposition() / 31))
                                {
                                    //we have detected a collision with a 25 pixel buffer
                                    //remove the missle once it's been hit and set for animation
                                    // PlayerMagazine[i].isFired = false;
                                    //PlayerMagazine[i].Bullet.Speed = 0;
                                    PlayerMagazine[i].isFired = false;//RELOAD!
                                    PlayerMagazine[i].Bullet.RemoveFromParent();
                                    
                                    return true;

                                }
                            }
                    }
                }
            }
            return false;
        }
    }
}
