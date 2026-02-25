using UnityEngine;

using BestHTTP.SocketIO3;

using System;

using BestHTTP.SocketIO3.Events;

using ViitorCloud.Hologram.Stage.Viewer;

namespace ViitorCloud.MapDiya {

    public class Lobby {

        [Header("Socket")]
        public static SocketManager socketManager;

        public static Socket socket;
        //https://diya.focalat.com/user-diya/get-screen-list

        public const string SOCKET_EVENT_CONNECT = "connect";
        public const string SOCKET_EVENT_RECONNECT = "reconnect";
        public const string SOCKET_EVENT_RECONNECTING = "reconnecting";
        public const string SOCKET_EVENT_RECONNECT_ATTEMPT = "reconnect_attempt";
        public const string SOCKET_EVENT_RECONNECT_FAILED = "reconnect_failed";
        public const string SOCKET_EVENT_DISCONNECT = "disconnect";

        public static void ConnectToSocket() {
            TimeSpan miliSecForReconnect = TimeSpan.FromMilliseconds(1000);

            SocketOptions options = new SocketOptions();
            options.ReconnectionAttempts = 100;
            options.AutoConnect = true;
            options.ReconnectionDelay = miliSecForReconnect;
            options.Timeout = TimeSpan.FromMilliseconds(10000);
            options.Reconnection = true;

            BestHTTP.HTTPManager.Setup();
            socketManager = new SocketManager(new Uri(Mining.Simulator.Constants.API.SocketBaseURL + "/socket.io/"), options);
            BestHTTP.HTTPManager.Setup();
            string Url = Mining.Simulator.Constants.API.SocketBaseURL + "/socket.io/";
            Debug.Log("url =>" + Url);

            //socket = socketManager.GetSocket("/Map-diya-wall-channel");

            socketManager.Socket.On<ConnectResponse>(SocketIOEventTypes.Connect, OnConnected);
            socketManager.Socket.On(SOCKET_EVENT_RECONNECT, OnReConnect);
            socketManager.Socket.On(SOCKET_EVENT_RECONNECTING, OnReConnecting);
            socketManager.Socket.On(SOCKET_EVENT_RECONNECT_ATTEMPT, OnReConnectAttempt);
            socketManager.Socket.On<ConnectResponse>(SOCKET_EVENT_RECONNECT_FAILED, OnReConnectFailed);
            socketManager.Socket.On(SocketIOEventTypes.Disconnect, OnDisconnect);
        }

        private static void OnConnected(ConnectResponse resp) {
            Debug.Log("-- Connected --");
            SocketManagerVC.Instance.AddListenerOfSocketEvent();
            SocketManagerVC.Instance.OnConnect?.Invoke();
        }

        private static void OnReConnect() {
            Debug.Log("-- Re-Connect...");
        }

        private static void OnReConnecting() {
            Debug.Log("-- Re-Connecting...");
        }

        private static void OnReConnectAttempt() {
            Debug.Log("-- Re-Connect Attempt...");
        }

        private static void OnReConnectFailed(ConnectResponse resp) {
            Debug.Log("-- Re-ConnectFailed...");
        }

        private static void OnDisconnect() {
#if !CUENECT_WEBGL
            Debug.Log("-- Disconnected --");
            SocketManagerVC.Instance.RemoveListenerOfSocketEvent();
            SocketManagerVC.Instance.OnDisconnect?.Invoke();
#endif
        }
    }
}