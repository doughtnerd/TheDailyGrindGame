using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grind
{
    public class ExplodingCharacter : MonoBehaviour
    {

        [SerializeField]
        private int damage = 1;

        [SerializeField]
        private float explosionRadius = 5f;

        [SerializeField]
        private float detonationTime = 5f;

        [SerializeField]
        private float blastForce = 20f;

        [SerializeField]
        private LayerMask damageableLayers;

        private Animator anim;

        private bool hasExploded = false;

        // Use this for initialization
        void Start()
        {
            anim = GetComponent<Animator>();
        }

        public void Explode()
        {
            if(!hasExploded)
            {
                hasExploded = true;
                StartCoroutine(ExplodeRoutine());
            }
        }

        IEnumerator ExplodeRoutine()
        {
            anim.SetTrigger("flash");
            yield return new WaitForSeconds(detonationTime);
            anim.SetTrigger("explode");
            Collider2D[] colls = Physics2D.OverlapCircleAll(transform.position, explosionRadius, damageableLayers);
            foreach (Collider2D coll in colls)
            {
                Damageable d = coll.gameObject.GetComponent<Damageable>();
                if(d)
                {
                    d.Damage(damage);
                    Vector2 pushDirection = coll.gameObject.transform.position - transform.position;
                    coll.gameObject.GetComponent<Rigidbody2D>().AddForce(pushDirection.normalized * blastForce, ForceMode2D.Impulse);
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, explosionRadius);
        }
    }

}