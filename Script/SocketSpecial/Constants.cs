using System.IO;

using Modules.Utility;

using ViitorCloud.API;

using static Modules.Utility.Utility;

using static ViitorCloud.API.Constants.API;

namespace ViitorCloud.Mining.Simulator {
    public abstract class Constants {
        public abstract class API {
            public static AppConfig appConfig = new AppConfig();

            private const string APIDevelopmentBaseURL = "http://192.168.1.89:8000/check-alignment/";
            private const string APIProductionBaseURL = "http://192.168.1.89:8000/check-alignment/";
            
            private const string SocketDevelopmentBaseURL = "http://192.168.1.89:8000/check-alignment/";
            private const string SocketProductionBaseURL = "http://192.168.1.89:8000/check-alignment/";

            public static string CheckAlignment {
                get {
                    return APIBaseURL + "check-alignment";
                }
            }
            
            public static string SocketBaseURL {
                get {
                    try {

                        return ServerCommunication.Instance.server switch {
                            Server.Live => SocketProductionBaseURL,
                            Server.Development => SocketDevelopmentBaseURL,
                            Server.FromConfig => appConfig.APIBaseURL,
                            _ => APIProductionBaseURL
                        };
                    } catch {
                        return APIProductionBaseURL;
                    }

                }
            }

            private static string APIBaseURL {
                get {
                    try {

                        return ServerCommunication.Instance.server switch {
                            Server.Live => APIProductionBaseURL,
                            Server.Development => APIDevelopmentBaseURL,
                            Server.FromConfig => appConfig.APIBaseURL,
                            _ => APIProductionBaseURL
                        };
                    } catch {
                        return APIProductionBaseURL;
                    }

                }
            }

            public class AppConfig {
                public bool offline;
                public string APIBaseURL;
            }

            public static void LoadFromConfig() {
                appConfig = null;
                string configPath = UnityEngine.Application.dataPath + "/config.json";

                try {
                    if (System.IO.File.Exists(configPath)) {
                        appConfig = UnityEngine.JsonUtility.FromJson<AppConfig>(EncryptDecrypt.Decrypt(System.IO.File.ReadAllText(configPath)));
                    } else {
                        GenerateJson();
#if UNITY_EDITOR
                        GenerateJsonsForEditor();
#endif
                    }
                } catch (System.Exception e) {
                    appConfig = new AppConfig {
                        APIBaseURL = APIProductionBaseURL,
                        offline = false
                    };
                    GenerateJson();
                    LogError(e.Message);
                }
            }
            private static void GenerateJson() {
                string configPath = UnityEngine.Application.dataPath + "/config.json";
                if (File.Exists(configPath)) {
                    File.Delete(configPath);
                }
                AppConfig config = new AppConfig {
                    APIBaseURL = APIProductionBaseURL,
                    offline = false
                };
                WriteAllText(configPath, config);
                LoadFromConfig();
            }

            private static void GenerateJsonsForEditor() {
                string configPath = UnityEngine.Application.dataPath + "/config-prod.json";

                AppConfig config = new AppConfig {
                    APIBaseURL = APIProductionBaseURL,
                    offline = false
                };
                WriteAllText(configPath, config);

                configPath = UnityEngine.Application.dataPath + "/config-staging.json";

                config = new AppConfig {
                    APIBaseURL = APIDevelopmentBaseURL,
                    offline = false
                };
                WriteAllText(configPath, config);
            }


            private static void WriteAllText(string configPath, AppConfig config) {
                File.WriteAllText(configPath, EncryptDecrypt.Encrypt(UnityEngine.JsonUtility.ToJson(config)));
            }
        }
    }
}