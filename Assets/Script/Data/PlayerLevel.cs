using System;
using System.Collections;
using UnityEngine;

namespace game
{
    public class PlayerLevel
    {
        private int level;
        private int requireExp;
        private int curExp;
        private PlayerLevel nextLevel;
        private bool isMaxLevel;

        public PlayerLevel() {
            curExp = 0;
            isMaxLevel = true;
        }
        public PlayerLevel(int level, int requireExp)
            : this()
        {
            this.level = level;
            this.requireExp = requireExp;
        }
        public PlayerLevel(int level, int requireExp, PlayerLevel nextLevel) 
            : this(level, requireExp)
        {
            this.nextLevel = nextLevel;
            isMaxLevel = false;
        }

        public int Level
        {
            get => level;
            set => level = value;
        }

        public int RequireExp
        {
            get => requireExp;
            set => requireExp = value;
        }
        public int CurExp
        {
            get => curExp;
        }

        private void levelUp(int overExp)
        {
            level = nextLevel.level;
            requireExp = nextLevel.requireExp;
            curExp = overExp;
            nextLevel = nextLevel.nextLevel;
        }

        public void addExp(int exp)
        {
            if (curExp + exp > requireExp)
            {
                if (isMaxLevel)
                {
                    curExp = requireExp;
                }
                else
                {
                    int overExp = curExp + exp - requireExp;
                    levelUp(overExp);
                }
            }
            else curExp += exp;
        }
    }
}