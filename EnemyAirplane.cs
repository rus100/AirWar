using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace AirWar
{
    class EnemyAirplane
    {
       public float life;
       public float speed;
       public int nomer;
      public  float x;
      public  float y;
      public float k;
     public bool added = false;
     public bool shoot=false;
     float speed1;
       public void Add() {
           x = 800;
            life = 100;
            Random rnd=new Random();
            y = rnd.Next(35, 250);
            Random rnd1 = new Random();
            speed = (float)(2*rnd1.NextDouble());
            Random rnd2 = new Random();
            nomer = rnd2.Next(0, 2);
            added = true;
        }
       public void Behaviour(bool vishe)
       {
           float a = 0;
           k = 0.5f;
           
           if (vishe == true)
           {
               a = 1;

           }
           else {
               a = -1;
           } 
         
       speed1 =  2*(10 / this.speed);
      if ((added == true)) {
          if ((this.y > 35) && (this.y < 250 - a * speed1)) { this.y += a*k * speed1; }
              if ((this.y >= 250))
              {
                   this.y = 250;
                   this.y -= speed1;
               }
             if ((this.y <= 35))
              {
                 this.y = 35;
                 this.y += speed1;
          } } 
       }
       public void Shooting() { this.shoot = true; }
    }
}
