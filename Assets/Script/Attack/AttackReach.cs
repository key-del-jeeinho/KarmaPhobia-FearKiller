using System.Collections;
using UnityEngine;

namespace game
{
    public class AttackReach : MonoBehaviour
    {
        public AttackType atkType;
        public string targetTag;

        private ArrayList targets;
        public ArrayList Targets
        {
            get => targets;
            set {
                if(targets == null)
                    targets = value;
                }
        }

        // Use this for initialization
        void Start()
        {
            targets = new ArrayList();
        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag(targetTag))
            {
                targets.Add(other.gameObject);
                //Debug.Log($"{obj.name} 가 {atkType} 공격범위에 들어왔다!");
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag(targetTag))
            {
                targets.Remove(other.gameObject);
                //Debug.Log($"{obj.name} 가 {atkType} 공격범위에서 나왔다!");
            }
        }
    }
}
