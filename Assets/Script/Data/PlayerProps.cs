using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace game
{
    public class PlayerProps : MonoBehaviour
    {
        private bool isControllInit;
        private ControllKey controlls;
        public KeyCode jump;
        public KeyCode sprint;
        public KeyCode moveLeft;
        public KeyCode moveRight;
        public KeyCode slash;
        public KeyCode lunge;

        private bool isStatInit;
        private Stat stat;
        public int hp;
        public int atk;
        public float atkSpeed;
        public int def;
        public float speed;
        public float jumpPower;
        public int mana;

        public ControllKey getControllData()
        {
            if (!isControllInit)
            {
                controlls = new ControllKey(sprint, jump, moveLeft, moveRight, slash, lunge);
                isControllInit = true;
            }
            return controlls;
        }

        public Stat getStatData()
        {
            if (!isStatInit)
            {
                stat = new Stat(hp, atk, atkSpeed, def, speed, jumpPower, mana);
                isStatInit = true;
            }
            return stat;
        }
    }
}