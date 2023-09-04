using TofAr.V0.Hand;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TofArARSamples.HandPoseEmoji
{
    public class HpeAppProgressManager : MonoBehaviour
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
        private Image emojiPanel;

        private PoseIndex currentHandPose;

        public void PoseReflection(PoseIndex poseLeft, PoseIndex poseRight)
        {
            if (poseLeft!=PoseIndex.None)
            {
                currentHandPose = poseLeft;
            }
            else if(poseRight != PoseIndex.None)
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
            if (currentHandPose==PoseIndex.None)
            {
                emojiPanel.sprite = questionMarkSprite;
            }
            else
            {
                Debug.Log("CURRENT DETECTED RIGHT HAND POSE::: " + currentHandPose.ToString());

                if (currentHandPose == PoseIndex.Pistol)
                {
                    Debug.Log("Pose Detected:: "+ currentHandPose.ToString());
                    emojiPanel.sprite = fingerGunSprite;

                }
                if (currentHandPose == PoseIndex.Fist)
                {
                    Debug.Log("Pose Detected:: " + currentHandPose.ToString());
                    emojiPanel.sprite = fistSprite;

                }
                if (currentHandPose == PoseIndex.Shot)
                {
                    Debug.Log("Pose Detected:: " + currentHandPose.ToString());
                    emojiPanel.sprite = indexFingerSprite;

                }
                if (currentHandPose == PoseIndex.OK)
                {
                    Debug.Log("Pose Detected:: " + currentHandPose.ToString());
                    emojiPanel.sprite = okSprite;

                }
                if (currentHandPose == PoseIndex.OpenPalm)
                {
                    Debug.Log("Pose Detected:: " + currentHandPose.ToString());
                    emojiPanel.sprite = openPalmSprite;

                }
                if (currentHandPose == PoseIndex.Peace)
                {
                    Debug.Log("Pose Detected:: " + currentHandPose.ToString());
                    emojiPanel.sprite = peaceSprite;

                }
                if (currentHandPose == PoseIndex.Fox)
                {
                    Debug.Log("Pose Detected:: " + currentHandPose.ToString());
                    emojiPanel.sprite = rockSignSprite;

                }
                if (currentHandPose == PoseIndex.ThumbUp)
                {
                    Debug.Log("Pose Detected:: " + currentHandPose.ToString());
                    emojiPanel.sprite = thumbsUpSprite;

                }
                if (currentHandPose == PoseIndex.ThreeFingers)
                {
                    Debug.Log("Pose Detected:: " + currentHandPose.ToString());
                    emojiPanel.sprite = threeFingersSprite;

                }
            }
        }
    }
}
