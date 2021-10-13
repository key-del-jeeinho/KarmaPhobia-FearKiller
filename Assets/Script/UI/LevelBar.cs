using System.Collections;
using UnityEngine;

namespace game
{
    public class LevelBar : Bar
    {
        private int requireExp;
        private int curExp;

        public int RequireExp
        { 
            get { return requireExp; }
            set { requireExp = value; }
        }
        public int CurExp
        {
            get { return curExp; }
            set { curExp = value; }
        }

        // Use this for initialization
        void Start()
        {
            ChildStart();
            curExp = requireExp;
        }

        // Update is called once per frame
        void Update()
        {
            ChildUpdate();
            Progress = ((float)curExp / requireExp) * 100;
            //Debug.Log($"health ratio : {healthRatio}, progress : {Progress}");
        }
    }
}