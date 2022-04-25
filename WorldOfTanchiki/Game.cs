using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldOfTanchiki
{
    class Game
    {
        int xLoc;
        int yLoc;

        TankController tc;

        public Game(int xLoc, int yLoc, char pressKey)
        {
            this.xLoc = xLoc;
            this.yLoc = yLoc;
            tc = new TankController(this.xLoc, this.yLoc, pressKey);
        }
        public void Move(out int yLoc, out int xLoc, int speed)
        {
            tc.Move(speed);
            yLoc = tc.PosY();
            xLoc = tc.PosX();
        }
        public void Shoot(out int yLoc, out int xLoc, out int BulX, out int BulY)
        {
            tc.Shoot();
            BulX = tc.PosBulX();
            BulY = tc.PosBulY();
            yLoc = tc.PosY();
            xLoc = tc.PosX();
            
        }
        public void TakeTurn(bool takeTurn)
        {
            tc.TakeTurn(takeTurn);
        }

        public int UpSpeed(int pressBtn, out int speed)
        {
            tc.ChooseUpgrade(pressBtn);
            tc.GetUpdateSpeed(out speed);
            return speed;
        }
        public int UpHealth(int pressBtn, out int health)
        {
            tc.ChooseUpgrade(pressBtn);
            tc.GetUpdateHealth(out health);
            return health;
        }
        public int UpDamage(int pressBtn, out int damage)
        {
            tc.ChooseUpgrade(pressBtn);
            tc.GetUpdateDamage(out damage);
            return damage;
        }
    }
}
