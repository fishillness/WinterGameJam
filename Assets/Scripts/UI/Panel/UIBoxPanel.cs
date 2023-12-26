using UnityEngine;
using UnityEngine.UI;

namespace WinterGameJam
{
    public class UIBoxPanel : MonoBehaviour
    {
        [SerializeField] private Text boxCountText;

        private void Start()
        {
            BoxCounter.OnBoxCountUpdate += UpdateBoxCountText;
        }

        private void OnDestroy()
        {
            BoxCounter.OnBoxCountUpdate -= UpdateBoxCountText;
        }

        private void UpdateBoxCountText(int boxCount)
        {
            boxCountText.text = boxCount.ToString();
        }
    }
}
