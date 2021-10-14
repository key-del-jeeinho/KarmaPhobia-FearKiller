using System.Collections;
using UnityEngine;

namespace game
{
    public class MonsterProps : MonoBehaviour
    {
        private bool isStatInit;
        private Stat stat;
        public int hp;
        public int atk;
        public float atkSpeed;
        public int def;
        public float speed;
        public float jumpPower;
        public int mana;

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