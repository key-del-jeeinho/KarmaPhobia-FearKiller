using System;
using UnityEngine;

namespace game
{
    public class Player : Actor
    {
        public Vector3 spawnpoint;
        public AttackReach lungeReach;
        public AttackReach slashReach;

        protected PlayerInput inputs;
        protected ControllKey controlls;
        protected PlayerLevel level;

        private Animator animator;
        private Rigidbody2D rigid2D;
        private PolygonCollider2D col2D;

        private HealthBar hpBar;
        private LevelBar lvBar;
        private StaminaBar staminaBar;
        private int healthPoint;
        public int Hp {
            get => healthPoint;
            set
            {
                healthPoint = value;
                hpBar.CurHealth = healthPoint;
                if (healthPoint <= 0)
                    Die();
                else Debug.Log($"{genId}:{Id} 의 현재체력 : {hpBar.CurHealth}");
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Monster"), true); //몬스터와의 충돌로 밀리는것을 방지한다
            transform.position = spawnpoint;

            inputs = new PlayerInput();

            random = new System.Random();

            initUnityComponents();
            initData();
            initBars();
            InvokeRepeating("restoreStamina", 0, 1);
        }

        private void initData()
        {
            PlayerProps props = GetComponent<PlayerProps>();
            controlls = props.getControllData();
            stat = props.getStatData();
            level = new PlayerLevel(1, 2,
                new PlayerLevel(2, 6,
                new PlayerLevel(3, 10,
                new PlayerLevel(4, 10,
                new PlayerLevel(5, 20,
                new PlayerLevel(6, 36,
                new PlayerLevel(7, 56,
                new PlayerLevel(8, 80,
                new PlayerLevel(9, 110,
                new PlayerLevel(10, 154,
                new PlayerLevel(11, 255,
                new PlayerLevel(12, 512)
                )))))))))));
            animator.SetFloat("attackSpeed", stat.AtkSpeed);
        }

        private void initUnityComponents()
        {
            animator = GetComponent<Animator>();
            rigid2D = GetComponent<Rigidbody2D>();
            col2D = GetComponent<PolygonCollider2D>();
        }

        private void initBars()
        {
            hpBar = GetComponent<HealthBar>();
            lvBar = GetComponent<LevelBar>();
            staminaBar = GetComponent<StaminaBar>();

            hpBar.MaxHealth = stat.Hp;
            Hp = hpBar.MaxHealth; //현재 체력을 100% 로 설정한다
            hpBar.CurHealth = Hp;

            lvBar.RequireExp = level.RequireExp;
            lvBar.CurExp = 0;
        }

        // Update is called once per frame
        void Update()
        {
            lvBar.CurExp = level.CurExp;
            lvBar.RequireExp = level.RequireExp;
            Debug.Log($"BarExp = {lvBar.CurExp} RealExp : {level.CurExp}");
            //Debug.Log(staminaBar.CurStamina);
            animator.SetInteger("Stamina", (int)(staminaBar.CurStamina * 100));
            //Box Cast 를 통한 점핑 여부 판정
            RaycastHit2D raycastHit = Physics2D.BoxCast(col2D.bounds.center, col2D.bounds.size, 0f, Vector2.down, 0.02f, LayerMask.GetMask("Platform"));
            if (raycastHit.collider != null)
                animator.SetBool("isJump", false);
            else animator.SetBool("isJump", true);

            //조작키 입력 처리
            //좌우이동
            if (Input.GetKey(controlls.MoveLeft))
                inputs.MoveLeft = true;
            else if (Input.GetKey(controlls.MoveRight))
                inputs.MoveRight = true;
            else animator.SetBool("isMove", false);
            //달리기
            if (Input.GetKey(controlls.Sprint))
                inputs.Run = true;
            else animator.SetBool("isSprint", false);
            //점프 
            if (Input.GetKey(controlls.Jump) && !isDuringJump())
                inputs.Jump = true;
            //공격
            if (Input.GetKey(controlls.Slash) && !isDuringAttack())
            {
                animator.SetTrigger("Slash");
                float animDelay = getAnimationDelay("Slash");
                Invoke("Slash", animDelay);
            }
            if (Input.GetKey(controlls.Lunge) && !isDuringAttack())
            {
                animator.SetTrigger("Lunge");
                float animDelay = getAnimationDelay("Lunge");
                Invoke("Lunge", animDelay);
            }
        }

        private void restoreStamina()
        {
            staminaBar.CurStamina += 4; //초당 4씩 회복
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

        private System.Random random;

        private void Attack(GameObject obj, int dmg, float critDmgMul, float critPer)
        {
            if(random.NextDouble() <= critPer)
            {
                dmg = (int)(dmg * critDmgMul);
            }
            obj.SendMessage("Attacked", dmg);
        }

        void Attacked(int dmg)
        {
            if (((int)(new System.Random().NextDouble() * 100)) < stat.Speed)
            {
                Debug.Log("회피하였습니다! (슈욱, 슉, 슈슉)");
                return; //회피율
            }
            Debug.Log($"공격당헀습니다! {dmg - stat.Def}");
            Hp -= (dmg - stat.Def);
        }


        private bool isDuringJump()
        {
            return
                animator.GetCurrentAnimatorStateInfo(0).IsName("Jump") || animator.GetBool("isJump");
        }

        public bool isDuringAttack()
        {
            return
                animator.GetCurrentAnimatorStateInfo(0).IsName("Lunge") |
                animator.GetCurrentAnimatorStateInfo(0).IsName("Slash");
        }

        public bool isDuringAttack(AttackType atkType)
        {
            switch (atkType)
            {
                case AttackType.Lunge:
                    return
                        animator.GetCurrentAnimatorStateInfo(0).IsName("Lunge");
                case AttackType.Slash:
                    return
                        animator.GetCurrentAnimatorStateInfo(0).IsName("Slash");
                default:
                    throw new NotImplementedException();
            }
        }

        private void Slash()
        {

            foreach (GameObject obj in slashReach.Targets)
            {
                int dmg = stat.Atk * 2;
                float critDmgMul = 2F;
                float critPer = 0.1F;
                if (isDuringJump())
                {
                    dmg += 30;
                    critPer -= 0.05F;
                }
                Attack(obj, dmg, critDmgMul, critPer);
            }
        }

        private void Lunge()
        {
            foreach (GameObject obj in lungeReach.Targets)
            {
                if (isDuringJump()) return;
                Attack(obj, stat.Atk, 5F, 0.3F);
            }

        }

        void FixedUpdate()
        {
            int runStamina = 20, runInitStamina = 50;
            if (inputs.MoveRight)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                animator.SetBool("isMove", true);

                float speed = stat.Speed;

                if (inputs.Run)
                {
                    if (staminaBar.CurStamina > runInitStamina)
                    {
                        staminaBar.CurStamina -= Time.deltaTime * runStamina;
                        animator.SetBool("isSprint", true);
                        speed *= 2;
                    }
                    else if (staminaBar.CurStamina < Time.deltaTime * runStamina)
                    {
                        animator.SetBool("isSprint", false);
                    }
                    else if (animator.GetBool("isSprint"))
                    {
                        speed *= 2;
                        staminaBar.CurStamina -= Time.deltaTime * runStamina;
                    }
                }

                transform.Translate(Vector3.right * Time.deltaTime * speed);

                inputs.MoveRight = false;
                inputs.Run = false;
            }
            if (inputs.MoveLeft)
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                animator.SetBool("isMove", true);

                float speed = stat.Speed;


                if (inputs.Run)
                {
                    if (staminaBar.CurStamina > runInitStamina)
                    {
                        staminaBar.CurStamina -= Time.deltaTime * runStamina;
                        animator.SetBool("isSprint", true);
                        speed *= 2;
                    }
                    else if (staminaBar.CurStamina < Time.deltaTime * runStamina)
                    {
                        animator.SetBool("isSprint", false);
                    }
                    else if (animator.GetBool("isSprint"))
                    {
                        speed *= 2;
                        staminaBar.CurStamina -= Time.deltaTime * runStamina;
                    }
                }

                transform.Translate(Vector3.left * Time.deltaTime * speed);

                inputs.MoveLeft = false;
                inputs.Run = false;
            }
            int jumpStemina = 10;
            if (inputs.Jump && staminaBar.CurStamina >= jumpStemina)
            {
                staminaBar.CurStamina -= jumpStemina;
                Debug.Log("Jumped!");
                rigid2D.velocity = new Vector2(rigid2D.velocity.x, stat.JumpPower);
                inputs.Jump = false;
            }
        }

        protected override void Die()
        {
            animator.SetTrigger("Death");            // die 애니메이션 실행
            //GetComponent<MonsterAI>().enabled = false;    // 추적 비활성화
            GetComponent<Collider2D>().enabled = false; // 충돌체 비활성화
            Destroy(GetComponent<Rigidbody2D>());       // 중력 비활성화
            Destroy(gameObject, 3);                     // 3초후 제거
            Destroy(hpBar.GameObject, 3);               // 3초후 체력바 제거
            Destroy(lvBar.GameObject, 3);               // 3초후 체력바 제거
            Destroy(staminaBar.GameObject, 3);               // 3초후 체력바 제거
            enabled = false;
            //Destroy(nameTagTransform.gameObject, 3);
        }

        void Killed(GameObject victim)
        {
            if (victim.tag.Equals("Monster"))
                level.addExp(victim.GetComponent<Monster>().exp);
        }
    }
}