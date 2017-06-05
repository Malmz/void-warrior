using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using VoidWarrior.View;

namespace VoidWarrior
{
    class Parser
    {
        private static Func<float, float> GetLambda(FuncJ func)
        {
            switch (func.Type)
            {
                case "0":
                    return x => 0;
                case "Sin":
                    return x => (float)Math.Sin(x / func.Param1) * func.Param2;
                case "X2":
                    return x => -(float)Math.Pow(x, 2) * func.Param2 + func.Param1 * x;
                default:
                    return x => 0;
            }
        }

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

        public static Level ParseLevel(string path, ResourcePool res)
        {
            var levelTemplate = JsonConvert.DeserializeObject<LevelJ>(File.ReadAllText(path));

            float translateX = 1 / (100f / Globals.SCREEN_WIDTH);
            float translateY = 1 / (100f / Globals.SCREEN_HEIGHT);

            // Add all enemies to the list
            var enemies = new List<Enemy>();
            if (levelTemplate.Enemies != null)
            {
                levelTemplate.Enemies.ForEach(enemyT => {
                    var func = GetLambda(enemyT.Path.Func);
                    enemies.Add(new Enemy(
                        sprite: new Sprite(res.GetTexture(enemyT.Texture),
                            X: enemyT.Rectangle.X * translateX - enemyT.Rectangle.X / 2,
                            Y: enemyT.Rectangle.Y * translateY - enemyT.Rectangle.Y / 2,
                            W: enemyT.Rectangle.W,
                            H: enemyT.Rectangle.H,
                            color: new Color(enemyT.Color.R / 255f, enemyT.Color.G / 255f, enemyT.Color.B / 255f)),
                        healthBarTexture: res.GetTexture("pixel"),
                        health: enemyT.Health,
                        value: enemyT.Value,
                        path: new Path(enemyT.Path.Speed, enemyT.Path.Angle, func),
                        delay: enemyT.Delay));
                });
            }

            if (levelTemplate.Templates != null)
            {
                levelTemplate.Templates.ForEach(enemyT => {
                    var tmp = ParseEnemyTemplate(res, enemyT);
                    tmp.X *= translateX;
                    enemies.Add(tmp);
                });
            }

            // create player
            var player = new Player(
                res,
                levelTemplate.Player.Rectangle.X * translateX - levelTemplate.Player.Rectangle.W / 2,
                levelTemplate.Player.Rectangle.Y * translateY - levelTemplate.Player.Rectangle.H / 2,
                levelTemplate.Player.Rectangle.W,
                levelTemplate.Player.Rectangle.H,
                new Color(levelTemplate.Player.Color.R / 255f, levelTemplate.Player.Color.G / 255f, levelTemplate.Player.Color.B / 255f)
            );
            return new Level(enemies, player, res.GetFont(levelTemplate.UiFont), res);
        }

        private static Dictionary<string, EnemyTemplateJ> TemplateCache = new Dictionary<string, EnemyTemplateJ>();

        private static Enemy ParseEnemyTemplate(ResourcePool res, EnemyInstanceJ instanceInfo)
        {
            EnemyTemplateJ enemyTemplate;
            if (TemplateCache.ContainsKey(instanceInfo.Path))
            {
                enemyTemplate = TemplateCache[instanceInfo.Path];
            }
            else
            {
                enemyTemplate = JsonConvert.DeserializeObject<EnemyTemplateJ>(File.ReadAllText(instanceInfo.Path));
            }
            var texture = res.GetTexture(enemyTemplate.Texture);
            var func = GetLambda(enemyTemplate.Func);

            Enemy enemy = new Enemy(
                sprite: new Sprite(texture,
                        X: instanceInfo.X,
                        Y: -enemyTemplate.H,
                        W: enemyTemplate.W,
                        H: enemyTemplate.H,
                        color: new Color(enemyTemplate.Color.R / 255f, enemyTemplate.Color.G / 255f, enemyTemplate.Color.B / 255f)),
                    healthBarTexture: res.GetTexture("pixel"),
                    health: enemyTemplate.Health,
                    value: enemyTemplate.Value,
                    path: new Path(enemyTemplate.Speed, instanceInfo.Angle, func),
                    delay: instanceInfo.Delay
            );
            return enemy;
        }
        
        private class LevelJ
        {
            public PlayerJ Player { get; set; }
            public List<EnemyJ> Enemies { get; set; }
            public List<EnemyInstanceJ> Templates { get; set; }
            public string UiFont { get; set; }
        }
        private class PlayerJ
        {
            public RectangleJ Rectangle { get; set; }
            public ColorJ Color { get; set; }
        }
        private class EnemyJ
        {
            public string Texture { get; set; }
            public RectangleJ Rectangle { get; set; }
            public ColorJ Color { get; set; }
            public int Health { get; set; }
            public int Value { get; set; }
            public float Delay { get; set; }
            public PathJ Path { get; set; }
        }
        private class PathJ
        {
            public float Speed { get; set; }
            public float Angle { get; set; }
            public string Func { get; set; }
        }
        private class RectangleJ
        {
            public float X { get; set; }
            public float Y { get; set; }
            public float W { get; set; }
            public float H { get; set; }
        }
        private class ColorJ
        {
            public int R { get; set; }
            public int G { get; set; }
            public int B { get; set; }
        }
        private class EnemyInstanceJ
        {
            public string Path { get; set; }
            public float X { get; set; }
            public float Delay { get; set; }
            public float Angle { get; set; }
        }
        private class EnemyTemplateJ
        {
            public string Texture { get; set; }
            public float W { get; set; }
            public float H { get; set; }
            public ColorJ Color { get; set; }
            public int Health { get; set; }
            public int Value { get; set; }
            public float Speed { get; set; }
            public FuncJ Func { get; set; }
        }
        private class FuncJ
        {
            public string Type { get; set; }
            public float Param1 { get; set; }
            public float Param2 { get; set; }
        }
    }
}
