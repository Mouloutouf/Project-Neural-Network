using UnityEngine;

namespace Es.InkPainter.Sample
{
	[RequireComponent(typeof(Collider), typeof(MeshRenderer))]
	public class CollisionPainter : MonoBehaviour
	{
        InkCanvas ink;

        [SerializeField]
        private Brush brush = null;

        [SerializeField]
        private int wait = 3;

        private int waitCount;

        public void Awake()
        {
            GetComponent<MeshRenderer>().material.color = brush.Color;
            ink = FindObjectOfType<InkCanvas>();

        }

        public void FixedUpdate()
        {
            ++waitCount;
        }

        void Update()
        {
            ink.Paint(brush, transform.position);

        }

        public void OnCollisionStay(Collision collision)
        {
            if (waitCount < wait)
                return;
            waitCount = 0;


        }
    }
}