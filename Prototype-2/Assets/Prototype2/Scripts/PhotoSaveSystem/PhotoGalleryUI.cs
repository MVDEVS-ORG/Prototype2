using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Prototype2.Scripts.PhotoSaveSystem
{
    public class PhotoGalleryUI : MonoBehaviour
    {
        [SerializeField] private GameObject _photoPrefab;
        [SerializeField] Transform _photoContainer;

        private IPhotoSaver _photoSaver;


        public void DisplayAllPhotos()
        {
            foreach (Transform child in _photoContainer)
            {
                Destroy(child.gameObject);
            }
            _photoSaver = new PhotoSaver();
            List<string> paths = _photoSaver.GetSavedPhotoPaths();
            Debug.LogError($"{paths.Count != 0}");
            foreach (string path in paths)
            {
                Texture2D photo = _photoSaver.LoadPhoto(path);
                if(photo != null)
                {
                    GameObject instance = Instantiate(_photoPrefab, _photoContainer);
                    instance.GetComponent<RawImage>().texture = photo;
                }
            }
        }
    }
}