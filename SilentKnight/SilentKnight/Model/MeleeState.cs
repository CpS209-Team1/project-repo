﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    class MeleeState : IState
    {
        StateMachine statemachine;
        public MeleeState() { }
        public void Update()
        {
            Random rand = new Random();
            foreach (Enemy i in World.Instance.Entities)
            {
                int randNum = rand.Next(0, 10);
                double enemyDistance = Math.Sqrt(Math.Pow(Player.Instance.PlayerLoc.X - i.EnemyLoc.X, 2) + Math.Pow(Player.Instance.PlayerLoc.Y - i.EnemyLoc.Y, 2));
                if (enemyDistance < 50 && World.Instance.CheatMode == false)
                {
                    if (Player.Instance.PlayerDirection == Direction.Left && i.EnemyLoc.X < Player.Instance.PlayerLoc.X)
                    {
                        i.RemoveEnemyHealth(2);
                        EnemyMove.Instance.Hit(i);
                    }
                    else if (Player.Instance.PlayerDirection == Direction.Right && i.EnemyLoc.X > Player.Instance.PlayerLoc.X)
                    {
                        i.RemoveEnemyHealth(2);
                        EnemyMove.Instance.Hit(i);
                    }
                    else if (Player.Instance.PlayerDirection == Direction.Up && i.EnemyLoc.Y < Player.Instance.PlayerLoc.Y)
                    {

                        i.RemoveEnemyHealth(2);
                        EnemyMove.Instance.Hit(i);
                    }
                    else if (Player.Instance.PlayerDirection == Direction.Down && i.EnemyLoc.Y > Player.Instance.PlayerLoc.Y)
                    {
                        i.RemoveEnemyHealth(2);
                        EnemyMove.Instance.Hit(i);
                    }
                    Player.Instance.PlayerCoolDown = 25;

                }
                else if (enemyDistance < 50 && World.Instance.CheatMode == true)
                {
                    i.RemoveEnemyHealth(i.Health);
                    EnemyMove.Instance.Hit(i);
                }
                if (i.Health <= 0)
                {
                    World.Instance.DeadEnemy.Add(i);// Once an enemy is removed from the Entities list, it is added to DeadEnemy for removal from the canvas
                    Player.Instance.PlayerScore += 1;
                    if (randNum == 7)
                    {
                        randNum = rand.Next(1, 5);
                        Player.Instance.Health += randNum;
                    }
                }
            }

            foreach (Enemy i in World.Instance.DeadEnemy)
            {
                i.KillEnemy();
            }
        }
        public void HandleInput(string data)
        {
            if(data == "melee" && Player.Instance.PlayerCoolDown == 0)
            {
                Update();
            }
            else if (data == "ranged" && Player.Instance.PlayerCoolDown == 0)
            {
                Player.Instance.PlayerState.Change("ranged");
            }
        }

        public void Change()
        {

        }
        public void Exit()
        {

        }

    }
}
