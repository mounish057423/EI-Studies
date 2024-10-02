// Target Interface
public interface IMediaPlayer
{
    void Play(string fileName);
}

// Adaptee
public class VideoPlayer
{
    public void PlayVideo(string fileName)
    {
        Console.WriteLine($"Playing video: {fileName}");
    }
}

// Adapter
public class MediaPlayerAdapter : IMediaPlayer
{
    private readonly VideoPlayer _videoPlayer;

    public MediaPlayerAdapter(VideoPlayer videoPlayer)
    {
        _videoPlayer = videoPlayer;
    }

    public void Play(string fileName)
    {
        _videoPlayer.PlayVideo(fileName);
    }
}

// Client
public class Program
{
    public static void Main()
    {
        VideoPlayer videoPlayer = new VideoPlayer();
        IMediaPlayer mediaPlayer = new MediaPlayerAdapter(videoPlayer);

        mediaPlayer.Play("movie.mp4");
    }
}
