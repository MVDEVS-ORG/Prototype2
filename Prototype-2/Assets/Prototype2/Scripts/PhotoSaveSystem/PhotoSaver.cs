using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Assets.Prototype2.Scripts.PhotoSaveSystem
{
    public class PhotoSaver : IPhotoSaver
    {
        private string folderPath;

        public PhotoSaver()
        {
            folderPath = Path.Combine(Application.persistentDataPath, "Photos");
            if(!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);
        }
        public List<string> GetSavedPhotoPaths()
        {
            if (!Directory.Exists(folderPath))
            {
                Debug.LogWarning("Photos folder not found: " + folderPath);
                return new List<string>();
            }

            string[] files = Directory.GetFiles(folderPath, "*.png", SearchOption.TopDirectoryOnly);
            Debug.Log("Found photo files: " + files.Length);
            return new List<string>(files);
        }

        public Texture2D LoadPhoto(string path)
        {
            if(!File.Exists(path)) return null;

            byte[] bytes = File.ReadAllBytes(path);
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(bytes);
            return texture;
        }

        public string SavePhoto(Texture2D photo)
        {
            byte[] bytes = photo.EncodeToPNG();
            string fileName = "Photo_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png";
            string savedFilePath = Path.Combine(folderPath, fileName);
            Debug.LogError($"photo saved at : {savedFilePath}");
            File.WriteAllBytes(savedFilePath, bytes);
            return savedFilePath;
        }
    }
}