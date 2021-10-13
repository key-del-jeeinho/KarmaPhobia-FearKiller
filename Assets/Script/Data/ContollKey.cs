using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace game
{
    public struct ControllKey
    {
        public ControllKey(
            KeyCode jump,
            KeyCode moveLeft,
            KeyCode moveRight,
            KeyCode attack)
        {
            Jump = jump;
            MoveLeft = moveLeft;
            MoveRight = moveRight;
            Attack = attack;
        }

        public KeyCode Jump { get; set; }
        public KeyCode MoveLeft { get; set; }
        public KeyCode MoveRight { get; set; }
        public KeyCode Attack { get; set; }
    }
}
