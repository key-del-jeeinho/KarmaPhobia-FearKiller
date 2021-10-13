using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace game
{
    public class Player : Actor
    {
        public Vector3 spawnpoint;

        protected PlayerInput inputs;
        protected ControllKey controlls;
        protected PlayerLevel level;

        private Animator animator;
        private Rigidbody2D rigid2D;
        private BoxCollider2D col2D;

        private HealthBar hpBar;
        private LevelBar lvBar;

        // Start is called before the first frame update
        void Start()
        {
            transform.position = spawnpoint;

            inputs = new PlayerInput();

            initData();
            initUnityComponents();
            initBars();
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
        }

        private void initUnityComponents()
        {
            animator = GetComponent<Animator>();
            rigid2D = GetComponent<Rigidbody2D>();
            col2D = GetComponent<BoxCollider2D>();
        }

        private void initBars()
        {
            hpBar = GetComponent<HealthBar>();
            lvBar = GetComponent<LevelBar>();

            hpBar.MaxHealth = stat.Hp;
            hpBar.CurHealth = hpBar.MaxHealth;

            lvBar.RequireExp = level.RequireExp;
            lvBar.CurExp = 0;
        }

        // Update is called once per frame
        void Update()
        {
            //Box Cast 를 통한 점핑 여부 판정
            RaycastHit2D raycastHit = Physics2D.BoxCast(col2D.bounds.center, col2D.bounds.size, 0f, Vector2.down, 0.02f, LayerMask.GetMask("Ground"));
            if (raycastHit.collider != null)
                animator.SetBool("isJump", false);
            else animator.SetBool("isJump", true);

            //조작키 입력 처리
            //좌우이동
            if (Input.GetKey(controlls.MoveLeft))
                inputs.MoveLeft = true;
            else if (Input.GetKey(controlls.MoveRight))
                inputs.MoveLeft = true;
            else animator.SetBool("isMove", false);
            //점프
            if (Input.GetKey(controlls.Jump))
                inputs.MoveLeft = true;
            //공격
            if (Input.GetKey(controlls.Attack))
                inputs.MoveLeft = true;
        }

        void FixedUpdate()
        {
            if (inputs.MoveRight)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                animator.SetBool("isMove", true);
                transform.Translate(Vector3.right * Time.deltaTime * stat.Speed);
                inputs.MoveRight = false;
            }
            if (inputs.MoveLeft)
            {
                transform.localScale = new Vector3(1, 1, 1);
                animator.SetBool("isMove", true);
                transform.Translate(Vector3.left * Time.deltaTime * stat.Speed);
                inputs.MoveLeft = false;
            }
            if (inputs.Jump)
            {
                inputs.Jump = false;
                rigid2D.velocity = new Vector2(rigid2D.velocity.x, stat.JumpPower);
            }
        }
    }
}