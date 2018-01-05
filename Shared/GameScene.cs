using System;

using CoreGraphics;
using Foundation;
using SpriteKit;
using SpriteTest;
using SpriteTest.Resources.Model;
#if __IOS__
using UIKit;
#else
using AppKit;
#endif



namespace SpriteKitGame
{
    
    public class GameScene : SKScene
    {
        //Global Variables
        short game;
        int FrameNumber;
        int score = 0;
        //PLAYER
        UtilityHelo PlayerRide = new UtilityHelo(); //utility helo is most primitive helo
        Bullets[] PlayerMagazine = new Bullets[20];
        //ENEMIES
        Towers[] EnemyTowers = new Towers[10];
        Tanks[] EnemyTanks = new Tanks[5];

        protected GameScene(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void DidMoveToView(SKView view)
        {
            // Setup your scene here
            var myLabel = new SKLabelNode("Chalkduster")
            {
                Text = "Helicopter Destroyers \n**COMING SOON**!",
                FontSize = 45,
                Position = new CGPoint(Frame.Width / 2, Frame.Height / 2)
            };

            var myLabel1 = new SKLabelNode("Black")
            {
                Text = "BDD copyright 2017",
                FontSize = 35,
                Position = new CGPoint(Frame.Width / 2, Frame.Height / 3),
                Name = "myLabel1"

            };

            var ScoreLabel = new SKLabelNode("Comicsans")
            {
                Text = "Score: ",
                FontSize = 42,
                Position = new CGPoint(Frame.Width - 100, 100),
                Name = "ScoreLabel"
            };

            var NavArrow = SKSpriteNode.FromImageNamed(NSBundle.MainBundle.PathForResource("NavArrow", "png"));

            var Compass = SKSpriteNode.FromImageNamed(NSBundle.MainBundle.PathForResource("compassgauge_small", "png"));
            var Image1 = SKSpriteNode.FromImageNamed(NSBundle.MainBundle.PathForResource("SideS", "png"));

            Image1.Position = new CGPoint(Frame.Width / 2, Frame.Height / 4);
            Compass.Position = new CGPoint(Frame.Width - 50, 50);
            Compass.Name = "Compass";
            NavArrow.Position = new CGPoint(Frame.Width - 50, 50);
            NavArrow.Name = "NavArrow";
            NavArrow.SetScale((nfloat)1.5);


            //INITIALIZE PLAYER BULLET MAGAZINE
            for (int j = 0; j < PlayerMagazine.Length; j += 1)
            {
                PlayerMagazine[j] = new Bullets();
            }

            //INITIALIZE TOWER ARRAY
            for (int i = 0; i < EnemyTowers.Length; i += 1){
                EnemyTowers[i] = new Towers(5);//towers with 5 guards
                //for now the number of guards means 2 bullets per guard
            }

            for (int i = 0; i < EnemyTanks.Length; i += 1){
                EnemyTanks[i] = new Tanks();
            }

            //PLACE TOWERS RANDOMLY THROUGHOUT THE FRAME
            Random randx = new Random((int)Frame.Width);
            Random randy = new Random((int)Frame.Height);

            for (int i = 0; i < EnemyTowers.Length; i += 1)
            {
                nfloat randomx = (nfloat)randx.Next() % Frame.Width;
                nfloat randomy = (nfloat)randy.Next() %  Frame.Height;

                EnemyTowers[i].tower.Position = new CGPoint(randomx, randomy);
                EnemyTowers[i].tower.Name = "Tower_" + i.ToString();


            }

            for (int i = 0; i < EnemyTanks.Length; i += 1){
                nfloat randomx = (nfloat)randx.Next() % Frame.Width;
                nfloat randomy = (nfloat)randy.Next() % Frame.Height;

                EnemyTanks[i].enemySprite.Position = new CGPoint(randomx, randomy);
                EnemyTanks[i].enemySprite.Name = "EnemyTank_" + i.ToString();

            }
            //ADD THE TOWERS TO THE GAMESCENE (PARENT)
            for (int i = 0; i < EnemyTowers.Length; i += 1)
            {
                AddChild(EnemyTowers[i].tower);
            }

            //ADD THE TANKS TO THE GAMESENE (PARENT)
            for (int i = 0; i < EnemyTanks.Length; i += 1){
                AddChild(EnemyTanks[i].enemySprite);
            }
            //ADD THE LABELS TO THE GAMESCENE (PARENT)
            AddChild(myLabel);
            AddChild(myLabel1);
            AddChild(ScoreLabel);
            //ADD THE GAUGES TO THE GAMESCENE
            AddChild(Compass);
            AddChild(NavArrow);
            //ESTABLISH AND RUN AN ACTION FOR myLabel and ScoreLabel
            var fadeout1 = SKAction.FadeOutWithDuration(5);
            var fadeout2 = SKAction.FadeOutWithDuration(5);
           
            myLabel.RunAction(fadeout2);
            myLabel1.RunAction(fadeout1);


         }

#if __IOS__
		public override void TouchesBegan (NSSet touches, UIEvent evt)
		{
			// Called when a touch begins
		/*	foreach (var touch in touches) {
				var location = ((UITouch)touch).LocationInNode (this);

				var sprite = new SKSpriteNode ("Spaceship") {
					Position = location,
					XScale = 0.5f,
					YScale = 0.5f
				};

				var action = SKAction.RotateByAngle (NMath.PI, 1.0);

				sprite.RunAction (SKAction.RepeatActionForever (action));

				AddChild (sprite);
			} */
		}

        #else   //**************************************************************
        //*****MouseDown(NsEvent theEvent){}************************************
        //*****is an event that takes places the player on the board or screen**
        public override void MouseDown(NSEvent theEvent)
        {
            // Called when a mouse click occurs

            if (game == 0x000F)
            {
                return;
            }
            var location = theEvent.LocationInNode(this);
            //initiliazint the Sprite object (SKSSpriteNode is the type)
            var sprite = SKSpriteNode.FromImageNamed(NSBundle.MainBundle.PathForResource("SideNW", "png"));
            sprite.Name = "BlakePlayer";//naming the sprite node for lookup
            sprite.Position = location;//
            sprite.SetScale(0.25f);//setting the scale of the helo
            PlayerRide.SetPlayer(sprite);//placing the sprite into the helo object

            AddChild(sprite);//adding a child places sprite object onto the screen
            game = 0x000F;

        }


        //**********************************************************************
        //*****KeyDown(NSEvent the Event){}*************************************
        //*****is an event that takes user inputs based of aswd controls********
        //*****if a player hits an s it will start throttling to the left(W)****
        //*****and so forth with the rest of the directionaliteis for N,S,E,W***
        //*****when the player hits the space bar " ", the helo will use********
        //*****it's weapon depending on if it's an attack or utility version****
        public override void KeyDown(NSEvent theEvent){
            //Instructions for the Game Controller (move the Player)
            string character = theEvent.Characters;
            var Player = this.GetChildNode("BlakePlayer");
            double radians = PlayerRide.GetHeading() * Math.PI / 180; 

            PlayerRide.SetPlayer((SKSpriteNode)Player);
            switch(character[0]){
                case 'w'://HIT THE THROTTLE
                    PlayerRide.MoveUp();//need to continue to debug
                    PlayerRide.SetPlayerDirection();

                    PlayerRide.RotatePlayer_Pitch();
                    break;
                case 's': //STOP ON THE THROTTLE
                    PlayerRide.MoveDown();//need to continue to debug
                    PlayerRide.SetPlayerDirection();

                    PlayerRide.RotatePlayer_Pitch();//need to continue to debug
                    break;
                case 'a'://CYCLIC TO THE LEFT GET OUT OF HERE (OR IS IT PULL UP)
                    PlayerRide.MoveLeft();
                    PlayerRide.SetPlayerDirection();

                    PlayerRide.RotatePlayer_Pitch();
                    break;

                case'd'://CYCLIC TO THE RIGHT GET OUT OF HERE (OR IS IT PUSH FORWARD)
                    //var action4 = SKAction.MoveBy(25, 0, 5);
                    //Player.RunAction(action4);
                    PlayerRide.MoveRight();
                    PlayerRide.SetPlayerDirection();

                    PlayerRide.RotatePlayer_Pitch();
                    break;
                    
                case ' '://FIRE THE MISSILES or MACHINE GUN!
                    
                    int index = PlayerRide.FireGun();
                    if(index >0){
                        AddChild(PlayerRide.MachineGun[index].Bullet);
                    }
                    /*
                    for (int i = 0; i < PlayerMagazine.Length; i += 1){

                        if (!PlayerMagazine[i].isFired)
                        {
                            
                           AddChild(PlayerMagazine[i].Bullet);
                            PlayerMagazine[i].FireBullet(PlayerRide.GetPitch(),
                                      PlayerRide.GetHeading(),
                                      PlayerRide.PlayerSpriteObject.Position.X,
                                      PlayerRide.PlayerSpriteObject.Position.Y);
                            
                            break;
                        }
                    }*/
                    break;


                    
            }

        }



#endif
        //**********************************************************************
        //*****Update(double currentTime){}*************************************
        //*****is an event that is your frame per second update feature*********
        //*****essentially your game engine if it's simple enough for writing***
        //*****after every frame checks for collisions between player weapons***
        //*****and enemy towersa label (myLabel1) is updated every 30 frames****
        //***** to display headingthis game is set to around 60 frames per sec**
        //*****for smoothness, Every Sec (60frames) a random enemy will fire****
        //**********************************************************************
        public override void Update(double currentTime)
        {
            // Called before each frame is rendered

            //check fuel

            //update screen gaugues
            SKLabelNode headingvalue = (SKLabelNode)GetChildNode("myLabel1");
            SKLabelNode scorelabel = (SKLabelNode)GetChildNode("ScoreLabel");
            SKSpriteNode NavArrow = (SKSpriteNode)GetChildNode("NavArrow");
            Random enemyNumber = new Random();
            if ((PlayerRide.PlayerSpriteObject != null) && (headingvalue != null))
            {
                if ((FrameNumber % 30) == 0)
                {
                    headingvalue.Text = PlayerRide.GetHeading().ToString();
                    var action = SKAction.MoveBy(PlayerRide.hSpeed, PlayerRide.vSpeed, 1.0);
                    PlayerRide.PlayerSpriteObject.RunAction(action);
                }

                var action2 = SKAction.RotateToAngle((nfloat)((PlayerRide.GetHeading() - 90) * Math.PI / 180), 1.0);
                NavArrow.RunAction(action2);

            } 

            if ((FrameNumber % 60) == 0)
            {
                int j = (enemyNumber.Next()) % ((EnemyTowers.Length));
                int y = (enemyNumber.Next()) % ((EnemyTowers[j].GuardMagazine.Length));

                if(EnemyTowers[j].GuardMagazine[y].Bullet != null){
                    
                    if (EnemyTowers[j].GuardMagazine[y].isFired == false)
                    {
                        AddChild(EnemyTowers[j].GuardMagazine[y].Bullet);
                        EnemyTowers[j].FireAtRandom();
                    }
                }
              

            }

            //EveryFrame we need to check for collisions between the missiles

            //1)check for player missile hits

            for (int i = 0; i < EnemyTowers.Length; i += 1)
            {
                if (PlayerRide.MachineGun[0].BulletCollided(PlayerRide.MachineGun, EnemyTowers[i]))
                {
                    EnemyTowers[i].TowerGotHit();
                    score += 10;
                    scorelabel.Text = "Score: " + score.ToString();
                }
            }

            for (int i = 0; i < EnemyTowers.Length; i += 1){
                if(EnemyTowers[i].isDead()){
                    EnemyTowers[i].tower.RemoveFromParent();
                }
            }

            FrameNumber += 1;
            /*
            if((FrameNumber% 120) == 0){
                for (int x = 0; x < EnemyTowers.Length; x = 1){
                    for (int y = 0; y < EnemyTowers[x].GuardMagazine.Length; y += 1){
                        if(EnemyTowers[x].GuardMagazine[y].getXposition() > Frame.Width){
                            EnemyTowers[x].GuardMagazine[y].Bullet.RemoveFromParent();
                        }
                    }
                } 
            } */
        }

    }
}

