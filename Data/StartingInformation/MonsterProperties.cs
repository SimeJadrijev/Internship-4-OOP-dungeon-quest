﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Data.StartingInformation
{
    public enum MonsterCategory
    {
        Goblin,
        Brute,
        Witch
    }
    public enum MonsterDamage
    {
        Goblin = 15,
        Brute = 25,
        Witch = 35
    }
    public enum MonsterHealthPoints
    {
        Goblin = 50,
        Brute = 80,
        Witch = 60
    }
    public enum MonsterExperience
    {
        Goblin = 15,
        Brute = 30,
        Witch = 45
    }
}