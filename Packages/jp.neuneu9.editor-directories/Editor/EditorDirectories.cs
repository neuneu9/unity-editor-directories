using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEditorInternal;
using System;
using System.Collections.Generic;
#if ADDRESSABLES
using UnityEngine.AddressableAssets;
#endif

namespace neuneu9.EditorDirectories
{
    /// <summary>
    /// Unity エディタ関連フォルダへのアクセス
    /// </summary>
    public class EditorDirectories
    {
        /// <summary>
        /// アプリケーションフォルダを開く
        /// </summary>
        [MenuItem("Directories/Unity Application", priority = 12)]
        private static void OpenApplicationFolder()
        {
            string path = Path.GetFullPath(Path.GetDirectoryName(EditorApplication.applicationPath));

            OpenDirectory(path);
        }

        /// <summary>
        /// プロジェクトフォルダを開く
        /// </summary>
        [MenuItem("Directories/Project", priority = 1)]
        private static void OpenProjectFolder()
        {
            string path = Path.GetFullPath(Path.Combine(Application.dataPath, "../"));

            OpenDirectory(path);
        }

        /// <summary>
        /// Packages フォルダを開く
        /// </summary>
        [MenuItem("Directories/Packages", priority = 1)]
        private static void OpenPackagesFolder()
        {
            string path = Path.GetFullPath(Path.Combine(Application.dataPath, "../Packages/"));

            OpenDirectory(path);
        }

        /// <summary>
        /// AssetStore ダウンロードフォルダを開く
        /// </summary>
        [MenuItem("Directories/AssetStore Download", priority = 1)]
        private static void OpenAssetStoreDownloadFolder()
        {
            // https://docs.unity3d.com/ja/2017.4/Manual/AssetStore.html
#if UNITY_EDITOR_WIN
            string path = Path.GetFullPath(Path.Combine(InternalEditorUtility.unityPreferencesFolder, "../../Asset Store-5.x"));
#elif UNITY_EDITOR_OSX
            string path = Path.GetFullPath(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Library/Unity/Asset Store-5.x"));
#endif

            OpenDirectory(path);
        }

        /// <summary>
        /// ログフォルダを開く
        /// </summary>
        [MenuItem("Directories/Editor Log", priority = 1)]
        private static void OpenEditorLogFolder()
        {
            // https://docs.unity3d.com/jp/460/Manual/LogFiles.html
#if UNITY_EDITOR_WIN
            string path = Path.GetFullPath(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Unity/Editor"));
#elif UNITY_EDITOR_OSX
            string path = Path.GetFullPath(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Library/Logs/Unity"));
#endif

            OpenDirectory(path);
        }

        /// <summary>
        /// 永続データフォルダを開く
        /// </summary>
        [MenuItem("Directories/Persistent Data", priority = 1)]
        private static void OpenPersistentDataFolder()
        {
            string path = Application.persistentDataPath;

            OpenDirectory(path);
        }

#if ADDRESSABLES
        //[MenuItem("Directories/Addressables/", priority = 23)]

        /// <summary>
        /// アセットバンドルフォルダを開く
        /// </summary>
        [MenuItem("Directories/Addressables/Asset Bundle", priority = 23)]
        private static void OpenAddressableAssetBundleFolder()
        {
            string path = new FileInfo(Path.Combine(Application.dataPath, "../", Addressables.RuntimePath)).FullName.Replace("\\", "/");

            OpenDirectory(path);
        }

        /// <summary>
        /// Addressables のカタログ保存先を開く
        /// </summary>
        [MenuItem("Directories/Addressables/Catalogs", priority = 23)]
        private static void OpenAddressableCatalogsFolder()
        {
            string path = Path.Combine(Application.persistentDataPath, "com.unity.addressables").Replace("\\", "/");

            OpenDirectory(path);
        }

        /// <summary>
        /// Addressables のキャッシュフォルダを開く
        /// </summary>
        [MenuItem("Directories/Addressables/Cache", priority = 23)]
        private static void OpenAddressableCacheFolders()
        {
            List<string> paths = new List<string>();
            Caching.GetAllCachePaths(paths);

            if (paths.Count > 1)
            {
                if (EditorUtility.DisplayDialog(nameof(EditorDirectories), $"{paths.Count} cache folders exist.\nDo you want to open them?", "OK", "Cancel"))
                {
                    for (int i = 0; i < paths.Count; i++)
                    {
                        OpenDirectory(paths[i]);
                    }
                }
            }
            else if (paths.Count > 0)
            {
                OpenDirectory(paths[0]);
            }
            else
            {
                EditorUtility.DisplayDialog(nameof(EditorDirectories), "Not found cache folders.", "OK");
            }
        }
#endif


        /// <summary>
        /// ディレクトリを開く
        /// </summary>
        /// <param name="path"></param>
        private static void OpenDirectory(string path)
        {
            if (Directory.Exists(path))
            {
                System.Diagnostics.Process.Start(path);
            }
            else
            {
                EditorUtility.DisplayDialog(nameof(EditorDirectories), "Not found folder.\n" + path, "OK");
            }
        }
    }
}
