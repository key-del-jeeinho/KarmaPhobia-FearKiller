using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace game
{
    public abstract class Actor : Entity
    {
        protected Stat stat;
        public Stat Stat
        {
            get { return stat; }
            set { stat = value; }
        }

        protected abstract void Die();

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