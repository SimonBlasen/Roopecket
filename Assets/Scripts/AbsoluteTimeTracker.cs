using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if MOBILE
#else
using Steamworks;
#endif
using System;

public class AbsoluteTimeTracker : MonoBehaviour
{

#if MOBILE
    internal void UploadScore()
    {

    }
#else
    protected CallResult<LeaderboardFindResult_t> m_LeaderboardFindResult;
    protected CallResult<LeaderboardScoresDownloaded_t> m_LeaderboardScoresDownloadedResult;

    private bool startCounting = false;
    private bool isFound = false;
    private bool startDownlScore = false;
    private SteamLeaderboard_t foundLeaderboard;

    public static int StartTime { get; set; }
    public static int AccTime { get; set; }

    private void Awake()
    {

        m_LeaderboardFindResult = CallResult<LeaderboardFindResult_t>.Create(OnLeaderboardFind);
        m_LeaderboardScoresDownloadedResult = CallResult<LeaderboardScoresDownloaded_t>.Create(OnScoreDownloaded);
    }

    // Start is called before the first frame update
    void Start()
    {
        oldTime = AccTime;
        timer = AccTime;

        if (SteamManager.Initialized)
        {
            SteamAPICall_t handle = SteamUserStats.FindLeaderboard("absolute_playtime");

            m_LeaderboardFindResult.Set(handle);
            Debug.Log("Created CallResult for FindLeaderboard");
        }
    }

    private void OnLeaderboardFind(LeaderboardFindResult_t param, bool bIOFailure)
    {
        isFound = true;
        foundLeaderboard = param.m_hSteamLeaderboard;
        Debug.Log("Found Leaderboard");
    }

    private void OnScoreDownloaded(LeaderboardScoresDownloaded_t param, bool bIOFailure)
    {
        LeaderboardEntry_t entry;
        int[] details = new int[0];

        SteamUserStats.GetDownloadedLeaderboardEntry(param.m_hSteamLeaderboardEntries, 0, out entry, details, 0);


        if (StartTime == -1)
        {
            StartTime = entry.m_nScore;
            timer = entry.m_nScore;
            oldTime = entry.m_nScore;
        }
        else
        {
            //timer = entry.m_nScore;
            //oldTime = entry.m_nScore;
        }

        startCounting = true;
        Debug.Log("Got entry with score: " + entry.m_nScore);
    }

    private float timer = 0f;
    private int oldTime = 0;


    public void UploadScore()
    {
        SteamUserStats.UploadLeaderboardScore(foundLeaderboard, ELeaderboardUploadScoreMethod.k_ELeaderboardUploadScoreMethodForceUpdate, StartTime + (AccTime / 60), new int[0], 0);
        Debug.Log("Updated score");
        Debug.Log("StartTime: " + StartTime);
        Debug.Log("AccTime/60 : " + ((AccTime / 60)).ToString());
    }

    // Update is called once per frame
    void Update()
    {
        if (isFound && startDownlScore == false)
        {
            startDownlScore = true;
            SteamAPICall_t handleEntries = SteamUserStats.DownloadLeaderboardEntriesForUsers(foundLeaderboard, new CSteamID[] { SteamUser.GetSteamID() }, 1);

            m_LeaderboardScoresDownloadedResult.Set(handleEntries);
            Debug.Log("Downloading entries...");
        }


        timer += Time.deltaTime;
        if (((int)timer) != oldTime)
        {
            oldTime = (int)timer;
            AccTime = oldTime;
            if (startCounting)
            {


                //SteamUserStats.UploadLeaderboardScore(foundLeaderboard, ELeaderboardUploadScoreMethod.k_ELeaderboardUploadScoreMethodForceUpdate, oldTime, new int[0], 0);
                //Debug.Log("Updated score");

            }
        }
    }
#endif
}
