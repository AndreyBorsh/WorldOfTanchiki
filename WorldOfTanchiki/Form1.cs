using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorldOfTanchiki
{
    public partial class Form1 : Form
    {
        Random rnd = new Random();
        int speed;
        int health;
        public Form1()
        {
            InitializeComponent();
            pictureBox3.Location = new Point(rnd.Next(200,900), rnd.Next(100,500));
            pressBtn = 0;
            Game game = new Game(0, 0, 'z');
            game.UpSpeed(pressBtn, out speed);
            game.UpHealth(pressBtn, out health);
            game.UpDamage(pressBtn, out damage);
        }
        int ammo = 1;

        char pressKey;

        int pressBtn;

        bool pressSpace;
        bool pressW;
        bool pressS;
        bool pressD;
        bool pressA;

        bool canShoot = true;
        bool canMove;

        bool takeTurn;

        bool btn1;
        bool btn2;
        bool btn3;
        bool btn4;
        bool btn5;
        bool btn6;

        int sec = 90;  // время хода

        int hitCount = 0; // количество попаданий по врагу

        int damage = 0;

        int sumCoin = 0;
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            int x = pictureBox1.Location.X;
            int y = pictureBox1.Location.Y;

            Game game;

            Image image = Properties.Resources.imgonline_com_ua_Transparent_backgr_45C9eZocmSix2;
            pictureBox1.Image = image;
            if (e.KeyCode == Keys.W && y >= 33 && !takeTurn)
            {
                pressW = true;
                pressD = false;
                pressS = false;
                pressA = false;

                pressKey = 'w';
                game = new Game(x, y, pressKey);
                game.Move(out y, out x, speed);
                pictureBox1.Location = new Point(x,y);
                GetCoin(x, y);
            }
            if (e.KeyCode == Keys.S && y <= 500 && !takeTurn)
            {
                pressS = true;
                pressW = false;
                pressD = false;
                pressA = false;

                pressKey = 's';
                game = new Game(x, y, pressKey);
                game.Move(out y, out x, speed);
                pictureBox1.Location = new Point(x, y);
                image.RotateFlip(RotateFlipType.Rotate180FlipX);
                GetCoin(x, y);
            }
            
            if (e.KeyCode == Keys.D && x <= 950 && !takeTurn)
            {
                pressD = true;
                pressW = false;
                pressS = false;
                pressA = false;

                pressKey = 'd';
                game = new Game(x, y, pressKey);
                game.Move(out y, out x, speed);
                pictureBox1.Location = new Point(x, y);
                image.RotateFlip(RotateFlipType.Rotate90FlipY);
                GetCoin(x, y);
            }
            if (e.KeyCode == Keys.A && x >= 65 && !takeTurn)
            {
                pressA = true;
                pressW = false;
                pressD = false;
                pressS = false;

                pressKey = 'a';
                game = new Game(x, y, pressKey);
                game.Move(out y, out x, speed);
                pictureBox1.Location = new Point(x, y);
                image.RotateFlip(RotateFlipType.Rotate270FlipY);
                GetCoin(x,y);
            }
            if (e.KeyCode == Keys.Space && !takeTurn)
            {
                ammo--;
                pictureBox1.Image = image;
                pressSpace = true;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void GetCoin(int x, int y)
        {
            if (y + 88 >= pictureBox3.Location.Y && y < pictureBox3.Location.Y + 50 && x + 88 >= pictureBox3.Location.X && x < pictureBox3.Location.X + 50)
            {
                sumCoin += 20;
                label2.Text = Convert.ToString(sumCoin);
                pictureBox3.Location = new Point(-200, -200);
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            Game game;

            int x = pictureBox1.Location.X;
            int y = pictureBox1.Location.Y;

            int x2 = pictureBox4.Location.X;
            int y2 = pictureBox4.Location.Y;

            int posBulX;
            int posBulY;

            Image image = Properties.Resources.imgonline_com_ua_Transparent_backgr_45C9eZocmSix2;
            Image image2 = Properties.Resources.imgonline_com_ua_Transparent_backgr_OxuXJfkRSy;

            sec -= 1;
            if (sec == 0 && !takeTurn)
            {
                pictureBox5.Enabled = false;
                MessageBox.Show("Время хода вышло");
                sec = 90;
                pictureBox5.Enabled = true;
                takeTurn = true;
                ammo = 1;
                game = new Game(0,0,'\0');
                game.TakeTurn(takeTurn);
            }
            if (sec != 0 && takeTurn)
            {
                pictureBox1.Enabled = false;
                
                image2.RotateFlip(RotateFlipType.Rotate90FlipY);
                pictureBox4.Image = image2;
                if (x2 < x - 150)
                {
                    x2 += 15;
                    pictureBox4.Location = new Point(x2, y2);
                    Refresh();
                }
                if (y2 < y && x2 >= x - 150)
                {
                    image2.RotateFlip(RotateFlipType.Rotate90FlipX);
                    pictureBox4.Image = image2;
                    y2 += 15;
                    pictureBox4.Location = new Point(x2, y2);
                    Refresh();
                }
                if(y2 >= y && x2 >= x - 150)
                {
                    pictureBox2.Visible = true;
                    posBulX = pictureBox4.Location.X;
                    posBulY = pictureBox4.Location.Y;
                    game = new Game(x2, y2, 'd');
                    int s = 0;
                    if(health > 0 && pictureBox1.Visible && ammo > 0)
                    {
                        while (s != 20 && canShoot)
                        {
                            game.Shoot(out x2, out y2, out posBulX, out posBulY);
                            pictureBox2.Location = new Point(posBulX, posBulY);
                            s++;
                            Refresh();
                        }
                        ammo--;
                        health--;
                    }

                    if(health <= 0)
                    {
                        pictureBox1.Visible = false;
                        canShoot = false;
                    }
                    pictureBox2.Visible = false;
                }
                if(x2 > x + 150)
                {
                    image.RotateFlip(RotateFlipType.Rotate180FlipY);
                    pictureBox4.Image = image2;
                    x2 -= 15;
                    pictureBox4.Location = new Point(x2, y2);
                    Refresh();
                    canMove = true;
                }
                if (y2 > y && x2 <= x + 150 && canMove)
                {
                    image2.RotateFlip(RotateFlipType.Rotate90FlipY);
                    pictureBox4.Image = image2;
                    y2 -= 15;
                    pictureBox4.Location = new Point(x2, y2);
                    Refresh();
                }
                if (y2 <= y && x2 <= x + 150 && canMove)
                {
                    pictureBox2.Visible = true;
                    posBulX = pictureBox4.Location.X;
                    posBulY = pictureBox4.Location.Y;
                    game = new Game(x2, y2, 'a');
                    int s = 0;
                    canShoot = true;
                    if(health > 0 && pictureBox1.Visible && ammo > 0)
                    {
                        while (s != 20 && canShoot)
                        {
                            game.Shoot(out x2, out y2, out posBulX, out posBulY);
                            pictureBox2.Location = new Point(posBulX, posBulY);
                            s++;
                            
                            Refresh();
                            canMove = false;
                        }
                        ammo--;
                        health--;
                    }
                    
                    if (health <= 0)
                    {
                        pictureBox1.Visible = false;
                        canShoot = false;
                    }
                    pictureBox2.Visible = false;
                }
            }
            else if(sec == 0)
            {
                pictureBox5.Enabled = false;
                pictureBox4.Enabled = false;
                takeTurn = false;
                MessageBox.Show("Время хода вышло");
                ammo = 1;
                sec = 90;
                pictureBox5.Enabled = true;
                pictureBox1.Enabled = true;
            }

            if (pressSpace && ammo >= 0)
            {
                pictureBox2.Visible = true;

                if (pressW)
                {
                    posBulX = pictureBox1.Location.X;
                    posBulY = pictureBox1.Location.Y;

                    game = new Game(x, y, pressKey);
                    int s = 0;
                    game.Shoot(out x, out y, out posBulX, out posBulY);
                    while (s != 20)
                    {
                        game.Shoot(out x, out y, out posBulX, out posBulY);
                        pictureBox2.Location = new Point(posBulX, posBulY);
                        s++;
                        Refresh();

                        if(posBulX <= x2 + 100 && posBulX >= x2 && posBulY >= y2 && posBulY <= y2 + 100)
                        {
                            pictureBox2.Enabled = false;
                            hitCount += damage;
                            if (hitCount == 1)
                                pictureBox6.Image = Properties.Resources.Health75;
                            if(hitCount == 2)
                                pictureBox6.Image = Properties.Resources.Health50;
                            if (hitCount >= 3)
                            {
                                pictureBox6.Image = Properties.Resources.Health0;
                                pictureBox4.Visible = false;
                            }
                            else
                                break;
                        }
                    }
                }
                if (pressD)
                {
                    image.RotateFlip(RotateFlipType.Rotate90FlipY);
                    pictureBox1.Image = image;
                    posBulX = pictureBox1.Location.X;
                    posBulY = pictureBox1.Location.Y;

                    game = new Game(x, y, pressKey);
                    int s = 0;
                    game.Shoot(out x, out y, out posBulX, out posBulY);
                    while (s != 20)
                    {
                        game.Shoot(out x, out y, out posBulX, out posBulY);
                        pictureBox2.Location = new Point(posBulX, posBulY);
                        s++;
                        Refresh();

                        if (posBulX <= x2 + 100 && posBulX >= x2 && posBulY >= y2 && posBulY <= y2 + 100)
                        {
                            pictureBox2.Enabled = false;
                            hitCount += damage;
                            if (hitCount == 1)
                                pictureBox6.Image = Properties.Resources.Health75;
                            if (hitCount == 2)
                                pictureBox6.Image = Properties.Resources.Health50;
                            if (hitCount >= 3)
                            {
                                pictureBox6.Image = Properties.Resources.Health0;
                                pictureBox4.Visible = false;
                            }
                            else
                                break;
                        }
                    }            
                }
                if (pressA)
                {
                    image.RotateFlip(RotateFlipType.Rotate270FlipY);
                    pictureBox1.Image = image;
                    posBulX = pictureBox1.Location.X;
                    posBulY = pictureBox1.Location.Y;

                    game = new Game(x, y, pressKey);
                    int s = 0;
                    game.Shoot(out x, out y, out posBulX, out posBulY);
                    while (s != 20)
                    {
                        game.Shoot(out x, out y, out posBulX, out posBulY);
                        pictureBox2.Location = new Point(posBulX, posBulY);
                        s++;
                        Refresh();

                        if (posBulX <= x2 + 100 && posBulX >= x2 && posBulY >= y2 && posBulY <= y2 + 100)
                        {
                            pictureBox2.Enabled = false;
                            hitCount += damage;
                            if (hitCount == 1)
                                pictureBox6.Image = Properties.Resources.Health75;
                            if (hitCount == 2)
                                pictureBox6.Image = Properties.Resources.Health50;
                            if (hitCount >= 3)
                            {
                                pictureBox6.Image = Properties.Resources.Health0;
                                pictureBox4.Visible = false;
                            }
                            else
                                break;
                        }
                    }
                }
                if (pressS)
                {
                    image.RotateFlip(RotateFlipType.Rotate180FlipX);
                    pictureBox1.Image = image;
                    posBulX = pictureBox1.Location.X;
                    posBulY = pictureBox1.Location.Y;

                    game = new Game(x, y, pressKey);
                    int s = 0;
                    game.Shoot(out x, out y, out posBulX, out posBulY);
                    while (s != 20)
                    {
                        game.Shoot(out x, out y, out posBulX, out posBulY);
                        pictureBox2.Location = new Point(posBulX, posBulY);
                        s++;
                        Refresh();

                        if (posBulX <= x2 + 100 && posBulX >= x2 && posBulY >= y2 && posBulY <= y2 + 100)
                        {
                            pictureBox2.Enabled = false;
                            hitCount += damage;
                            if (hitCount == 1)
                                pictureBox6.Image = Properties.Resources.Health75;
                            if (hitCount == 2)
                                pictureBox6.Image = Properties.Resources.Health50;
                            if (hitCount >= 3)
                            {
                                pictureBox6.Image = Properties.Resources.Health0;
                                pictureBox4.Visible = false;
                            }
                            else
                                break;
                        }
                    }
                }

                pressSpace = false;
                pressW = false;
                pressD = false;
                pressS = false;
                pressA = false;
                pictureBox2.Visible = false;
            }
            if (!pictureBox1.Visible)
            {
                sec = 90;

                pictureBox5.Refresh();
                pictureBox5.Enabled = false;
                pictureBox4.Enabled = false;

                button1.Visible = true;
                button2.Visible = true;
                button3.Visible = true;
                if(btn3)
                    button4.Visible = true;
                if(btn2)
                    button5.Visible = true;
                if(btn1)
                    button6.Visible = true;

                newRoundBTN.Visible = true;

                label3.Visible = true;
                label4.Visible = true;
                label5.Visible = true;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void newRoundBTN_Click(object sender, EventArgs e)
        {
            pictureBox1.Visible = true;

            pictureBox4.Location = new Point(34,35);
            pictureBox1.Location = new Point(914,479);

            pictureBox5.Refresh();

            pictureBox5.Enabled = true;
            pictureBox4.Enabled = false;
            pictureBox1.Enabled = true;

            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
            button5.Visible = false;
            button6.Visible = false;

            newRoundBTN.Visible = false;

            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;

            canShoot = true;

            takeTurn = false;

            ammo = 1;

            hitCount = 0;

            pictureBox6.Image = Properties.Resources.FullHealth;

            pictureBox3.Location = new Point(rnd.Next(200, 900), rnd.Next(100, 500));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(label2.Text) >= Convert.ToInt32(button1.Text))
            {
                btn1 = true;
                button1.Enabled = false;
                button6.Visible = true;
                label2.Text = Convert.ToString(Convert.ToInt32(label2.Text) - Convert.ToInt32(button1.Text));

                pressBtn = 1;
                Game game = new Game(0, 0, 'z');
                game.UpHealth(pressBtn, out health);
            }
            else
                MessageBox.Show("Денег нет");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(label2.Text) >= Convert.ToInt32(button2.Text))
            {
                btn2 = true;
                button2.Enabled = false;
                button5.Visible = true;
                label2.Text = Convert.ToString(Convert.ToInt32(label2.Text) - Convert.ToInt32(button2.Text));

                pressBtn = 2;
                Game game = new Game(0, 0, 'z');
                game.UpSpeed(pressBtn, out speed);
            }
            else
                MessageBox.Show("Денег нет");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(label2.Text) >= Convert.ToInt32(button3.Text))
            {
                btn3 = true;
                button3.Enabled = false;
                button4.Visible = true;
                label2.Text = Convert.ToString(Convert.ToInt32(label2.Text) - Convert.ToInt32(button3.Text));

                pressBtn = 3;
                Game game = new Game(0, 0, 'z');
                game.UpDamage(pressBtn, out damage);
            }
            else
                MessageBox.Show("Денег нет");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(label2.Text) >= Convert.ToInt32(button4.Text))
            {
                btn4 = true;
                button4.Enabled = false;
                label2.Text = Convert.ToString(Convert.ToInt32(label2.Text) - Convert.ToInt32(button4.Text));

                pressBtn = 4;
                Game game = new Game(0, 0, 'z');
                game.UpDamage(pressBtn, out damage);
            }
            else
                MessageBox.Show("Денег нет");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(label2.Text) >= Convert.ToInt32(button5.Text))
            {
                btn5 = true;
                button5.Enabled = false;
                label2.Text = Convert.ToString(Convert.ToInt32(label2.Text) - Convert.ToInt32(button5.Text));

                pressBtn = 5;
                Game game = new Game(0, 0, 'z');
                game.UpSpeed(pressBtn, out speed);
            }
            else
                MessageBox.Show("Денег нет");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(label2.Text) >= Convert.ToInt32(button6.Text))
            {
                btn6 = true;
                button6.Enabled = false;
                label2.Text = Convert.ToString(Convert.ToInt32(label2.Text) - Convert.ToInt32(button6.Text));

                pressBtn = 6;
                Game game = new Game(0, 0, 'z');
                game.UpHealth(pressBtn, out health);
            }
            else
                MessageBox.Show("Денег нет");
        }
    }
}
