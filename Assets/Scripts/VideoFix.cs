using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoFix : MonoBehaviour
{
    [SerializeField]
    private VideoPlayer videoPlayer;

    void Start() {
        videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, "video3.mp4");
    }

    public void PlayVideo() {
        videoPlayer.Play();
    }
}
