using System;
using Foundation;
using SpriteKit;
using SpriteTest.Resources.Model;

namespace SpriteTest
{
    public class Tanks : Enemy
    {
        Bullets[] Cannon;
        int heading;
        int pitch_cannon;
        int hSpeed;
        int vSpeed;
        int MAXhSPEED;
        int MAXvSPEED;
        
        public Tanks()
        {
            this.enemySprite = new SKSpriteNode(
                SKTexture.FromImageNamed(NSBundle.MainBundle.PathForResource("Tank", "png")));
            this.Cannon = new Bullets[12];

            this.MAXhSPEED = 25;
            this.MAXvSPEED = 25;
            this.pitch_cannon = 0;

        }

        public void FireAtRandom()
        {
            //UNFINISHED! NEED TO TEST AND DEBUG
            Random radians = (new Random((int)Math.PI));

            for (int i = 0; i < this.Cannon.Length; i += 1)
            {
                this.Cannon[i].FireBullet(radians.Next() % (int)(2 * Math.PI), this.heading, this.GetXposition(), this.GetYposition());
            }
        }

        public void MoveNorth()
        {
            if (!(this.vSpeed == MAXvSPEED))
            {
                this.vSpeed += 3;
            }

            var action1 = SKAction.MoveBy(0, this.vSpeed, 1.0);
            this.enemySprite.RunAction(action1);

            return;
        }

        public void MoveSouth()
        {
            if (!(this.hSpeed == MAXvSPEED))
            {
                this.vSpeed -= 3;
            }

            this.y_world -= 3;
            var action1 = SKAction.MoveBy(0, this.vSpeed, 1.0);
            this.enemySprite.RunAction(action1);

            return;
        }

        public void MoveWest()
        {

            if (!(this.hSpeed == MAXhSPEED))
            {
                this.hSpeed -= 3;
            }
            this.x_world -= 3;
            this.heading += 5;
            var action1 = SKAction.MoveBy(this.hSpeed, 0, 1.0);
            this.enemySprite.RunAction(action1);


        }

        public void MoveEast()
        {
            if (!(this.hSpeed == MAXhSPEED))
            {
                this.hSpeed += 3;
            }
            this.x_world += 3;
            this.heading = (int) Math.Atan2(vSpeed, hSpeed);
            var action1 = SKAction.MoveBy(this.hSpeed, 0, 1.0);
            this.enemySprite.RunAction(action1);
        }


        public void FireAtPlayer(nfloat x, nfloat y){
            int headingdegPlayer;
            int pitchdegPlayer = 1; //don't need to know the pitch of the player
                                    //only need to know general direction for tank
            double X = (double)x;
            double Y = (double)y;
            double pX = (double)this.enemySprite.Position.X;
            double pY = (double)this.enemySprite.Position.Y;
            //intx and inty are the position of the player
            //1)TODO:calculate the angle between the tank and the player
            headingdegPlayer = (int) Math.Atan2((pY - Y), (pX - X));
            this.pitch_cannon = headingdegPlayer;
            //2)TODO:rotate the tank to that player angle
            var rotate = SKAction.RotateToAngle((nfloat)(headingdegPlayer*Math.PI/180), 2.0);
            //4)TODO:shoot the missile at that angle
            this.Cannon[0].FireBullet(headingdegPlayer,pitchdegPlayer, (nfloat)x, (nfloat)y);

        }

    }
}
