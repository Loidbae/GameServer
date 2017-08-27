﻿using System;
using LeagueSandbox.GameServer.Core.Logic;
using LeagueSandbox.GameServer.Logic.Maps;
using System.Collections.Generic;
using System.Diagnostics;
using LeagueSandbox.GameServer.Logic.GameObjects.Other.Pathfinder;

namespace LeagueSandbox.GameServer.Logic.GameObjects
{
    public class CollisionHandler
    {
        private Game _game = Program.ResolveDependency<Game>();

        private List<GameObject> _objects = new List<GameObject>();

        public void AddObject(GameObject obj)
        {
            _objects.Add(obj);
        }

        public void RemoveObject(GameObject obj)
        {
            _objects.Remove(obj);
        }

        public void Update()
        {
            foreach (var obj in _objects)
            {
                if (obj is BaseTurret || obj is Inhibitor || obj is Nexus)
                {
                    continue;
                }

                if (!_game.Map.NavGrid.IsWalkable(obj.X, obj.Y))
                {
                    obj.onCollision(null);
                }

                foreach (var obj2 in _objects)
                {
                    if (obj == obj2)
                    {
                        continue;
                    }

                    if (obj.IsCollidingWith(obj2))
                    {
                        obj.onCollision(obj2);
                    }
                }
            }
        }
    }
}
