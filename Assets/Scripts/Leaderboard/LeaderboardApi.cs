using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using System;
using System.Text;

/// <summary>
/// Static LeaderboardAPI class. Provides corotuines to match the possible requests thast can be sent to the server
/// in terms of domain objects (ie LeaderboardRecords).
/// </summary>
public static class LeaderboardApi
{
    private static readonly string URI = "https://covidcrossing.offbeat.systems/leaderboard";

    /// <summary>
    /// Result handler interface used to report the result of a network request when it completes.
    /// This has no success payload and corresponds to an HTTP 200 with no body.
    /// </summary>
    public interface ApiResultHandler
    {
        void OnSuccess();
        void OnError(string error);
    }

    /// <summary>
    /// Result handler interface used to report the result of a network request when it co pletes.
    /// This has a success payload according to the contents of the server response.
    /// </summary>
    /// <typeparam name="T">The type of the response data</typeparam>
    public interface ApiResultHandler<T>
    {
        void OnSuccess(T value);
        void OnError(string error);
    }

    /// <summary>
    /// Unfortunately we have to use this wrapper class because the Unity JSON deserializer does not support directly
    /// deserializing an array. For now, this works, but if any more sophisticated deserialization were necessary it
    /// would probably be best to move to Json.NET for Unity or similar. The JSON library does need to 'work with' the
    /// Unity platform though -- you cannot just use any.
    /// </summary>
    [Serializable]
    private class Leaderboard
    {
        public List<PlayerRecord> contents = null;
    }

    /// <summary>
    /// Fetch the leaderboard contents from the server, parse them and return the result in the form of a list of PlayerRecords
    /// in descending order of score unless an error occurs (in which case the network or HTTP error messagr will be passed on).
    /// </summary>
    /// <param name="resultHandler">The ApiResultHandler to which we eventually pass the result</param>
    /// <returns>IEnumerator to be used with StartCoroutine</returns>
    public static IEnumerator GetLeaderboardCoroutine(ApiResultHandler<IList<PlayerRecord>> resultHandler)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(URI))
        {
            yield return request.SendWebRequest();

            if (!request.isNetworkError && !request.isHttpError)
            {
                string text = request.downloadHandler.text;

                // Need to use wrapper: see comment above.
                text = $"{{\"contents\": {text}}}";
                Leaderboard leaderboard = JsonUtility.FromJson<Leaderboard>(text);
                resultHandler.OnSuccess(leaderboard.contents);
            }
            else { resultHandler.OnError(request.error); }
        }
    }

    /// <summary>
    /// Post a high score to the server and wait for an OK result. An existing high score entry will be overwritten if it has
    /// the same GUID, otherwise a new one is created. The status of the request after it completes is reported using
    /// the result handler.
    /// </summary>
    /// <param name="record">The record to post to the server</param>
    /// <param name="resultHandler">The ApiResultHandler to which we eventually pass the result<</param>
    /// <returns>IEnumerator to be used with StartCoroutine</returns>
    public static IEnumerator PostHighScoreCoroutine(PlayerRecord record, ApiResultHandler resultHandler)
    {
        string json = JsonUtility.ToJson(record);

        // The way we do this is a bit rondabout because UnityWebRequest always wants to use a url-encoded body for
        // POST (but not PUT) using application/x-www-form-urlencoded. We just want send plain JSON so we cannot
        // use the helper method.
        // See https://forum.unity.com/threads/unitywebrequest-post-url-jsondata-sending-broken-json.414708/.
        using (UnityWebRequest request = new UnityWebRequest(URI, "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (!request.isNetworkError && !request.isHttpError) { resultHandler.OnSuccess(); }
            else { resultHandler.OnError(request.error); }
        }
    }
}
