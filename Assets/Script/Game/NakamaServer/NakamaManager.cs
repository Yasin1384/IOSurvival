using UnityEngine;
using Nakama;
using System.Threading.Tasks;
using System;
using UnityEngine.SceneManagement;
using System.Linq;

public partial class NakamaManager : MonoBehaviour
{
    public static NakamaManager Instance { get; private set; }


    private IMatchmakerTicket matchmakerTicket;
    private IClient client;
    private ISession session;
    public ISocket socket;
    public IMatch CurrentMatch { get; private set; }
    public IUserPresence SelfPresence => CurrentMatch?.Self;
    public IUserPresence OpponentPresence { get; private set; }


    private string scheme = "http";
    //private string host = "127.0.0.1";
    private string host = "10.28.252.218";
    private int port = 7350;
    private string serverKey = "defaultkey";

    private bool shouldLoadGameScene = false;
    private string targetSceneName = "Game";

    public bool IsOnline { get; private set; } = false;

    public string MyUsername { get; private set; }
    public string OpponentUsername { get; private set; }
    public string CurrentMatchId { get; private set; }



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
    public async void FindMatch(string sceneToLoad)
    {
        if (socket == null)
        {
            Debug.LogError("dis_Soket!");
            return;
        }

        Debug.Log("search player...");

        socket.ReceivedMatchmakerMatched -= OnMatchmakerMatched;
        socket.ReceivedMatchmakerMatched += OnMatchmakerMatched;

        matchmakerTicket = await socket.AddMatchmakerAsync("*", 2, 2);
    }

    private async void OnMatchmakerMatched(IMatchmakerMatched matched)
    {
        Debug.Log("find player...");

        socket.ReceivedMatchmakerMatched -= OnMatchmakerMatched;


        try
        {
            Debug.Log("before join");

            CurrentMatch = await socket.JoinMatchAsync(matched);

            Debug.Log("after join");

            CurrentMatchId = CurrentMatch.Id;
            MyUsername = matched.Self.Presence.Username;
            OpponentUsername = matched.Users.FirstOrDefault()?.Presence.Username ?? "Unknown";

            OpponentPresence = CurrentMatch.Presences
                .FirstOrDefault(p => p.SessionId != CurrentMatch.Self.SessionId);

            Debug.Log($"MatchId: {CurrentMatchId}");
            Debug.Log($"My: {MyUsername}");
            Debug.Log($"Opponent: {OpponentUsername}");

            shouldLoadGameScene = true;
        }
        catch (Exception e)
        {
            Debug.LogError($"JoinMatch failed: {e}");
        }
    }

    public async Task<bool> SetPlayerUsernameAsync(string username)
    {
        try
        {
            if (session == null) return false;

            await client.UpdateAccountAsync(session, username);

            PlayerPrefs.SetString("PlayerUsername", username);
            PlayerPrefs.Save();


            Debug.Log($"Username successfully updated to: {username}");
            return true;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to update username: {ex.Message}");
            return false;
        }
    }
}
