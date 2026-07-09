using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyLittleBoat
{
    public class AlbumUiController : MonoBehaviour
    {
        private enum AlbumTab
        {
            Photos,
            Scenery,
            Letters
        }

        private GameObject albumPanel;
        private Text albumTitleText;
        private Text albumContentText;
        private AlbumTab currentTab = AlbumTab.Photos;

        public void Initialize(Transform canvasTransform)
        {
            if (canvasTransform == null)
            {
                return;
            }

            albumPanel = SimpleUiFactory.CreatePanel(canvasTransform, "Album Panel", new Color(1f, 1f, 1f, 0.88f));
            RectTransform panelRect = albumPanel.GetComponent<RectTransform>();
            SimpleUiFactory.Stretch(panelRect, new Vector2(0.07f, 0.20f), new Vector2(0.93f, 0.86f), Vector2.zero, Vector2.zero);

            VerticalLayoutGroup layout = albumPanel.AddComponent<VerticalLayoutGroup>();
            layout.padding = new RectOffset(32, 32, 32, 32);
            layout.spacing = 16f;
            layout.childControlHeight = true;
            layout.childControlWidth = true;
            layout.childForceExpandHeight = false;
            layout.childForceExpandWidth = true;

            albumTitleText = SimpleUiFactory.CreateText(albumPanel.transform, "Album Title", "사진 앨범", 42, TextAnchor.MiddleLeft, new Color(0.08f, 0.18f, 0.28f));
            SimpleUiFactory.AddLayoutElement(albumTitleText.gameObject, 0f, 70f);

            GameObject tabs = SimpleUiFactory.CreatePanel(albumPanel.transform, "Album Tabs", new Color(1f, 1f, 1f, 0f));
            HorizontalLayoutGroup tabLayout = tabs.AddComponent<HorizontalLayoutGroup>();
            tabLayout.spacing = 10f;
            tabLayout.childControlWidth = true;
            tabLayout.childControlHeight = true;
            tabLayout.childForceExpandWidth = true;
            SimpleUiFactory.AddLayoutElement(tabs, 0f, 74f);

            CreateTabButton(tabs.transform, "사진", AlbumTab.Photos);
            CreateTabButton(tabs.transform, "풍경", AlbumTab.Scenery);
            CreateTabButton(tabs.transform, "편지", AlbumTab.Letters);

            albumContentText = SimpleUiFactory.CreateText(albumPanel.transform, "Album Content", string.Empty, 30, TextAnchor.UpperLeft, new Color(0.12f, 0.22f, 0.32f));
            SimpleUiFactory.AddLayoutElement(albumContentText.gameObject, 0f, 560f);

            Button closeButton = SimpleUiFactory.CreateButton(albumPanel.transform, "닫기", new Color(0.86f, 0.93f, 0.96f, 1f));
            closeButton.onClick.AddListener(ToggleAlbum);
            SimpleUiFactory.AddLayoutElement(closeButton.gameObject, 0f, 74f);

            albumPanel.SetActive(false);
            RefreshAlbum();
        }

        /// <summary>
        /// Opens or closes the album panel and refreshes the selected tab.
        /// </summary>
        public void ToggleAlbum()
        {
            if (albumPanel == null)
            {
                return;
            }

            albumPanel.SetActive(!albumPanel.activeSelf);
            RefreshAlbum();
        }

        private void CreateTabButton(Transform parent, string label, AlbumTab tab)
        {
            Button button = SimpleUiFactory.CreateButton(parent, label, new Color(0.83f, 0.92f, 0.90f, 1f));
            button.onClick.AddListener(delegate
            {
                currentTab = tab;
                RefreshAlbum();
            });
        }

        private void RefreshAlbum()
        {
            if (albumTitleText == null || albumContentText == null)
            {
                return;
            }

            List<string> entries = GetCurrentEntries();
            albumTitleText.text = GetCurrentTitle();

            if (entries.Count == 0)
            {
                albumContentText.text = "아직 기록이 없어요. 바다에 조금 더 머물러 보세요.";
                return;
            }

            albumContentText.text = string.Empty;
            for (int i = 0; i < entries.Count; i++)
            {
                albumContentText.text += "- " + entries[i] + "\n";
            }
        }

        private List<string> GetCurrentEntries()
        {
            if (currentTab == AlbumTab.Scenery)
            {
                return MyLittleBoatSession.Album.Scenery;
            }

            if (currentTab == AlbumTab.Letters)
            {
                return MyLittleBoatSession.Album.Letters;
            }

            return MyLittleBoatSession.Album.Photos;
        }

        private string GetCurrentTitle()
        {
            if (currentTab == AlbumTab.Scenery)
            {
                return "풍경 앨범";
            }

            if (currentTab == AlbumTab.Letters)
            {
                return "편지 보관함";
            }

            return "사진 앨범";
        }
    }
}
