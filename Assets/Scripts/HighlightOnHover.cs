using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Grind
{
    public class HighlightOnHover : MonoBehaviour
    {
        [SerializeField]
        private int sceneToLoad;

        private SpriteRenderer render;
        private Collider2D coll;
        private bool selectionMade = false;

        void Start()
        {
            render = GetComponent<SpriteRenderer>();
            coll = GetComponent<Collider2D>();
        }

        private void OnMouseEnter()
        {
            if (!selectionMade)
            {
                render.color = new Color(1, 1, 1, 1);
            }
        }

        private void OnMouseExit()
        {
            if (!selectionMade)
            {
                render.color = new Color(1, 1, 1, .5f);
            }
        }

        private void OnMouseDown()
        {
            if (!selectionMade)
            {
                selectionMade = true;
                StartCoroutine(FlashRoutine(new Color(1, 1, 1, .5f), .1f, 5));
            }
        }

        IEnumerator FlashRoutine(Color color, float interval, int repetitions)
        {
            Debug.Log("Running flash routine");
            for (int i = 0; i < repetitions; i++)
            {
                render.color = color;
                yield return new WaitForSeconds(interval);
                render.color = Color.white;
                yield return new WaitForSeconds(interval);
            }
            SceneLoader.Instance.LoadScene(this.sceneToLoad);
        }
    }
}
