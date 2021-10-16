using System.Collections;
using UnityEngine;

namespace game
{
    public class BackgroundManager : MonoBehaviour
    {
        public Transform target;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            transform.position = new Vector3(target.position.x, target.position.y, 0);
        }
    }
}