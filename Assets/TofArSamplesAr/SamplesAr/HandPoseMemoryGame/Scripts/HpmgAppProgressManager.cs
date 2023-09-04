using TofAr.V0.Hand;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

namespace TofArARSamples.HandPoseMemoryGame
{
    public class HpmgAppProgressManager : MonoBehaviour
    {
        [SerializeField]
        private Sprite fingerGunSprite;

        [SerializeField]
        private Sprite fistSprite;

        [SerializeField]
        private Sprite indexFingerSprite;

        [SerializeField]
        private Sprite okSprite;

        [SerializeField]
        private Sprite openPalmSprite;

        [SerializeField]
        private Sprite peaceSprite;

        [SerializeField]
        private Sprite rockSignSprite;

        [SerializeField]
        private Sprite thumbsUpSprite;

        [SerializeField]
        private Sprite questionMarkSprite;

        [SerializeField]
        private Sprite threeFingersSprite;

        [SerializeField]
        private Sprite getReadySprite;

        [SerializeField]
        private Sprite startSprite;

        [SerializeField]
        private Sprite gameOverSprite;

        [SerializeField]
        private Image promptPanel;

        [SerializeField]
        private TextMeshProUGUI userScorePanel;

        [SerializeField]
        private Transform playAgainMessage;

        private int userScore = 0;

        private PoseIndex currentHandPose;

        private enum AppState
        {
            idle,
            prompt,
            game,
            win,
            loss
        }

        private AppState currentAppState = AppState.idle;

        private int currentNumberOfPrompts = 3;

        private PoseIndex currentPromptToEvaluate;

        private bool waitingOnUserPose = false;

        private float userCorrectPoseHoldTime = 0f;
        private float userWrongPoseHoldTime = 0f;
        private float playAgainPosePoseHoldTime = 0f;
        private float playAgainThreshold = 1f;

        private IEnumerator promptStarted;

        private Queue<PoseIndex> promptQueue = new Queue<PoseIndex>();

        private PoseIndex[] allowedPrompts = {
            PoseIndex.Pistol,
            PoseIndex.Fist,
            PoseIndex.Shot,
            PoseIndex.OK,
            PoseIndex.OpenPalm,
            PoseIndex.Peace,
            PoseIndex.Fox,
            PoseIndex.ThumbUp,
            PoseIndex.ThreeFingers
        };

        public void PoseReflection(PoseIndex poseLeft, PoseIndex poseRight)
        {
            if (poseLeft != PoseIndex.None)
            {
                currentHandPose = poseLeft;
            }
            else if (poseRight != PoseIndex.None)
            {
                currentHandPose = poseRight;
            }
            else
            {
                currentHandPose = PoseIndex.None;
            }
        }

        private void Update()
        {
            if (currentAppState == AppState.idle || currentAppState==AppState.win || currentAppState == AppState.loss)
            {
                //show Thumbs Up to play
                if (currentHandPose == PoseIndex.ThumbUp)
                {
                    playAgainPosePoseHoldTime += Time.deltaTime;
                    if (playAgainPosePoseHoldTime >= playAgainThreshold)
                    {
                        currentAppState = AppState.prompt;
                    }
                }
            }

            if (currentAppState == AppState.prompt)
            {
                //display "Current Level number of" prompts
                if (promptStarted==null)
                {
                    Debug.Log("Time to start the Prompt coroutine");
                    promptStarted = GeneratePrompts();
                    StartCoroutine(promptStarted);
                }
            }

            if (currentAppState == AppState.game)
            {
                //if (currentHandPose == PoseIndex.ThumbUp)
                //{
                //    currentAppState = AppState.prompt;
                //}

                if (!waitingOnUserPose)
                {
                    if (promptQueue.Count==0)
                    {
                        currentAppState = AppState.win;
                        userCorrectPoseHoldTime = 0f;
                        waitingOnUserPose = false;
                        Debug.Log("User WON::::::::::");
                    }
                    else
                    {
                        currentPromptToEvaluate = promptQueue.Dequeue();
                        Debug.Log("currentPromptToEvaluate :: "+ currentPromptToEvaluate.ToString());
                        waitingOnUserPose = true;
                    }
                }
                else
                {
                    if (currentHandPose==currentPromptToEvaluate)
                    {
                        userCorrectPoseHoldTime += Time.deltaTime;
                    }
                    else
                    {
                        userWrongPoseHoldTime += Time.deltaTime;
                    }
                    if (userCorrectPoseHoldTime>1f)
                    {
                        DisplayPrompts(currentPromptToEvaluate);
                        userScore += 1;
                        ResetControlVariable();
                    }
                    if (userWrongPoseHoldTime > 5f)
                    {
                        currentAppState = AppState.loss;
                        userScore = 0;
                        promptQueue.Clear();
                        ResetControlVariable();
                        Debug.Log("User LOSS::::::::::");
                    }
                }
            }

            UpdateUI();
        }

        private void ResetControlVariable()
        {
            userCorrectPoseHoldTime = 0f;
            userWrongPoseHoldTime = 0f;
            playAgainPosePoseHoldTime = 0f;
            waitingOnUserPose = false;
        }

        private void UpdateUI()
        {
            userScorePanel.text = "Score: " + userScore;
            if (currentAppState==AppState.loss)
            {
                promptPanel.sprite = gameOverSprite;
                playAgainMessage.gameObject.SetActive(true);
            }
            else if (currentAppState == AppState.win)
            {
                promptPanel.sprite = questionMarkSprite;
                playAgainMessage.gameObject.SetActive(true);
            }
            else
            {
                playAgainMessage.gameObject.SetActive(false);
            }
        }

        private IEnumerator GeneratePrompts()
        {
            promptPanel.sprite = getReadySprite;

            PoseIndex prevPrompt=PoseIndex.None;

            int promptShowed = 0;

            while (promptShowed < currentNumberOfPrompts)
            {

                yield return new WaitForSeconds(1);

                PoseIndex prompt = allowedPrompts[Random.Range(0, allowedPrompts.Length)];
                
                while (prompt==prevPrompt)
                {
                    prompt = allowedPrompts[Random.Range(0, allowedPrompts.Length)];
                }

                prevPrompt = prompt;

                DisplayPrompts(prompt);
                promptQueue.Enqueue(prompt);
                promptShowed += 1;

                Debug.Log("Prompt Done:: " + promptShowed);
                Debug.Log("Now Showing Prompt:: "+ prompt.ToString());
            }
            yield return new WaitForSeconds(1);

            promptPanel.sprite = startSprite;
            currentAppState = AppState.game;
            Debug.Log("PROMPT STATGE DONE:::");
            promptStarted = null;
        }

        private void DisplayPrompts(PoseIndex temp)
        {
            if (temp == PoseIndex.Pistol)
            {
                promptPanel.sprite = fingerGunSprite;

            }
            if (temp == PoseIndex.Fist)
            {
                promptPanel.sprite = fistSprite;

            }
            if (temp == PoseIndex.Shot)
            {
                promptPanel.sprite = indexFingerSprite;

            }
            if (temp == PoseIndex.OK)
            {
                promptPanel.sprite = okSprite;

            }
            if (temp == PoseIndex.OpenPalm)
            {
                promptPanel.sprite = openPalmSprite;

            }
            if (temp == PoseIndex.Peace)
            {
                promptPanel.sprite = peaceSprite;

            }
            if (temp == PoseIndex.Fox)
            {
                promptPanel.sprite = rockSignSprite;

            }
            if (temp == PoseIndex.ThumbUp)
            {
                promptPanel.sprite = thumbsUpSprite;

            }
            if (temp == PoseIndex.ThreeFingers)
            {
                promptPanel.sprite = threeFingersSprite;

            }
        }

    }
}
