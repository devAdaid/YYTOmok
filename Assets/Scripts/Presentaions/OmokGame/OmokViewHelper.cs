using UnityEngine;

namespace Presentaions
{
    [System.Serializable]
    public class OmokViewHelper : MonoBehaviour
    {
        private static float padRatio = 0.99f;
        [SerializeField]
        private float stoneRatio = 0.95f;
        public RectTransform dol;
        public RectTransform topRight;

        private Camera _cam;
        private Camera cam
        {
            get
            {
                if (_cam == null)
                {
                    _cam = FindObjectOfType<Camera>();
                }
                return _cam;
            }
        }

        private RectTransform _rectTf;
        private RectTransform rectTf
        {
            get
            {
                if (_rectTf == null)
                {
                    _rectTf = (RectTransform)transform;
                }
                return _rectTf;
            }
        }

        private float boardSizeHalf => (topRight.position - rectTf.position).x * padRatio;
        private float boardSize => 2 * boardSizeHalf;
        private float gridSize => boardSize / Define.OMOK_COUNT;
        private float xMin => rectTf.position.x - boardSizeHalf;
        private float xMax => rectTf.position.x + boardSizeHalf;
        private float yMin => rectTf.position.y - boardSizeHalf;
        private float yMax => rectTf.position.y + boardSizeHalf;


        private (int rowIndex, int colIndex) GetGridIndex(Vector3 position)
        {
            var colIndex = -1;
            if (position.x < xMin)
            {
                colIndex = 0;
            }
            else if (position.x > xMax)
            {
                colIndex = Define.OMOK_COUNT - 1;
            }
            else
            {
                colIndex = (int)((position.x - xMin) / gridSize);
            }

            var rowIndex = -1;
            if (position.y < yMin)
            {
                rowIndex = 0;
            }
            else if (position.y > yMax)
            {
                rowIndex = Define.OMOK_COUNT - 1;
            }
            else
            {
                rowIndex = (int)((position.y - yMin) / gridSize);
            }

            return (rowIndex, colIndex);
        }

        private Vector3 GetPosition(int rowIndex, int colIndex)
        {
            var xPos = xMin + gridSize * colIndex;
            var yPos = yMin + gridSize * rowIndex;
            xPos += gridSize / 2;
            yPos += gridSize / 2;
            return new Vector3(xPos, yPos, 0f);
        }
    }
}