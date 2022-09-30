/*
 * SPDX-License-Identifier: (Apache-2.0 OR GPL-2.0-only)
 *
 * Copyright 2022 Sony Semiconductor Solutions Corporation.
 *
 */

using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace TofArARSamples.SimpleARFoundation
{
    /// <summary>
    /// Switch modeling functions depending on the environment.
    /// IOS : ARFoundation
    /// androif : TofAr
    /// </summary>
    public class ModelingPlatformController : MonoBehaviour
    {
        private ARMeshManager arMeshManager;

        void Awake()
        {
            arMeshManager = FindObjectOfType<ARMeshManager>();

#if UNITY_EDITOR
            Debug.Log("UNITY_EDITOR environment: Switch Modleing");
            arMeshManager.enabled = true;
            TofAr.V0.Modeling.TofArModelingManager.Instance.autoStart = false;
            TofAr.V0.Modeling.TofArModelingManager.Instance.StopStream();

#elif UNITY_IOS
            Debug.Log("UNITY_IOS:Switch Modeling");
            arMeshManager.enabled = true;
            TofAr.V0.Modeling.TofArModelingManager.Instance.autoStart = false;
            TofAr.V0.Modeling.TofArModelingManager.Instance.StopStream();

#elif UNITY_ANDROID
            Debug.Log("UNITY_ANDROID environment:Switch Modeling");
            arMeshManager.enabled = false;
            TofAr.V0.Modeling.TofArModelingManager.Instance.autoStart = true;
              
            
#endif
        }
    }
}
