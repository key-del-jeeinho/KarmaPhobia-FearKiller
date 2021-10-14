using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace game
{
    public struct ControllKey
    {
        public ControllKey(
            KeyCode sprint,
            KeyCode jump,
            KeyCode moveLeft,
            KeyCode moveRight,
            KeyCode slash,
            KeyCode lunge)
        {
            Sprint = sprint;
            Jump = jump;
            MoveLeft = moveLeft;
            MoveRight = moveRight;
            Slash = slash;
            Lunge = lunge;
        }

        public KeyCode Sprint { get; set; }
        public KeyCode Jump { get; set; }
        public KeyCode MoveLeft { get; set; }
        public KeyCode MoveRight { get; set; }
        public KeyCode Slash { get; set; }
        public KeyCode Lunge { get; set; }
    }
}
