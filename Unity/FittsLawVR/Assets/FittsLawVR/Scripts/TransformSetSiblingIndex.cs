using UnityEngine;

public class TransformSetSiblingIndex : MonoBehaviour
{
    public void MoveTrialUp()
    {
        if (transform.GetSiblingIndex() >= 1)
        {
            //Update the Sibling Index of the GameObject
            transform.SetSiblingIndex(transform.GetSiblingIndex() - 1);

            //Tell the Playlist Manager that the Trial moved
            NewPlaylistManager.playlistController.TrialMoved(transform.GetSiblingIndex() + 1, transform.GetSiblingIndex());
        }
    }

    public void MoveTrialDown()
    {
        if (transform.GetSiblingIndex() < (transform.parent.childCount) - 1)
        {
            //Update the Sibling Index of the GameObject
            transform.SetSiblingIndex(transform.GetSiblingIndex() + 1);

            //Tell Playlist Manager that the Trial moved
            NewPlaylistManager.playlistController.TrialMoved(transform.GetSiblingIndex() - 1, transform.GetSiblingIndex());
        }
    }

    public void RemoveTrial()
    {
        //Tell Playlist Manager to delete the Trial
        NewPlaylistManager.playlistController.DeleteTrial(gameObject);
    }
}