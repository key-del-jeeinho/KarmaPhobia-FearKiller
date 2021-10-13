using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace game
{
    public class Actor : Entity
    {
        protected Stat stat;
        public Stat Stat
        {
            get { return stat; }
            set { stat = value; }
        }

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
        }
    }
}