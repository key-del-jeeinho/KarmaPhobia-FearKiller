using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace game
{
    public class Monster : Actor
    {
        private Animator animator;

        private HealthBar hpBar;
        private int healthPoint;
        private int hp
        {
            get => healthPoint;
            set
            {
                healthPoint = value;
                hpBar.CurHealth = healthPoint;
                if (healthPoint <= 0) 
                    Die();
                else Debug.Log($"현재체력 : {hpBar.CurHealth}");
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            initUnityComponents();
            initData();
            initBars();
        }

        private void initUnityComponents()
        {
            animator = GetComponent<Animator>();
        }

        private void initData()
        {
            MonsterProps props = GetComponent<MonsterProps>();
            stat = props.getStatData();
        }

        private void initBars()
        {
            hpBar = GetComponent<HealthBar>();

            hpBar.MaxHealth = stat.Hp;
            hp = hpBar.MaxHealth; //현재 체력을 100% 로 설정한다
            hpBar.CurHealth = hp;
        }

        // Update is called once per frame
        void Update()
        {

        }

        void Attacked(int dmg)
        {
            if (((int)(new System.Random().NextDouble() * 100)) < stat.Speed)
            { 
                Debug.Log("회피하였습니다! (슈욱, 슉, 슈슉)");
                return; //회피율
            }
            Debug.Log($"공격당헀습니다! {dmg - stat.Def}");
            hp -= (dmg - stat.Def);
        }

        protected override void Die()
        {
            animator.SetTrigger("Death");            // die 애니메이션 실행
            //GetComponent<MonsterAI>().enabled = false;    // 추적 비활성화
            GetComponent<Collider2D>().enabled = false; // 충돌체 비활성화
            Destroy(GetComponent<Rigidbody2D>());       // 중력 비활성화
            Destroy(gameObject, 3);                     // 3초후 제거
            Destroy(hpBar.GameObject, 3);               // 3초후 체력바 제거
            //Destroy(nameTagTransform.gameObject, 3);
        }
    }
}
