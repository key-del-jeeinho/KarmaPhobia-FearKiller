using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace game
{
    public class Entity : MonoBehaviour
    {
        public int genId;
        private long id;
        public long Id { get; }

        // Start is called before the first frame update
        void Start()
        {
            id = generateId();
            Debug.Log($"new Entity is created. [gen : {genId}, id : {id}]");
        }

        private long generateId()
        {
            return DateTimeOffset.Now.ToUnixTimeMilliseconds();
        }

        // Update is called once per frame
        void Update()
        {

        }

        enum GenId : int
        {
            PLAYER_KNIGHT,
            MONSTER_UNDEAD_LANCER
        }
    }

}