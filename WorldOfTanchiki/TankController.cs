using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldOfTanchiki
{
    class TankController
    {
        List<TankController> players;

        int xLoc;
        int yLoc;
        Tank tank;
        char pressKey;

        int BulX;
        int BulY;
        public TankController(int xLoc, int yLoc, char pressKey)
        {
            this.xLoc = xLoc;
            this.yLoc = yLoc;
            tank = new Tank(xLoc, yLoc);
            this.pressKey = pressKey;
            BulX = xLoc;
            BulY = yLoc;
        }

        public void TakeTurn(bool change)
        {
            if (change)
            {

            }
        }
        public void GetHit()
        {
            
        }

        public void Move(int speed)
        {
            if (pressKey == 'w')
            {
                tank.MoveUp(out yLoc, speed);
            }
            if(pressKey == 's')
            {
                tank.MoveDown(out yLoc, speed);
            }
            if (pressKey == 'd')
            {
                tank.MoveRight(out xLoc, speed);
            }
            if (pressKey == 'a')
            {
                tank.MoveLeft(out xLoc, speed);
            }
        }

        public void Shoot()
        {
            if (pressKey == 'w')
            {
                tank.ShootUp(out BulY);
            }
            if (pressKey == 'd')
            {
                tank.ShootRight(out BulX);
            }
            if (pressKey == 'a')
            {
                tank.ShootLeft(out BulX);
            }
            if (pressKey == 's')
            {
                tank.ShootDown(out BulY);
            }
        }

        public int PosY()
        {
            return yLoc;
        }
        public int PosX()
        {
            return xLoc;
        }
        public int PosBulX()
        {
            if(pressKey == 'w' || pressKey == 'd' || pressKey == 's')
                return BulX + 40;
            if (pressKey == 'a')
                return BulX;
            return 0;

        }
        public int PosBulY()
        {
            if(pressKey == 'w' || pressKey == 's')
                return BulY;
            if (pressKey == 'd' || pressKey == 'a')
                return BulY + 40;
            return 0;
        }
        public void ChooseUpgrade(int pressBtn)
        {
            tank.ChooseSpeed(pressBtn);
            tank.ChooseHealth(pressBtn);
            tank.ChooseDamage(pressBtn);
        }
        public int GetUpdateSpeed(out int speedi)
        {
            tank.GetSpeed(out int speed);
            speedi = speed;
            return speedi;
        }
        public int GetUpdateDamage(out int damagi)
        {
            tank.GetDamage(out int damage);
            damagi = damage;
            return damagi;
        }
        public int GetUpdateHealth(out int healthi)
        {
            tank.GetHealth(out int health);
            healthi = health;
            return healthi;
        }
    }
}
