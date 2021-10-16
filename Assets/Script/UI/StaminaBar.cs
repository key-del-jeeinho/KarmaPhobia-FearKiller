using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace game
{
    public class StaminaBar : Bar
    {
        private const int MAX_STAMINA = 100;
        private float stamina;

        public float CurStamina
        {
            get => stamina;
            set {
                if (value > 100)
                    stamina = 100;
                else if (value < 0) stamina = 0;
                else stamina = value;
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            ChildStart();
            stamina = MAX_STAMINA;
        }

        // Update is called once per frame
        void Update()
        {
            ChildUpdate();
            float staminaRatio = stamina / MAX_STAMINA * 100;
            //Debug.Log($"stamina ratio : {staminaRatio}, progress : {Progress}");

            //Smooth Bar moving
            if (Progress != staminaRatio)
            {
                int modifier = (int)Mathf.Abs(staminaRatio - Progress) / 10;
                modifier = modifier == 0 ? 1 : modifier;
                if (Mathf.Abs(Progress - staminaRatio) < modifier) Progress = staminaRatio;
                else if (Progress > staminaRatio) Progress -= modifier;
                else Progress += modifier;
            }
        }
    }
}
