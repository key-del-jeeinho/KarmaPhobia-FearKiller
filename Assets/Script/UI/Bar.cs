using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace game {
    public class Bar : MonoBehaviour
    {
        public GameObject prfBar;
        public Canvas canvas;

        public float barYPrefix;

        private GameObject bar;
        private RectTransform barTransform;
        private Image curBar;

        private float progress; //percent
        public float Progress { get { return progress; } set { progress = value; } }

        // Start is called before the first frame update
        void Start()
        {
            bar = Instantiate(prfBar, canvas.transform);
            barTransform = bar.GetComponent<RectTransform>();
            curBar = barTransform.transform.GetChild(0).GetComponent<Image>();
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 barPos = Camera.main.WorldToScreenPoint(
                new Vector3(transform.position.x, transform.position.y + barYPrefix));
            barTransform.position = barPos;
            curBar.fillAmount = progress / 100;
            //Debug.Log($"curBar is set to {curBar.fillAmount}");
        }

        protected void ChildUpdate() { Update(); }
        protected void ChildStart() { Start(); }
    }
}
