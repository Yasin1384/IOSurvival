using UnityEngine;
using Nakama;
using System.Threading.Tasks;
using System;
using UnityEngine.SceneManagement;

public partial class NakamaManager : MonoBehaviour
{
    public static NakamaManager Instance { get; private set; }


    private IClient client;
    private ISession session;
    public ISocket socket;

    private string scheme = "http";
    private string host = "127.0.0.1";
    private int port = 7350;
    private string serverKey = "defaultkey";

    private bool shouldLoadGameScene = false;
    private string targetSceneName = "Game";

    public bool IsOnline { get; private set; } = false;



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        if (shouldLoadGameScene)
        {
            shouldLoadGameScene = false;
            SceneManager.LoadScene(targetSceneName);
        }
    }

    public async Task<bool> InitializeAndConnectAsync()
    {
        try
        {
            client = new Client(scheme, host, port, serverKey);
            session = await client.AuthenticateDeviceAsync(SystemInfo.deviceUniqueIdentifier);
            Debug.Log($"Nakama Session Established! User ID: {session.UserId}");

            socket = client.NewSocket();
            await socket.ConnectAsync(session, appearOnline: true);

            Debug.Log("Nakama Socket Connected successfully!");

            IsOnline = true;
            return true;
        }
        catch (ApiResponseException ex)
        {
            Debug.LogWarning($"Nakama API Error (Offline or Bad Key): {ex.Message}");
            IsOnline = false;
            return false;
        }
        catch (Exception ex)
        {
            Debug.LogWarning($"Connection failed/Offline mode: {ex.Message}");
            IsOnline = false;
            return false;
        }
    }


    private IMatchmakerTicket matchmakerTicket;
    public string CurrentMatchId { get; private set; }

    public async void FindMatch(string sceneToLoad)
    {
        if (socket == null)
        {
            Debug.LogError("dis_Soket!");
            return;
        }

        Debug.Log("search player...");

        socket.ReceivedMatchmakerMatched += OnMatchmakerMatched;

        matchmakerTicket = await socket.AddMatchmakerAsync("*", 2, 2);
    }

    private async void OnMatchmakerMatched(IMatchmakerMatched matched)
    {
        Debug.Log("find player...");

        socket.ReceivedMatchmakerMatched -= OnMatchmakerMatched;

        var match = await socket.JoinMatchAsync(matched);
        CurrentMatchId = match.Id;

        Debug.Log($"{CurrentMatchId}");

        shouldLoadGameScene = true;
    }
}
