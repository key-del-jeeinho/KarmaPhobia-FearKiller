using System.Collections;
using UnityEngine;

namespace game
{
    public class HealthBar : Bar
    {
        private int maxHealth;
        private int curHealth;
        private float healthRatio; //부드러운 체력바 음직임을 위해 실제 체력비율을 저장

        public int MaxHealth { 
            get { return maxHealth; }
            set { maxHealth = value; }
        }
        public int CurHealth
        {
            get { return curHealth; }
            set { curHealth = value; }
        }

        // Use this for initialization
        void Start()
        {
            ChildStart();
            curHealth = maxHealth;
        }

        // Update is called once per frame
        void Update()
        {
            ChildUpdate();
            healthRatio = ((float)curHealth / maxHealth) * 100;
            //Debug.Log($"health ratio : {healthRatio}, progress : {Progress}");

            //Smooth Bar moving
            if (Progress != healthRatio)
            {
                int modifier = (int)Mathf.Abs(healthRatio - Progress) / 10;
                modifier = modifier == 0 ? 1 : modifier;
                if (Progress > healthRatio) Progress -= modifier;
                else Progress += modifier;
            }
        }
    }
}