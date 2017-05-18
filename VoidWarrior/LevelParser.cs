using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using VoidWarrior.View;

namespace VoidWarrior
{
    class LevelParser
    {
        private static Func<float, float> GetLambda(string key)
        {
            switch (key)
            {
                case "0":
                    return x => 0;
                case "Sin":
                    return x => (float)Math.Sin(x / 100) * 100;
                default:
                    return x => 0;
            }
        }

        public static Level Parse(string name, ResourcePool res)
        {
            var levelTemplate = JsonConvert.DeserializeObject<LevelT>(File.ReadAllText(name));

            float translateX = 1 / (100f / Globals.SCREEN_WIDTH);
            float translateY = 1 / (100f / Globals.SCREEN_HEIGHT);

            // Add all enemies to the list
            var enemies = new List<Enemy>();
            levelTemplate.Enemies.ForEach(enemyT => {
                var func = GetLambda(enemyT.Path.Func);
                enemies.Add(new Enemy(
                    new Sprite(res.GetTexture(enemyT.Texture), 
                        enemyT.Rectangle.X * translateX - enemyT.Rectangle.X / 2,
                        enemyT.Rectangle.Y * translateY - enemyT.Rectangle.Y / 2,
                        enemyT.Rectangle.W, 
                        enemyT.Rectangle.H, 
                    new Color(enemyT.Color.R / 255f, enemyT.Color.G / 255f, enemyT.Color.B / 255f)),
                    res.GetTexture("pixel"), enemyT.Health, enemyT.Value, 
                    new Path(enemyT.Path.Speed, enemyT.Path.Angle, func), 
                    enemyT.Delay));
            });

            // create player
            var player = new Player(
                res,
                levelTemplate.Player.Rectangle.X * translateX - levelTemplate.Player.Rectangle.W / 2,
                levelTemplate.Player.Rectangle.Y * translateY - levelTemplate.Player.Rectangle.H / 2,
                levelTemplate.Player.Rectangle.W,
                levelTemplate.Player.Rectangle.H,
                new Color(levelTemplate.Player.Color.R / 255f, levelTemplate.Player.Color.G / 255f, levelTemplate.Player.Color.B / 255f)
            );

            /*
            // Add all enemies to the list
            var enemies = new List<Enemy>();
            levelTemplate.Enemies.ForEach(enemyT => {
                var func = GetLambda(enemyT.Path.Func);
                enemies.Add(new Enemy(
                    new Sprite(res.GetTexture(enemyT.Texture),
                        enemyT.Rectangle.X,
                        enemyT.Rectangle.Y,
                        enemyT.Rectangle.W,
                        enemyT.Rectangle.H,
                    new Color(enemyT.Color.R / 255f, enemyT.Color.G / 255f, enemyT.Color.B / 255f)),
                    res.GetTexture("pixel"), enemyT.Health, enemyT.Value,
                    new Path(enemyT.Path.Speed, enemyT.Path.Angle, func),
                    enemyT.Delay));
            });

            // create player
            var player = new Player(
                res,
                levelTemplate.Player.Rectangle.X,
                levelTemplate.Player.Rectangle.Y,
                levelTemplate.Player.Rectangle.W,
                levelTemplate.Player.Rectangle.H,
                new Color(levelTemplate.Player.Color.R / 255f, levelTemplate.Player.Color.G / 255f, levelTemplate.Player.Color.B / 255f)
            );
            */

            return new Level(enemies, player, res.GetFont(levelTemplate.UiFont), res);
        }

        private class LevelT
        {
            public PlayerT Player { get; set; }
            public List<EnemyT> Enemies { get; set; }
            public string UiFont { get; set; }
        }
        private class PlayerT
        {
            public RectangleT Rectangle { get; set; }
            public ColorT Color { get; set; }
        }
        private class EnemyT
        {
            public string Texture { get; set; }
            public RectangleT Rectangle { get; set; }
            public ColorT Color { get; set; }
            public int Health { get; set; }
            public int Value { get; set; }
            public float Delay { get; set; }
            public PathT Path { get; set; }
        }
        private class PathT
        {
            public float Speed { get; set; }
            public float Angle { get; set; }
            public string Func { get; set; }
        }
        private class RectangleT
        {
            public float X { get; set; }
            public float Y { get; set; }
            public float W { get; set; }
            public float H { get; set; }
        }
        private class ColorT
        {
            public int R { get; set; }
            public int G { get; set; }
            public int B { get; set; }
        }
    }
}
