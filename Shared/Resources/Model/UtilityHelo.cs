using System;

using SpriteTest.Resources.Model;
using Foundation;
using SpriteKit;

namespace SpriteTest
{
    public class UtilityHelo : Helicopter
    {
        public Bullets[] MachineGun;

        public UtilityHelo()
        {
            this.MachineGun = new Bullets[50];
            for (int i = 0; i < this.MachineGun.Length; i += 1){
                this.MachineGun[i] = new Bullets();
                this.MachineGun[i].Bullet.Texture = SKTexture.FromImageNamed(NSBundle.MainBundle.PathForResource("BulletImage_small", "png"));
            }
             
        }

        public int FireGun(){
            int i = 0;
            while( this.MachineGun[i].isFired){
                i += 1;
                if (i >= this.MachineGun.Length){
                    return -1;
                }
            }
            this.MachineGun[i].FireBullet(this.GetPitch(), this.GetHeading(), 
                                          this.PlayerSpriteObject.Position.X, 
                                          this.PlayerSpriteObject.Position.Y);
            this.MachineGun[i].isFired = true;

            return i;
        }


    }
}
