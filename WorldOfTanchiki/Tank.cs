using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldOfTanchiki
{
    class Tank
    {
        int xLoc;
        int yLoc;

        int s = 1;

        private int speed;
        private int health;
        private int damage;

        private void Speedy(ITankBodySpeed tankSpeed)
        {
            speed = tankSpeed.Speed;
        }
        public int GetSpeed(out int speedy)
        {
            speedy = speed;
            return speedy;
        }

        private void Healthy(ITankBodyHealth tankHealth)
        {
            health = tankHealth.Health;
        }
        public int GetHealth(out int healthy)
        {
            healthy = health;
            return healthy;
        }

        private void Damagy(IBullet tankDamage)
        {
            damage = tankDamage.Damage;
        }
        public int GetDamage(out int damagy)
        {
            damagy = damage;
            return damagy;
        }
        public void ChooseSpeed(int pressBtn)
        {
            if (pressBtn == 0)
            {
                MinSpeed minSpeed = new MinSpeed();
                Speedy(minSpeed);
            }
            if (pressBtn == 2)
            {
                MedSpeed medSpeed = new MedSpeed();
                Speedy(medSpeed);
            }
            if (pressBtn == 5)
            {
                MaxSpeed maxSpeed = new MaxSpeed();
                Speedy(maxSpeed);
            }
        }

        

        public void ChooseHealth(int pressBtn)
        {
            if (pressBtn == 0)
            {
                MinHealth minHealth = new MinHealth();
                Healthy(minHealth);
            }
            if (pressBtn == 1)
            {
                MedHealth medHealth = new MedHealth();
                Healthy(medHealth);
            }
            if (pressBtn == 6)
            {
                MaxHealth maxHealth = new MaxHealth();
                Healthy(maxHealth);
            }
        }

        

        public void ChooseDamage(int pressBtn)
        {
            if (pressBtn == 0)
            {
                MinDamage minDamage = new MinDamage();
                Damagy(minDamage);
            }
            if (pressBtn == 3)
            {
                MedDamage medDamage = new MedDamage();
                Damagy(medDamage);
            }
            if (pressBtn == 4)
            {
                MaxDamage maxDamage = new MaxDamage();
                Damagy(maxDamage);
            }
        }


        public Tank(int xLoc, int yLoc)
        {
            this.xLoc = xLoc;
            this.yLoc = yLoc;
        }
        public int MoveUp(out int y, int speed)
        {
            y = yLoc-speed;
            yLoc = y;
            return y;
        }
        public int MoveDown(out int y, int speed)
        {
            y = yLoc + speed;
            return y;
        }
        public int MoveRight(out int x, int speed)
        {
            x = xLoc + speed;
            return x;
        }
        public int MoveLeft(out int x, int speed)
        {
            x = xLoc - speed;
            return x;
        }

        public int ShootUp(out int BulY)
        {
            BulY = yLoc;
            BulY -= 10 * s;
            s++;
            return BulY;
        }
        public int ShootRight(out int BulX)
        {
            BulX = xLoc;
            BulX += 10 * s;
            s++;
            return BulX;
        }
        public int ShootLeft(out int BulX)
        {
            BulX = xLoc;
            BulX -= 10 * s;
            s++;
            return BulX;
        }
        public int ShootDown(out int BulY)
        {
            BulY = yLoc;
            BulY += 10 * s;
            s++;
            return BulY;
        }


        private interface ITankBodyHealth
        {
            int Health { get; set; }
        }
        private interface ITankBodySpeed
        {
            int Speed { get; set; }
        }
        private interface IBullet
        {
            int Damage { get; set; }
        }
        private struct MaxHealth : ITankBodyHealth
        {
            public int Health { get => 3; set => Health = value; }
        }
        private struct MaxSpeed : ITankBodySpeed
        {
            public int Speed { get => 20; set => Speed = value; }
        }
        private struct MaxDamage : IBullet
        {
            public int Damage { get => 3; set => Damage = value; }
        }
        private struct MedHealth : ITankBodyHealth
        {
            public int Health { get => 2; set => Health = value; }
        }
        private struct MedSpeed : ITankBodySpeed
        {
            public int Speed { get => 15; set => Speed = value; }
            
        }
        private struct MedDamage : IBullet
        {
            public int Damage { get => 2; set => Damage = value; }
        }
        private struct MinHealth : ITankBodyHealth
        {
            public int Health { get => 1; set => Health = value; }
        }
        private struct MinSpeed : ITankBodySpeed
        {
            public int Speed { get => 10; set => Speed = value; }
        }
        private struct MinDamage : IBullet
        {
            public int Damage { get => 1; set => Damage = value; }
        }
    }
}
