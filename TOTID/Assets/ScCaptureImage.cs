using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ScCaptureImage : MonoBehaviour
{
    public GameObject firstObject;
    public GameObject secondObject;

    private Image firstImage;
    private Image secondImage;

    private void Start()
    {
        // Assure-toi que les GameObjects ont des composants Image attachés
        firstImage = firstObject.GetComponent<Image>();
        secondImage = secondObject.GetComponent<Image>();
    }

    // Sauvegarde l'image de l'objet actif dans un fichier
    public void SaveImageFromActiveObject()
    {
        Image activeImage = GetActiveImage();

        if (activeImage != null)
        {
            Texture2D texture = (Texture2D)activeImage.mainTexture;
            byte[] bytes = texture.EncodeToPNG();
            File.WriteAllBytes("ActiveObjectImage.png", bytes);
            Debug.Log("Image from ActiveObject saved!");
        }
        else
        {
            Debug.LogError("ActiveObject does not have an Image component!");
        }
    }

    // Échange les images entre les deux GameObjects actifs
    public void SwitchImages()
    {
        Image firstActiveImage = GetActiveImage(firstObject);
        Image secondActiveImage = GetActiveImage(secondObject);

        if (firstActiveImage != null && secondActiveImage != null)
        {
            Sprite tempSprite = firstActiveImage.sprite;
            firstActiveImage.sprite = secondActiveImage.sprite;
            secondActiveImage.sprite = tempSprite;

            Debug.Log("Images switched between ActiveObjects!");
        }
        else
        {
            Debug.LogError("One or both ActiveObjects do not have an Image component!");
        }
    }

    // Retourne l'Image du GameObject actif
    private Image GetActiveImage()
    {
        return GetActiveImageInHierarchy(firstObject) ?? GetActiveImageInHierarchy(secondObject);
    }

    // Retourne l'Image du GameObject spécifié s'il est actif
    private Image GetActiveImage(GameObject gameObject)
    {
        return GetActiveImageInHierarchy(gameObject);
    }

    // Retourne l'Image du GameObject spécifié s'il est actif dans la hiérarchie
    private Image GetActiveImageInHierarchy(GameObject gameObject)
    {
        Image image = gameObject.GetComponent<Image>();
        if (image != null && gameObject.activeInHierarchy)
        {
            return image;
        }

        return null;
    }
}
