using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Prototype2.Scripts.PhotoSaveSystem
{
    public class PhotoGalleryUI : MonoBehaviour
    {
        private IPhotoSaver _photoSaver;

        private void Start()
        {
            _photoSaver = new PhotoSaver();
        }

        public Texture GetPhoto(string path)
        {
            Texture2D photo = _photoSaver.LoadPhoto(path);
            if (photo != null)
            {
                return photo;
            }
            return null;
        }

    }
}