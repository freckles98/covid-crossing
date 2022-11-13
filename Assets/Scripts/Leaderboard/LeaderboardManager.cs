using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Leaderboard manager class. This class is responsible for managing the visual display of the leaderboard
/// and initiating requests to the server and responding to results (though the actual implementation of these
/// requests can be found in LeaderboardAPI).
/// </summary>
public class LeaderboardManager : MonoBehaviour
{
    public GameObject prefab;
    public GameObject scrollView;
    public GameObject content;
    public GameObject message;
    private bool cancel = false;
    public bool IsActive => gameObject.activeSelf;

    /// <summary>
    /// Leaderboard API result handler used to handle the result of the network request to request the leaderboard contents.
    /// </summary>
    private class GetLeaderboardResultHandler : LeaderboardApi.ApiResultHandler<IList<PlayerRecord>>
    {
        private LeaderboardManager manager;

        /// <summary>
        /// Initializes the handler with the given manager.
        /// </summary>
        /// <param name="manager">Manager instance</param>
        public GetLeaderboardResultHandler(LeaderboardManager manager)
        {
            this.manager = manager;
        }

        /// <summary>
        /// Called when the server returned the list of leaderboard records. Adds UI views for each of these leaderboard entries
        /// and updates the display so that leaderboard contents are displayed.
        /// </summary>
        /// <param name="leaderboard"></param>
        public void OnSuccess(IList<PlayerRecord> leaderboard)
        {
            if (manager.cancel) { return; }

            manager.message.SetActive(false);
            manager.scrollView.SetActive(true);

            int counter = 1;
            foreach (PlayerRecord record in leaderboard)
            {
                GameObject newObj = (GameObject)Instantiate(manager.prefab, manager.content.transform);

                var ranking = newObj.transform.Find("Ranking").GetComponent<Text>();
                var nickname = newObj.transform.Find("Nickname").GetComponent<Text>();
                var score = newObj.transform.Find("Score").GetComponent<Text>();
                ranking.text = counter.ToString();
                nickname.text = record.nickname;
                score.text = record.highScore.ToString();

                if (record.guid == SharedCanvas.Instance.saveManager.record.guid)
                {
                    ranking.color = nickname.color = score.color = Color.blue;
                }

                counter++;
            }
        }

        /// <summary>
        /// Called when there was an error retrieving the leaderboard contents. Notifies the manager of the error state.
        /// </summary>
        /// <param name="error">A message for the error that took place</param>
        public void OnError(string error)
        {
            if (manager.cancel) { return; }
            manager.OnError();
        }
    }

    /// <summary>
    /// Leaderboard API result handler used to handle the result of the network request to post the player high score.
    /// </summary>
    private class PostHighScoreResultHandler : LeaderboardApi.ApiResultHandler
    {
        LeaderboardManager manager;

        /// <summary>
        /// Initializes the handler with the given manager.
        /// </summary>
        /// <param name="manager">Manager instance</param>
        public PostHighScoreResultHandler(LeaderboardManager manager)
        {
            this.manager = manager;
        }

        /// <summary>
        /// Called when the server successfully posted the high score entry. Begins fetching the leadeerboard contents from the
        /// server using the corresponding API result handler.
        /// </summary>
        /// <param name="leaderboard"></param>
        public void OnSuccess()
        {
            if (manager.cancel) { return; }
            manager.StartCoroutine(LeaderboardApi.GetLeaderboardCoroutine(new GetLeaderboardResultHandler(manager)));
        }

        /// <summary>
        /// Called when there was an error posting the high score entry. Notifies the manager of the error state.
        /// </summary>
        /// <param name="error">A message for the error that took place</param>
        public void OnError(string error)
        {
            if (manager.cancel) { return; }
            manager.OnError();
        }
    }
    
    /// <summary>
    /// Called when there is an issue a leaderboard network request. Updates the status message to reflect the error condition.
    /// </summary>
    public void OnError()
    {
        message.GetComponent<Text>().text = "Failed to fetch leaderboard. Check your internet connection.";
    }

    /// <summary>
    /// Called when the user dismisses the leaderboard dialogue.
    /// </summary>
    public void OkClicked()
    {
        if (gameObject.activeSelf)
        {
            cancel = true;
            message.SetActive(true);
            scrollView.SetActive(false);
            gameObject.SetActive(false);
            foreach (Transform child in content.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }

    /// <summary>
    /// Begin displaying the leaderboard to the screen. Begins posting the player high score to the server and in the meantime, displays a status message
    /// indicating that records are being fetched. After posting the high score, the leaderboard is fetched and the display is updated.
    /// </summary>
    public void ShowLeaderboard()
    {
        cancel = false;
        gameObject.SetActive(true);
        message.GetComponent<Text>().text = "Fetching leaderboard entries...";
        StartCoroutine(LeaderboardApi.PostHighScoreCoroutine(SharedCanvas.Instance.saveManager.record, new PostHighScoreResultHandler(this)));
    }
}


