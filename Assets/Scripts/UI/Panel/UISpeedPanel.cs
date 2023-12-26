using UnityEngine;
using UnityEngine.UI;

namespace WinterGameJam
{
    public class UISpeedPanel : MonoBehaviour, IDependency<RoadGenerator>
    {
        [SerializeField] private Text speedText;

        private RoadGenerator roadGenerator;
        public void Construct(RoadGenerator obj) => roadGenerator = obj;

        private void Update()
        {
            speedText.text = roadGenerator.currentSpeed.ToString();
        }
    }
}
