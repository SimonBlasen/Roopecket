using Steamworks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLeaderboardDownloader : MonoBehaviour
{
    protected CallResult<LeaderboardFindResult_t> m_LeaderboardFindResult;
    protected CallResult<LeaderboardScoresDownloaded_t> m_LeaderboardScoresDownloadedResult;
    protected CallResult<LeaderboardScoresDownloaded_t> m_LeaderboardScoresDownloadedResult_First;

    private bool isFound = false;
    private bool startDownlScore = false;
    private SteamLeaderboard_t foundLeaderboard;


    private void Awake()
    {
        m_LeaderboardFindResult = CallResult<LeaderboardFindResult_t>.Create(OnLeaderboardFind);
        m_LeaderboardScoresDownloadedResult = CallResult<LeaderboardScoresDownloaded_t>.Create(OnScoreDownloaded);
        m_LeaderboardScoresDownloadedResult_First = CallResult<LeaderboardScoresDownloaded_t>.Create(OnScoreDownloadedFirst);
    }

    private void OnScoreDownloadedFirst(LeaderboardScoresDownloaded_t param, bool bIOFailure)
    {
        Debug.Log("Downloaded leaderboards top 10");

        LeaderboardEntry_t entry;
        int[] details = new int[0];

        SteamUserStats.GetDownloadedLeaderboardEntry(param.m_hSteamLeaderboardEntries, 0, out entry, details, 0);

        timeFirst = entry.m_nScore;
        nameFirst = SteamFriends.GetFriendPersonaName(entry.m_steamIDUser);
        downloaded2 = true;
        if (downloaded1 && downloaded2 && resScreenG != null)
        {
            resScreenG.RefreshLeaderboard();
        }
    }

    private void OnScoreDownloaded(LeaderboardScoresDownloaded_t param, bool bIOFailure)
    {
        times = new int[param.m_cEntryCount];
        places = new int[param.m_cEntryCount];
        names = new string[param.m_cEntryCount];

        for (int i = 0; i < param.m_cEntryCount; i++)
        {
            LeaderboardEntry_t entry;
            int[] details = new int[0];

            SteamUserStats.GetDownloadedLeaderboardEntry(param.m_hSteamLeaderboardEntries, i, out entry, details, 0);

            times[i] = entry.m_nScore;
            places[i] = entry.m_nGlobalRank;
            names[i] = SteamFriends.GetFriendPersonaName(entry.m_steamIDUser);
        }

        Debug.Log("Downloaded leaderboards around user");
        downloaded1 = true;
        if (downloaded1 && downloaded2 && resScreenG != null)
        {
            resScreenG.RefreshLeaderboard();
        }
    }

    public int[] times;
    public int[] places;
    public string[] names;
    public int timeFirst;
    public string nameFirst;

    private bool downloaded1 = false;
    private bool downloaded2 = false;

    private void OnLeaderboardFind(LeaderboardFindResult_t param, bool bIOFailure)
    {
        isFound = true;
        foundLeaderboard = param.m_hSteamLeaderboard;
        Debug.Log("Found Leaderboard");
    }

    // Start is called before the first frame update
    void Start()
    {
        if (SteamManager.Initialized)
        {
            SteamAPICall_t handle = SteamUserStats.FindLeaderboard("level_" + GameObject.FindObjectOfType<LevelNumber>().LevelNumberProp + "_rocket_" + SavedGame.LastPlayedRocket);

            m_LeaderboardFindResult.Set(handle);
            Debug.Log("Created CallResult for FindLeaderboard");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isFound && startDownlScore == false)
        {
            startDownlScore = true;
            SteamAPICall_t handleEntries = SteamUserStats.DownloadLeaderboardEntries(foundLeaderboard, ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobalAroundUser, 50, 50);
            SteamAPICall_t handleEntriesFirst = SteamUserStats.DownloadLeaderboardEntries(foundLeaderboard, ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobal, 1, 10);
            //SteamAPICall_t handleEntries = SteamUserStats.DownloadLeaderboardEntriesForUsers(foundLeaderboard, new CSteamID[] { SteamUser.GetSteamID() }, 1);

            m_LeaderboardScoresDownloadedResult.Set(handleEntries);
            m_LeaderboardScoresDownloadedResult_First.Set(handleEntriesFirst);
            Debug.Log("Downloading entries...");
        }
    }

    private resultScreen resScreenG = null;
    public void UploadScore(int score, resultScreen resScreen)
    {
        resScreenG = resScreen;
        downloaded1 = false;
        downloaded2 = false;
        SteamUserStats.UploadLeaderboardScore(foundLeaderboard, ELeaderboardUploadScoreMethod.k_ELeaderboardUploadScoreMethodKeepBest, score, new int[] { }, 0);
        Debug.Log("Uploaded Score");



        SteamAPICall_t handleEntries = SteamUserStats.DownloadLeaderboardEntries(foundLeaderboard, ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobalAroundUser, 50, 50);
        SteamAPICall_t handleEntriesFirst = SteamUserStats.DownloadLeaderboardEntries(foundLeaderboard, ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobal, 1, 10);
        //SteamAPICall_t handleEntries = SteamUserStats.DownloadLeaderboardEntriesForUsers(foundLeaderboard, new CSteamID[] { SteamUser.GetSteamID() }, 1);

        m_LeaderboardScoresDownloadedResult.Set(handleEntries);
        m_LeaderboardScoresDownloadedResult_First.Set(handleEntriesFirst);

        Debug.Log("Downloading entries...");
    }
}
