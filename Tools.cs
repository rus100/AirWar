using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirWar
{
    class Tools
    {
        public int nomer=1;
       public float y=50;
       public float speed = 0;
       public float x = 800;
      public void Add() {
           Random rnd = new Random();
           y = rnd.Next(35, 250);
           x = 800;
           Random rnd1 = new Random();
           nomer = rnd1.Next(0, 2);
       }
    }
}
