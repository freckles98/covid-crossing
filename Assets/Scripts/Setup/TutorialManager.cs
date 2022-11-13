using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages running of tutorial: cycles through 5 tutorial images.
/// </summary>
public class TutorialManager : MonoBehaviour
{
    //current image:
    private int currentImage;
    
    //images to display
    public GameObject image0;
    public GameObject image1;
    public GameObject image2;
    public GameObject image3;
    public GameObject image4;

    private GameObject[] imageArr;


    /// <summary>
    /// Places gameObjects to be paged through in an array for easier navigation.
    /// </summary>
    void Start()
    {
        currentImage = 0;
        imageArr = new GameObject[] { image0, image1, image2 , image3, image4 };
    }

    /// <summary>
    /// Called by "next" button.
    /// Removes current image from screen and replaces it with the next tutorial image. Calls setupManager's FinishTutorial() if next button is clicked and at last picture.
    /// </summary>
    public void NextImage()
    {
        imageArr[currentImage].gameObject.SetActive(false);
        currentImage += 1;
        if (currentImage != (imageArr.Length)){ //if not at end
            imageArr[currentImage].gameObject.SetActive(true);
        }
        else //finished tutorial
        {
            SharedCanvas.Instance.setupManager.FinishTutorial();
        }
    }
}
