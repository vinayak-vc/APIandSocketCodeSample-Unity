using System.Collections;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

using ViitorCloud.MapDiya;
using ViitorCloud.API;
using ViitorCloud.API.StandardTemplates;

namespace ViitorCloud.Hologram.Stage.Viewer {

    public class WebRequestManager : MonoBehaviour {
        public static WebRequestManager Instance;

        private void CreateSingleton() {
            if (Instance == null) {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            } else {
                DestroyImmediate(gameObject);
            }
        }

        private void Awake() {
            CreateSingleton();
        }

        public void CheckAlignmentsAPI(UnityAction<APIResponse<CheckAlignment>> callbackOnSuccess,
            UnityAction<string> callbackOnFail) {
            ServerCommunication.Instance.SendRequestGet(string.Format(Mining.Simulator.Constants.API.CheckAlignment), callbackOnSuccess, callbackOnFail);
        }
    }
}