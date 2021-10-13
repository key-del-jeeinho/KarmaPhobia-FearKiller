using System.Collections;
using System.Collections.Generic;

namespace game
{
    public struct Stat
    {
        public Stat(int hp, int atk, float atkSpeed, int def, float speed, float jumpPower, int mana)
        {
            Hp = hp;
            Atk = atk;
            AtkSpeed = atkSpeed;
            Def = def;
            Speed = speed;
            JumpPower = jumpPower;
            Mana = mana;
        }

        public int Hp { get; }
        public int Atk { get; }
        public float AtkSpeed { get; }
        public int Def { get; }
        public float Speed { get; }
        public float JumpPower { get; set; }
        public int Mana { get; set; }

        public override string ToString() => $"hp={Hp}, atk={Atk}, atk_speed={AtkSpeed}, def={Def}, speed={Speed}";
    }
}