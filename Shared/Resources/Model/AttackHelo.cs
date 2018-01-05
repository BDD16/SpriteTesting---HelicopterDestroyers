using System;

using SpriteTest.Resources.Model;

namespace SpriteTest
{
    public class AttackHelo : Helicopter
    {
        Bullets[] MissilePods;
        Bullets[] MachineGun;

        public AttackHelo()
        {
            this.MissilePods = new Bullets[30];
            this.MachineGun = new Bullets[50];
            //setting fule to be 254 seconds worth of gameplay
            //everytime the player destroys an enemy it will get a boost in fuel
            this.SetFuel_byte(254);
            //initialize the missiles
            for (int i = 0; i < this.MissilePods.Length; i += 1){
                this.MissilePods[i] = new Bullets();
            }

            //initialize the bullets (for now just have a place holder
            //until the image is made
            for (int i = 0; i < this.MachineGun.Length; i += 1){
                this.MachineGun[i] = new Bullets();
            }
        }

        public void HeatSeekerLaunch(Towers Target){
            //TODO: EVERYTHING OVERRIDE THE LAUNCH OF THE MISSILE TO BE A 
            //HEAT SEEKER OR THAT IT'S ACTS AS A SIDE WINDER, A SLOWER MISSILE,
            //BUT THAT CAN MOVE TOWARDS AN ENEMY GIVEN IT'S SURROUNDINGS

        }

        public void FireMissile()
        {
            int i = 0;
            while (this.MissilePods[i].isFired)
            {
                i += 1;
                if (i >= this.MissilePods.Length)
                {
                    return;
                }
            }
            this.MissilePods[i].FireBullet(this.GetPitch(), this.GetHeading(),
                                          this.PlayerSpriteObject.Position.X,
                                          this.PlayerSpriteObject.Position.Y);
            this.MissilePods[i].isFired = true;
        }



    }
}
