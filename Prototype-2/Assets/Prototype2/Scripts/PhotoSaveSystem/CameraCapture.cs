using System;
using System.Collections;
using UnityEngine;

namespace Assets.Prototype2.Scripts.PhotoSaveSystem
{
    public class CameraCapture : MonoBehaviour
    {
        private IPhotoSaver _photoSaver;
        [SerializeField] private Camera _targetCamera;

        private void Awake()
        {
            _photoSaver = new PhotoSaver(); // we can Inject this if we use Dependency Injections
        }

        public async Awaitable<string> TakePhotoAsync()
        {
            _targetCamera.gameObject.SetActive(true);
            string path = await CaptureRoutineAsync();
            _targetCamera.gameObject.SetActive(false);
            return path;
        }

        private async Awaitable<string> CaptureRoutineAsync()
        {
            await Awaitable.EndOfFrameAsync();

            // Set up RenderTexture
            int width = Screen.width;
            int height = Screen.height;

            RenderTexture rt = new RenderTexture(width, height, 24);
            _targetCamera.targetTexture = rt;
            Texture2D photo = new Texture2D(width, height, TextureFormat.RGB24, false);

            _targetCamera.Render();

            // Read pixels
            RenderTexture.active = rt;
            photo.ReadPixels(new Rect(0, 0, width, height), 0, 0);
            photo.Apply();

            // Reset
            _targetCamera.targetTexture = null;
            RenderTexture.active = null;
            Destroy(rt);

            string path = _photoSaver.SavePhoto(photo);
            Destroy(photo); // clean up
            return path;
        }
    }
}