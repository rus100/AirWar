using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace AirWar
{         
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        int scoreinterval = 0;
        int level = 1;
        float uronc;
        float uronarpl;
        float k;
        float start_life;
        float start_fuel;
        bool fuel_set = false;
        bool life_set = false;
        EnemyAirplane enemy;
        List<EnemyAirplane> enemys = new List<EnemyAirplane>();
        List<Shoot> shoots = new List<Shoot>();
        List<Shoot> shootsenemy = new List<Shoot>(); 
        Airplane airpl = new Airplane(); 
        Tools tool = new Tools();
        Shoot sh;
        float second;
        bool vzriv;
        int nomer;
        int nomer1;
        bool added = false;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D texture;
        Texture2D airplane;
        Texture2D messer;
        Texture2D foker;
        Texture2D fuel;
        Texture2D heart;
        Texture2D heart1;
        Texture2D speed;
        Texture2D fuel1;
        Texture2D gun;
        Texture2D gold;
        Texture2D explosion;
        SoundEffect tratata;
        SoundEffect bum;
        float dx = 1f;
        float dy = 1f;
        Vector2 vector1;
        Vector2 vector;
        Vector2 vectorend;
        Vector2 vector3;
        Vector2 gameover2;
        Vector2 explose;
        string s,s1,s2,s0;
       List<Vector2> vectorgun=new List<Vector2>();
       List<Vector2> vectorgunenemy = new List<Vector2>();
       List<Vector2> vectorplane = new List<Vector2>();
       List<Rectangle> messers = new List<Rectangle>(10);
       List<Rectangle> fokers = new List<Rectangle>(10);
       List<int> nomers = new List<int>();
        SpriteFont font;
        SpriteFont gameover;
        Rectangle m ;
        Rectangle f ;
        bool gameover1=false;
        SettingsLevel settings = new SettingsLevel();
        bool level_completed = false;
        bool exited = false;
        bool win = false;
        bool nachalo_igri=true;
        bool vzriv_texture;
        float last_speed_gun_enemy;
        public Game1()
        {     
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            settings.SetParametrs(level);
            scoreinterval = settings.scoreinterval;
            uronarpl = settings.uronairplan;
            uronc = settings.uroncomp;
            start_fuel = settings.fuel_start;
            start_life = settings.life_start;
            k = settings.k;
            airpl.life = start_life;
            airpl.fuel = start_fuel;
  
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            texture = Content.Load<Texture2D>(@"Image\pole-trava-derevya-nebo-gory");
             airplane = Content.Load<Texture2D>(@"Image\mig3");
             messer = Content.Load<Texture2D>(@"Image\messer");
             foker = Content.Load<Texture2D>(@"Image\foker");
             fuel = Content.Load<Texture2D>(@"Image\fuel");
             heart = Content.Load<Texture2D>(@"Image\heart");
             heart1 = Content.Load<Texture2D>(@"Image\heart1");
             font = Content.Load<SpriteFont>(@"Font\SpriteFont1");
             gameover = Content.Load<SpriteFont>(@"Font\SpriteFont2");
             speed = Content.Load<Texture2D>(@"Image\speed");
             fuel1 = Content.Load<Texture2D>(@"Image\fuel1");
             gun = Content.Load<Texture2D>(@"Image\gun");
             gold = Content.Load<Texture2D>(@"Image\gold");
             tratata = Content.Load<SoundEffect>(@"Sound\gun5");
             bum = Content.Load<SoundEffect>(@"Sound\mix3");
             explosion = Content.Load<Texture2D>(@"Image\explosion");
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
             
            
            if(Keyboard.GetState().IsKeyDown(Keys.Enter)==true){
                nachalo_igri = false;
            }
            if (nachalo_igri == false)
            {
                if (gameover1 == false)
                {
                    if (level_completed == false)
                    {
                        vzriv = false;
                        Rectangle airplan = new Rectangle((int)vector1.X, (int)vector1.Y, airplane.Width, airplane.Height);
                        int j1 = -1;
                        int j2 = -1;
                        vector.X -= dx;
                        airpl.speed = dx;
                        second = (float)gameTime.TotalGameTime.TotalSeconds;
                        if (level == 1) {airpl.fuel -= 100 * (airpl.speed / 750) * ((float)(second / 1000)); }
                        if (level == 2) { airpl.fuel -= 100 * (airpl.speed / 750) * ((float)(second / 3000)); }
                        if (level >= 3) { airpl.fuel -= 100 * (airpl.speed / 750) * ((float)(second / 8000)); }
                        if (airpl.fuel <= 0)
                        {
                            airpl.fuel = 0;
                        }

                        if (Keyboard.GetState().IsKeyDown(Keys.Space) == true)
                        {
                            if (gameTime.TotalGameTime.Ticks % 30 == 0)
                            {
                                sh = new Shoot();
                                tratata.Play();
                                sh.speed = dx + 1f;
                                sh.y = vector1.Y + (airplane.Height / 2);
                                sh.x = airplane.Width;
                                shoots.Add(sh);
                                Vector2 v = new Vector2(sh.x, sh.y);
                                vectorgun.Add(v);
                            }
                        }
                        for (int i = 0; i < shoots.Count; i++)
                        {
                            float x0 = vectorgun[i].X;
                            float y0 = vectorgun[i].Y;
                            //vectorgun.RemoveAt(i);
                            x0 += shoots[i].speed;
                            Vector2 v = new Vector2(x0, y0);
                            vectorgun[i] = v;
                            if (x0 > 850)
                            {
                                shoots.RemoveAt(i);
                                vectorgun.RemoveAt(i);
                            }
                        }
                        
                        if ((Keyboard.GetState().IsKeyDown(Keys.D) == true))
                        { if (dx < 3f) { dx += 0.016f; } }
                        if ((Keyboard.GetState().IsKeyDown(Keys.A) == true))
                        { if (dx > 0.5f) { dx -= 0.016f; } }
                        if ((Keyboard.GetState().IsKeyDown(Keys.W) == true))
                        { vector1.Y -= dy; }
                        if ((Keyboard.GetState().IsKeyDown(Keys.S) == true))
                        { vector1.Y += dy; }
                        
                        if ((vector1.Y < 35)) { vector1.Y = 35; }
                        if ((vector1.Y > 250)) { vector1.Y = 250; }
                        vectorend.X = Window.ClientBounds.Width + vector.X;
                        if (vectorend.X < 0) { vector.X = 0; }
                        airpl.y = vector1.Y;

                        if (vectorplane.Count > 0)
                        {
                            if ((vectorplane[vectorplane.Count - 1].X < 160))
                            {
                                enemy = new EnemyAirplane();
                                enemy.Add();
                                added = enemy.added;
                                nomer = enemy.nomer;
                                enemys.Add(enemy);
                                Vector2 v = new Vector2();
                                v.X = enemy.x;
                                v.Y = enemy.y;
                                nomers.Add(enemy.nomer);
                                enemy.k = k;
                                vectorplane.Add(v);
                                if (enemys.Count > 0)
                                {
                                    if (enemy.nomer == 0)
                                    {
                                        m = new Rectangle();
                                        m.X = (int)enemy.x;
                                        m.Y = (int)enemy.y;
                                        m.Width = messer.Width;
                                        m.Height = messer.Height;
                                        messers.Add(m);
                                    }
                                    else
                                    {
                                        f = new Rectangle();
                                        f.X = (int)enemy.x;
                                        f.Y = (int)enemy.y;
                                        f.Width = messer.Width;
                                        f.Height = messer.Height;
                                        fokers.Add(f);
                                    }
                                }
                                //дописать
                            }
                        }
                        else
                        {
                            enemy = new EnemyAirplane();
                            enemy.Add();
                            added = enemy.added;
                            nomer = enemy.nomer;
                            enemys.Add(enemy);
                            Vector2 v = new Vector2();
                            v.X = enemy.x;
                            v.Y = enemy.y;
                            enemy.k = k;
                            vectorplane.Add(v);
                            nomers.Add(enemy.nomer);
                            if (enemys.Count > 0)
                            {
                                if (enemy.nomer == 0)
                                {
                                    m = new Rectangle();
                                    m.X = (int)enemy.x;
                                    m.Y = (int)enemy.y;
                                    m.Width = messer.Width;
                                    m.Height = messer.Height;
                                    messers.Add(m);
                                }
                                else
                                {
                                    f = new Rectangle();
                                    f.X = (int)enemy.x;
                                    f.Y = (int)enemy.y;
                                    f.Width = messer.Width;
                                    f.Height = messer.Height;
                                    fokers.Add(f);
                                }
                            }
                        }
                        for (int i = 0; i < enemys.Count; i++)
                        {
                            if (gameTime.TotalGameTime.Ticks % 60 == 0)
                            {
                                if (enemys[i].y - airpl.y <= 0)
                                {
                                    enemys[i].Behaviour(true);
                                }
                                else { enemys[i].Behaviour(false); }

                            }
                        }
                        for (int i = 0; i < enemys.Count; i++)
                        {
                            float x0 = vectorplane[i].X;
                            float y0 = vectorplane[i].Y;
                            x0 -= enemys[i].speed + dx;
                            y0 = enemys[i].y;
                            Vector2 v = new Vector2(x0, y0);
                            vectorplane[i] = v;
                            if (nomers[i] == 0)
                            {
                                j1++;

                                if (j1 < messers.Count)
                                {
                                    Rectangle m = new Rectangle((int)vectorplane[i].X, (int)vectorplane[i].Y, messer.Width, messer.Height);
                                    messers[j1] = m;
                                }
                            }
                            else
                            {
                                j2++;
                                if (j2 < fokers.Count)
                                {

                                    Rectangle f = new Rectangle((int)vectorplane[i].X, (int)vectorplane[i].Y, foker.Width, foker.Height);
                                    fokers[j2] = f;
                                }
                            }
                        }

                        j1 = -1;
                        j2 = -1;
                           vzriv_texture = false;
                        for (int i = 0; i < enemys.Count; i++)
                        {
                                
                            if (enemys[i].life <= 0)
                            { last_speed_gun_enemy = enemys[i].speed;
                            
                            vzriv_texture = true; 
                            explose.X = vectorplane[i].X;
                            explose.Y = vectorplane[i].Y;
                                bum.Play();
                                
                                vzriv = true;
                                
                                if (nomers[i] == 0)
                                {
                                    j1++;
                                    if (j1 < messers.Count) { messers.RemoveAt(j1); }
                                }
                                else
                                {
                                    j2++;
                                    if (j2 < fokers.Count) { fokers.RemoveAt(j2); }
                                }
                                enemys[i].added = false;
                                vectorplane.RemoveAt(i);
                                enemys.RemoveAt(i);
                                nomers.RemoveAt(i); 
                            }    
                        }
                        j1 = -1;
                        j2 = -1;
                        for (int i = 0; i < enemys.Count; i++)
                        {
                            float x0 = vectorplane[i].X;
                            float y0 = vectorplane[i].Y;
                            if (x0 < -200)
                            {
                                if (nomers[i] == 0)
                                {
                                    j1++;
                                    if (j1 < messers.Count) { messers.RemoveAt(j1); }
                                }
                                else
                                {
                                    j2++;
                                    if (j2 < fokers.Count) { fokers.RemoveAt(j2); }
                                }
                                nomers.RemoveAt(i);
                                enemys[i].added = false;
                                vectorplane.RemoveAt(i);
                                enemys.RemoveAt(i);
                            }
                        }
                        for (int i = 0; i < enemys.Count; i++)
                        {
                            if (Math.Abs(enemys[i].y - airpl.y) <= airplan.Height)
                            {
                                enemys[i].Shooting();
                            }
                        }
                        for (int j = 0; j < enemys.Count; j++)
                        {
                            if (enemys[j].shoot == true)
                            {
                                if (gameTime.TotalGameTime.Ticks % 50 == 0)
                                {
                                    sh = new Shoot();
                                    tratata.Play();
                                    if (vzriv == false) { sh.speed = enemys[j].speed + 1f + dx; }
                                    else {
                                         
                                        sh.speed = last_speed_gun_enemy + 1f + dx; }
                                    if (nomers[j] == 0)
                                    {
                                        sh.y = vectorplane[j].Y + (messer.Height / 2);
                                        sh.x = vectorplane[j].X;
                                    }
                                    else
                                    {
                                        sh.y = vectorplane[j].Y + (foker.Height / 2);
                                        sh.x = vectorplane[j].X;
                                    }
                                    shootsenemy.Add(sh);
                                    Vector2 v = new Vector2(sh.x, sh.y);
                                    vectorgunenemy.Add(v);
                                }

                                for (int i = 0; i < shootsenemy.Count; i++)
                                {
                                    float x0 = vectorgunenemy[i].X;
                                    float y0 = vectorgunenemy[i].Y;
                                    x0 -= shootsenemy[i].speed;
                                    Vector2 v = new Vector2(x0, y0);
                                    vectorgunenemy[i] = v;
                                    Rectangle guns = new Rectangle((int)vectorgunenemy[i].X, (int)vectorgunenemy[i].Y, gun.Width, gun.Height);
                                    if (guns.Intersects(airplan) == true)
                                    {
                                        shootsenemy.RemoveAt(i);
                                        vectorgunenemy.RemoveAt(i);
                                        airpl.life -= uronarpl;
                                    }

                                    if (x0 < 0)
                                    {
                                        shootsenemy.RemoveAt(i);
                                        vectorgunenemy.RemoveAt(i);
                                    }
                                }
                            }
                        }
                        if ((vector3.X == 0) || (vector3.X < -40))
                        {
                            tool.Add();
                            nomer1 = tool.nomer;
                        }
                        tool.speed = dx;
                        tool.x -= tool.speed;
                        vector3.X = tool.x;
                        vector3.Y = tool.y;
                        j1 = -1;
                        j2 = -1;
                        if (enemys.Count > 0)
                        {
                            for (int i = 0; i < enemys.Count; i++)
                            {
                                if (enemys[i].nomer == 0)
                                {
                                    j1++;
                                    if (j1 < messers.Count)
                                    {
                                        if (airplan.Intersects(messers[j1]) == true)  
                                        {
                                            bum.Play();
                                            gameover1 = true;
                                        }
                                        for (int j = 0; j < shoots.Count; j++)
                                        {
                                            Rectangle guns = new Rectangle((int)vectorgun[j].X, (int)vectorgun[j].Y, gun.Width, gun.Height);
                                            if (guns.Intersects(messers[j1]) == true)
                                            {
                                                shoots.RemoveAt(j);
                                                vectorgun.RemoveAt(j);
                                                enemys[i].life -= uronc; //поправил уменьшение жизни при попадании в самолет противника.
                                                airpl.score += scoreinterval;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    j2++;
                                    if (j2 < fokers.Count)
                                    {
                                        if (airplan.Intersects(fokers[j2]) == true)
                                        {
                                            bum.Play();
                                            gameover1 = true;
                                        }
                                        for (int j = 0; j < shoots.Count; j++)
                                        {
                                            Rectangle guns = new Rectangle((int)vectorgun[j].X, (int)vectorgun[j].Y, gun.Width, gun.Height);
                                            if (guns.Intersects(fokers[j2]) == true)
                                            {
                                                shoots.RemoveAt(j);
                                                vectorgun.RemoveAt(j);
                                                enemys[i].life -= uronc;
                                                airpl.score += scoreinterval;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (tool.nomer == 0)
                        {
                            Rectangle hearts = new Rectangle((int)vector3.X, (int)vector3.Y, heart.Width, heart.Height);
                            life_set = airplan.Intersects(hearts);
                            if ((life_set == true))
                            {
                                if ((vector3.X < airplane.Width) && (vector3.X >= 0))
                                {
                                    tool = new Tools();
                                    tool.Add();
                                    tool.x = 960;
                                    nomer1 = tool.nomer;
                                    life_set = true;
                                    if (airpl.life < 100)
                                    {
                                        airpl.life += 10f;
                                    }
                                    if (airpl.life > 100)
                                    {
                                        airpl.life = 100f;
                                    }
                                    if (airpl.life < 0)
                                    {
                                        airpl.life = 0f;
                                    }
                                }
                                if (vector3.X <= 0) { life_set = false; }
                                life_set = false;
                            }
                        }
                        else
                        {
                            Rectangle fuels = new Rectangle((int)vector3.X, (int)vector3.Y, fuel.Width, fuel.Height);
                            fuel_set = airplan.Intersects(fuels);
                            if ((fuel_set == true))
                            {
                                if ((vector3.X < airplane.Width) && (vector3.X >= 0))
                                {
                                    tool = new Tools();
                                    tool.Add();
                                    tool.x = 960;
                                    nomer1 = tool.nomer;
                                    fuel_set = true;
                                    if (airpl.fuel < 100)
                                    {
                                        airpl.fuel += 10f;
                                    }
                                    if (airpl.fuel > 100)
                                    {
                                        airpl.fuel = 100f;
                                    }
                                }
                                if (vector3.X <= 0) { fuel_set = false; }
                                fuel_set = false;
                            }
                        }

                        if (airpl.score >= settings.needscore_for_new_level)
                        {
                            level_completed = true;
                            if (level == 5)
                            {
                                win = true;
                                if (Keyboard.GetState().IsKeyDown(Keys.Enter) == true) {
                                    Exit();
                                }
                            }
                        }
                        if (airpl.fuel <= 0)
                        {
                            gameover1 = true;
                        }
                        if (airpl.life <= 0)
                        {
                            bum.Play();
                            gameover1 = true;
                        }

                    }
                    else
                    {
                        if (Keyboard.GetState().IsKeyDown(Keys.Enter)==true)
                        {
                            if (level_completed == true)
                            {
                                level++;
                                if (level < 5)
                                {
                                    settings.SetParametrs(level);
                                    scoreinterval = settings.scoreinterval;
                                    uronarpl = settings.uronairplan;
                                    uronc = settings.uroncomp;
                                    start_fuel = settings.fuel_start;
                                    start_life = settings.life_start;
                                    k = settings.k;
                                    airpl.life = start_life;
                                    airpl.fuel = start_fuel;
                                }

                            }
                            level_completed = false;
                            airpl.score = 0;
                        }
                    }

                }
                else
                {    
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape) == true){
                        Exit();
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.M) == true) {
                        nachalo_igri = true;
                          gameover1 = false;
                         
                         airpl = new Airplane();
                        enemys = new List<EnemyAirplane>();
                         shoots = new List<Shoot>();
                          shootsenemy = new List<Shoot>();
                       
                          tool = new Tools();
                           vectorgun = new List<Vector2>();
                         vectorgunenemy = new List<Vector2>();
                        vectorplane = new List<Vector2>();
                         messers = new List<Rectangle>(10);
                        fokers = new List<Rectangle>(10);
                       nomers = new List<int>();
                      settings = new SettingsLevel();
                         settings.SetParametrs(level);
                         scoreinterval = settings.scoreinterval;
                         uronarpl = settings.uronairplan;
                         uronc = settings.uroncomp;
                         start_fuel = settings.fuel_start;
                         start_life = settings.life_start;
                         k = settings.k;
                         airpl.life = start_life;
                         airpl.fuel = start_fuel;
                    }
                }
                base.Update(gameTime);
            }
        }

        ///<summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {   
           GraphicsDevice.Clear(Color.Blue);
           if (nachalo_igri == true)
           {
               spriteBatch.Begin();
               GraphicsDevice.Clear(Color.White);
               s = "Game author Ruslan Akhmetov. Goal:survive and score points.";
               s1 = "A,D-Faster and slowly;W,S -DOWN and UP, Space-shooting.";
               s2 = "To start the game, press Enter.";
               gameover2.X = graphics.PreferredBackBufferWidth / 2 - (s.Length / 2) * 10;
               gameover2.Y = graphics.PreferredBackBufferHeight / 2;
               spriteBatch.DrawString(gameover, s, gameover2, Color.DarkGreen);
               gameover2.X = graphics.PreferredBackBufferWidth / 2 - (s1.Length / 2) * 10;
               gameover2.Y = graphics.PreferredBackBufferHeight / 2 + 20;
               spriteBatch.DrawString(gameover, s1, gameover2, Color.DarkGreen);
               gameover2.X = graphics.PreferredBackBufferWidth / 2 - (s2.Length / 2) * 10;
               gameover2.Y = graphics.PreferredBackBufferHeight / 2 + 40;
               spriteBatch.DrawString(gameover, s2, gameover2, Color.DarkGreen);
               spriteBatch.End();
           }
           else
           {
               if (gameover1 == false)
               {
                   if (level_completed == false)
                   {
                       spriteBatch.Begin();
                       spriteBatch.Draw(texture, vector, Color.White);
                       spriteBatch.Draw(texture, vectorend, Color.White);
                       if (vzriv_texture == true) {
                           spriteBatch.Draw(explosion, explose, Color.White);
                       }
                       for (int i = 0; i < shoots.Count; i++)
                       {
                           spriteBatch.Draw(gun, vectorgun[i], Color.White);
                       }
                       for (int i = 0; i < shootsenemy.Count; i++)
                       {
                           spriteBatch.Draw(gun, vectorgunenemy[i], Color.White);
                       }
                       if (airplane.IsDisposed == false) { spriteBatch.Draw(airplane, vector1, Color.White); }
                       for (int i = 0; i < enemys.Count; i++)
                       {
                           if (nomers[i] == 0)
                           {
                               if (messer.IsDisposed == false)
                               {
                                   spriteBatch.Draw(messer, vectorplane[i], Color.White);
                               }
                           }
                           else
                           {
                               if (foker.IsDisposed == false)
                               {
                                   spriteBatch.Draw(foker, vectorplane[i], Color.White);
                               }
                           }
                       }
                       if ((nomer1 == 0))
                       {
                           if ((life_set == false))
                           {
                               spriteBatch.Draw(heart, vector3, Color.White);
                           }
                       }
                       else
                       {
                           if ((fuel_set == false))
                           {
                               spriteBatch.Draw(fuel, vector3, Color.White);
                           }
                       }
                       //создать шрифт                
                       Vector2 v1 = new Vector2(30, 5);
                       Vector2 v2 = new Vector2(60, 5);
                       Vector2 v3 = new Vector2(90, 5);
                       Vector2 v4 = new Vector2(130, 5);
                       Vector2 v5 = new Vector2(160, 5);
                       Vector2 v6 = new Vector2(200, 5);
                       Vector2 v7 = new Vector2(230, 5);
                       Vector2 v8 = new Vector2(290, 5);
                       spriteBatch.Draw(heart1, Vector2.Zero, Color.White);
                       spriteBatch.Draw(speed, v2, Color.White);
                       spriteBatch.DrawString(font, ((int)airpl.life).ToString(), v1, Color.Red);
                       spriteBatch.DrawString(font, ((int)(500 * airpl.speed)).ToString(), v3, Color.Red);
                       spriteBatch.Draw(fuel1, v4, Color.White);
                       spriteBatch.DrawString(font, ((int)(airpl.fuel)).ToString(), v5, Color.Red);
                       spriteBatch.Draw(gold, v6, Color.White);
                       spriteBatch.DrawString(font, ((airpl.score)).ToString(), v7, Color.Red);
                       spriteBatch.DrawString(font, ("L:"+(level)).ToString(), v8, Color.Red);
                       spriteBatch.End();
                   }
                   else
                   {
                       spriteBatch.Begin();
                       GraphicsDevice.Clear(Color.White);
                       s = "Level is completed.Your score is" +" "+ airpl.score;
                       gameover2.X = graphics.PreferredBackBufferWidth / 2 - (s.Length / 2) * 10;
                       gameover2.Y = graphics.PreferredBackBufferHeight / 2;
                       spriteBatch.DrawString(gameover, s, gameover2, Color.DarkGreen);
                       s2 = "To continue, press Enter";
                       gameover2.X = graphics.PreferredBackBufferWidth / 2 - (s2.Length / 2) * 10;
                       gameover2.Y = graphics.PreferredBackBufferHeight / 2 + 40;
                       spriteBatch.DrawString(gameover, s2, gameover2, Color.DarkGreen);
                       exited = true;
                       spriteBatch.End();
                   }
               }
               else
               {
                   spriteBatch.Begin();
                   GraphicsDevice.Clear(Color.White);
                   s = "Airplane was destroyed.Game over";
                   s0 = "Your  score is" + " " + airpl.score +" "+"points";
                   s1 = "If you want to go start the game, press key M";
                   s2 = "If you want to exit the game, press Escape";
                   if (airpl.life <= 0)
                   {
                       s = "Airplane was destroyed.Game over";
                       s0 = "Your  score is" + " " + airpl.score +" "+"points";
                       s1 = "If you want to go start the game, press key  M";
                       s2 = "If you want to exit the game, press Escape";
                   }
                   if (airpl.fuel <= 0)
                   {
                       s = "Fuel is empty.Game over";
                       s0 = "Your  score is" +" "+ airpl.score+" "+"points";
                       s1 = "If you want to go start the game, press key  M";
                       s2 = "If you want to exit the game, press Escape";
                   }
                   gameover2.X = graphics.PreferredBackBufferWidth / 2 - (s.Length / 2) * 10;
                   gameover2.Y = graphics.PreferredBackBufferHeight / 2;
                   spriteBatch.DrawString(gameover, s, gameover2, Color.DarkGreen);
                   gameover2.X = graphics.PreferredBackBufferWidth / 2 - (s0.Length / 2) * 10;
                   gameover2.Y = graphics.PreferredBackBufferHeight / 2+20;
                   spriteBatch.DrawString(gameover, s0, gameover2, Color.DarkGreen);
                   gameover2.X = graphics.PreferredBackBufferWidth / 2 - (s1.Length / 2) * 10;
                   gameover2.Y = graphics.PreferredBackBufferHeight / 2 + 40;
                   spriteBatch.DrawString(gameover, s1, gameover2, Color.DarkGreen);
                   gameover2.X = graphics.PreferredBackBufferWidth / 2 - (s2.Length / 2) * 10;
                   gameover2.Y = graphics.PreferredBackBufferHeight / 2 + 60;
                   spriteBatch.DrawString(gameover, s2, gameover2, Color.DarkGreen);
                   exited = true;
                   spriteBatch.End();
               }
               if (win == true) {
                   spriteBatch.Begin();
                   GraphicsDevice.Clear(Color.White);
                   s = "Congrulations!.You win!";
                   gameover2.X = graphics.PreferredBackBufferWidth / 2 - (s.Length / 2) * 10;
                   gameover2.Y = graphics.PreferredBackBufferHeight / 2;
                   spriteBatch.DrawString(gameover, s, gameover2, Color.DarkGreen);
                   s2 = "To continue, press Enter";
                   gameover2.X = graphics.PreferredBackBufferWidth / 2 - (s2.Length / 2) * 10;
                   gameover2.Y = graphics.PreferredBackBufferHeight / 2 + 40;
                   spriteBatch.End();
               }
               // TODO: Add your drawing code here
               base.Draw(gameTime);
           }
        }
    }
}

