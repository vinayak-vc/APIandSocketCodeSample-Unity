using System;

using BestHTTP.SocketIO3;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

using ViitorCloud.API.StandardTemplates;
using ViitorCloud.MapDiya;

namespace ViitorCloud.Hologram.Stage.Viewer {
    public class SocketManagerVC : MonoBehaviour {

        public API.Constants.API.Server server;

        private const string MaskEvent = "mask_event";
        private const string BallFrame = "ball_frame";

        public static SocketManagerVC Instance { get; private set; }

        public UnityEvent OnDisconnect { get; private set; }

        public UnityEvent OnConnect { get; private set; }

        private void Awake() {
            Instance = this;
        }

        private void Start() {
            StartSocket();
        }

        private void StartSocket() {
            Lobby.ConnectToSocket();
        }


        public void ExitGame() {
            Application.Quit();
        }

        public void AddListenerOfSocketEvent() {
            Socket socket = Lobby.socketManager.Socket;

            socket.On<TableData>(MaskEvent, MaskEventRaised);
            socket.On<FrameData>(BallFrame, BallFrameEventRaised);
        }

        public void RemoveListenerOfSocketEvent() {
            if (Lobby.socketManager != null) {
                Socket socket = Lobby.socketManager.Socket;

                socket.Off(MaskEvent);
                socket.Off(BallFrame);
            }
        }
        private void BallFrameEventRaised(FrameData obj) {

        }
        private void MaskEventRaised(TableData obj) {

        }

        private void CallCheckAlignmentsAPI() {
            WebRequestManager.Instance.CheckAlignmentsAPI(CheckAlignmentsSuccess, CheckAlignmentsFail);
        }
        private void CheckAlignmentsSuccess(APIResponse<CheckAlignment> arg0) {

        }
        private void CheckAlignmentsFail(string arg0) {

        }


        private void ReloadScene() {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private void OnDisable() {
            RemoveListenerOfSocketEvent();
        }
    }
}