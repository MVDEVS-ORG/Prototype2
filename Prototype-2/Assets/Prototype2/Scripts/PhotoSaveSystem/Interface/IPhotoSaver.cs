using System.Collections.Generic;
using UnityEngine;

namespace Assets.Prototype2.Scripts.PhotoSaveSystem
{
    public interface IPhotoSaver
    {
        string SavePhoto(Texture2D photo , string name);
        List<string> GetSavedPhotoPaths();
        Texture2D LoadPhoto(string path);
    }
}