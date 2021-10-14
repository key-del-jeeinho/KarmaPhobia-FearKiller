using System.Collections;
using UnityEngine;

namespace game
{
    public class AttackReach : MonoBehaviour
    {
        public AttackType atkType;

        private ArrayList monsters;
        public ArrayList Monsters
        {
            get => monsters;
            set => monsters = value;
        }

        private Collider2D reach;
        private Player player;
        // Use this for initialization
        void Start()
        {
            monsters = new ArrayList();
            reach = GetComponent<Collider2D>();
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Monster"))
            {
                Monster monster = other.gameObject.GetComponent<Monster>();
                monsters.Add(other.gameObject);
                Debug.Log($"{monster.genId} 가 {atkType} 공격범위에 들어왔다!");
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Monster"))
            {
                Monster monster = other.gameObject.GetComponent<Monster>();
                monsters.Remove(other.gameObject);
                Debug.Log($"{monster.genId} 가 {atkType} 공격범위에서 나왔다!");
            }
        }
    }
}
