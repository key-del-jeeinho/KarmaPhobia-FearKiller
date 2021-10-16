using System;
using System.Collections;
using UnityEngine;

namespace game
{
    public class MonsterStandardAI : MonoBehaviour
    {
        public float visionRange;
        public AttackReach slashReach;

        private Animator animator;
        private Monster monster;
        private GameObject player;

        private void Start()
        {
            random = new System.Random();

            animator = GetComponent<Animator>();
            monster = GetComponent<Monster>();

            player = GameObject.FindGameObjectWithTag("Player");
        }

        void Update()
        {
            if (player == null) return;
            float distanse = Vector2.Distance(transform.position, player.transform.position);
            if(distanse <= visionRange)
            {
                if (player.transform.position.x - transform.position.x < 0)
                    transform.localScale = new Vector3(-1, 1, 1);
                else transform.localScale = new Vector3(1, 1, 1);

                if (slashReach.Targets.Count > 0 && !isDuringAttack())
                {
                    animator.SetBool("isMove", false);
                    animator.SetTrigger("Slash");
                    float animDelay = getAnimationDelay("Slash");
                    Invoke("Slash", animDelay);
                } else
                {
                    animator.SetBool("isMove", true);
                    transform.position = Vector2.Lerp(transform.position, player.transform.position, Time.deltaTime * monster.Stat.Speed);
                }
            }else
            {
                animator.SetBool("isMove", false);
            }
        }
        public bool isDuringAttack()
        {
            return
                animator.GetCurrentAnimatorStateInfo(0).IsName("Slash");
        }

        private void Slash()
        {

            foreach (GameObject obj in slashReach.Targets)
            {
                int dmg = monster.Stat.Atk;
                float critDmgMul = 2F;
                float critPer = 0.1F;
                Attack(obj, dmg, critDmgMul, critPer);
            }
        }


        private System.Random random;
        private void Attack(GameObject obj, int dmg, float critDmgMul, float critPer)
        {
            if (random.NextDouble() <= critPer)
            {
                dmg = (int)(dmg * critDmgMul);
            }
            obj.SendMessage("Attacked", dmg);
        }


        private float getAnimationDelay(string animName)
        {
            RuntimeAnimatorController rac = animator.runtimeAnimatorController;

            foreach (AnimationClip clip in rac.animationClips)
            {
                if (clip.name == animName) return clip.length;
            }

            return 0;
        }
    }
}