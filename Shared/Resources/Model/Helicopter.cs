using System;

using CoreGraphics;
using Foundation;
using SpriteKit;
using SpriteTest.Resources;

namespace SpriteTest.Resources.Model
{
    public class Helicopter
    {
        private
        double maxpitch = (3.14 / 3);
        String Name;
        String Callsign;
        int Pitch; //used for rotation
        int Heading; //used for rotation
        int Fuel;
        int Px; int Py; int Pz;  //position on screen
        int Heliwidth;
        int Heliheight;
        double PitchDouble;

        int x_world;
        int y_world;
        int dir;
        int MAXFUEL;
        int MAXLIFE;//used for medvac as the pilots may need to
                    //monitor patient status
        int MAXDAMAGE;
        byte MAXBULLET;

        public SByte vSpeed;
        public SByte hSpeed;
        byte MAXvSPEED;
        byte MAXhSPEED;
        byte altitude;
        byte isLanding;
        byte fire;
        byte fuel;
        byte fuelCheck;
        byte life;
        byte isCrashing;
       
        public int DAMAGE;
        public SKSpriteNode PlayerSpriteObject { get; set; }

        public Helicopter()
        {
            this.Callsign = "PwnNoobs";
            this.hSpeed = 0;
            this.hSpeed = 0;
            this.fuel = 254;
            this.Fuel = 254;
            this.altitude = 100;
            this.MAXLIFE = 254;//same as fuel
            this.Pitch = 0; //stationed flat;
            this.Heading = 0; //due North first
            this.MAXhSPEED = 100; //five levels, five different speeds
            this.MAXvSPEED = 100; //five levels, five different speeds
            this.MAXDAMAGE = 254 * 10; //helicopter can only sustain 10 critical
                                       //hits
            this.altitude = 100;
            this.isLanding = 0;
            this.fire = 0;
            this.fuel = 254;
            this.fuelCheck = 0;
            this.life = 5;
            this.isCrashing = 0;
           
        }

        public void SetPlayer(SKSpriteNode player) => this.PlayerSpriteObject = player;

        public void SetXPos(){
            this.x_world = (int) this.PlayerSpriteObject.Position.X;
        }

        public void SetYPos(){
            this.y_world = (int) this.PlayerSpriteObject.Position.Y;
        }

        public int getXPos(){
            SetXPos();
            return this.x_world;
        }

        public int getYPos(){
            SetYPos();
            return this.y_world;
        }

        public void MoveUp(){
            if (!(this.vSpeed == MAXvSPEED))
            {
                this.vSpeed += 3;
            }
            this.altitude += 3;
            this.y_world += 3;
            var action1 = SKAction.MoveBy(0, this.vSpeed, 1.0);
            this.PlayerSpriteObject.RunAction(action1);

            return;
        }

        public void MoveDown(){
            if (!(this.hSpeed == MAXvSPEED))
            {
                this.vSpeed -= 3;
            }
            this.altitude -= 3;
            this.y_world -= 3;
            var action1 = SKAction.MoveBy(0, this.vSpeed, 1.0);
            this.PlayerSpriteObject.RunAction(action1);

            return;
        }

        public void MoveLeft(){

            if (!(this.hSpeed == MAXhSPEED))
            {
                this.hSpeed -= 3;
            }
            this.x_world -= 3;
            this.Pitch += 5;
            var action1 = SKAction.MoveBy(this.hSpeed, 0, 1.0);
            this.PlayerSpriteObject.RunAction(action1);

            
        }

        public void MoveRight(){
            if(!(this.hSpeed == MAXhSPEED))
            {
                this.hSpeed += 3;
            }
            this.x_world += 3;
            this.Pitch -= 5;
            var action1 = SKAction.MoveBy(this.hSpeed, 0, 1.0);
            this.PlayerSpriteObject.RunAction(action1);
        }

        public void LeftPedal(){
            //1)UPDATE THE HEADING CCW
            this.Heading = (this.Heading + 5) % 360;
            var action1 = SKAction.RotateByAngle(this.Heading, 1.0);
            this.PlayerSpriteObject.RunAction(action1);
        }

        public void RightPedal(){
            //1)UPDATE THE HEADING CW
            this.Heading = (this.Heading - 5) % 180;//heading is in degrees
            //2)ROTATE BY THE ANGLE OF THE HEADING
            var action1 = SKAction.RotateByAngle(this.Heading, 1.0);
            //HAVE PLAYER MOVE
            this.PlayerSpriteObject.RunAction(action1);
        }

        public nfloat GetXposition(){
            //refresh the coordinates of the x_world
            this.x_world = (int) this.PlayerSpriteObject.Position.X;
            return this.PlayerSpriteObject.Position.X;
        }

        public nfloat GetYposition()
        {
            //refresh the coordinates of the y_world
            this.y_world = (int) this.PlayerSpriteObject.Position.Y;
            return this.PlayerSpriteObject.Position.Y;
        }

        public int GetFuel_integer(){
            //get the fuel info
            return this.Fuel;
        }

        public void SetFuel_integer(int Fuelsetter){
            //set the fuel infor for an integer
            this.Fuel = Fuelsetter;
            return;
        }

        public byte GetFuel_byte(){
            //set get the fuel info
            return this.fuel;
        }

        public void SetFuel_byte(byte fuelsetter){
            //set the fuel
            this.fuel = fuelsetter;
            return;
        }


        public byte GetLife(){
            return this.life;
        }

        public void Setlife(byte lifesetting){
            this.life = lifesetting;
            return;
        }

        public int GetHeading(){
            return this.Heading;
        }

        public byte GetDirection(){
            int hspeed = this.hSpeed;//x-direction
            int vspeed = this.vSpeed;//y-direction
            double angle = 0;
            byte direction;//this will return the quadrant (1-4)
            //determine which coordinates these are in
            if(hspeed == 0 && vspeed >0){
                this.Heading = 90;
                return (byte)this.dir;
            }

            if ((hspeed == 0) && (vspeed <0)){
                this.Heading = 270;
                return (byte)this.dir;
            }

            if ((hspeed == 0) && (vspeed == 0)){
                return (byte)this.dir;
            }
            angle = Math.Atan2(vspeed, hspeed);

           // int quadrant = (int) (angle / (Math.PI / 2));
            while(angle < 0){
                angle += (2 * Math.PI);
            }

            if (angle >= 0 && angle <= (Math.PI / 2))
            {
                //first quadrant
                direction = 1;
                this.Heading = (int)(angle * (180 / Math.PI));
            }

            else if (angle > (Math.PI / 2) && angle <= Math.PI){
                //second quadrant
                direction = 2;
                this.Heading = (int)(angle * (180/Math.PI));
            }

            else if(angle > Math.PI && angle <= ((3*Math.PI)/2)){
                //thrid quadrant
                direction = 3;
                this.Heading = (int)(angle * (180 / Math.PI));
            }

            else{
                //fourth quadrant
                direction = 4;
                this.Heading = (int)(angle * (180 / Math.PI));
            }


            return direction;
        }

        public void SetPlayerDirection(){
            byte direction = 0;
            direction = GetDirection();
            this.dir = direction;
            //all SIde E for now as the sprite pictures need to be redrawn
            switch (direction)
            {
                case 1:
                    this.PlayerSpriteObject.Texture = SKTexture.FromImageNamed(NSBundle.MainBundle.PathForResource("SideE", "png"));
                    break;
                case 2:
                    this.PlayerSpriteObject.Texture = SKTexture.FromImageNamed(NSBundle.MainBundle.PathForResource("SideW", "png"));
                    break;
                case 3:
                    this.PlayerSpriteObject.Texture = SKTexture.FromImageNamed(NSBundle.MainBundle.PathForResource("SideW", "png"));
                    break;
                case 4:
                    this.PlayerSpriteObject.Texture = SKTexture.FromImageNamed(NSBundle.MainBundle.PathForResource("SideE", "png"));
                    break;
            }
        }

        public void RotatePlayer_Heading(){
            //rotates by the heading
            int Heading1 = this.Heading;
            double radians = Heading1 * Math.PI / 180;
            var action1 = SKAction.RotateToAngle((float)radians, 1.0);
            //HAVE PLAYER MOVE rotation
            this.PlayerSpriteObject.RunAction(action1);

        }

        public void RotatePlayer_Pitch(){
            int pitch1 = this.Pitch;
            double radians = pitch1 * Math.PI / 180;
            var action1 = SKAction.RotateToAngle((float)radians, 1.0);
            //HAVE PLAYER MOVE TO ROTATED ANGLE
            this.PlayerSpriteObject.RunAction(action1);
        }

        public int GetPitch(){
            return this.Pitch;
        }



        //public void setFrame(int x, int y) => this.bkgrnd = new int[x][y];
    }
}
