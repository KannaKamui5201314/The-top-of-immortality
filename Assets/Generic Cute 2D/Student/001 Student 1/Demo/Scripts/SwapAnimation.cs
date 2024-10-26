using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Student1
{
    public class SwapAnimation : MonoBehaviour
    {
        public Animator[] characters;
        public Text animationNameText;
        private int animationIndex = 0;

        // Start is called before the first frame update
        void Start()
        {
            /*
            animators = new Animator[characters.Length];
            for (int i=0; i < characters.Length; i++)
            {
                animators[i] = characters[i].GetComponent<Animator>();
            }
            */
            animationNameText.text = characters[0].runtimeAnimatorController.animationClips[animationIndex].name;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                PlayNextAnimation();
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                PlayPreviousAnimation();
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                ReplayAnimation();
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                Debug.Log("D");
                characters[0].Play("run", 0, 0f);
            }
            else if (Input.GetKeyUp(KeyCode.D))
            {
                characters[0].Play("idle", 0, 0f);
            }
            else if(Input.GetMouseButtonDown(0))
            {
                characters[0].Play("blocking hit", 0, 0f);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                characters[0].Play("idle", 0, 0f);
                //characters[0].Play("blocking hit", 0, 0f);
            }

        }

        public void PlayNextAnimation()
        {
            animationIndex++;
            if (animationIndex >= characters[0].runtimeAnimatorController.animationClips.Length)
            {
                animationIndex = 0;
            }
            Debug.Log("characters.Length=" + characters.Length);
            for (int i = 0; i < characters.Length; i++)
            {
                Debug.Log("animationIndex=" + animationIndex);
                string animationName = characters[i].runtimeAnimatorController.animationClips[animationIndex].name;
                characters[i].Play(animationName, 0, 0f);
            }
            animationNameText.text = characters[0].runtimeAnimatorController.animationClips[animationIndex].name;
        }
        public void PlayPreviousAnimation()
        {
            animationIndex--;
            if (animationIndex < 0)
            {
                animationIndex = characters[0].runtimeAnimatorController.animationClips.Length - 1;
            }
            for (int i = 0; i < characters.Length; i++)
            {

                string animationName = characters[i].runtimeAnimatorController.animationClips[animationIndex].name;
                characters[i].Play(animationName, 0, 0f);
            }
            animationNameText.text = characters[0].runtimeAnimatorController.animationClips[animationIndex].name;
        }
        public void ReplayAnimation()
        {
            for (int i = 0; i < characters.Length; i++)
            {
                string animationName = characters[i].runtimeAnimatorController.animationClips[animationIndex].name;
                characters[i].Play(animationName, 0, 0f);
            }
        }
    }
}