using System;
namespace TradplusSDK.Api
{
    public class TradplusBase
    {
        public enum AdPosition
        {
            TopLeft = 0,
            TopCenter = 1,
            TopRight = 2,
            Centered = 3,
            BottomLeft = 4,
            BottomCenter = 5,
            BottomRight = 6
        }

        public enum AdContentMode
        {
            Top = 0,//顶部水平居中
            Center = 1,//垂直居中并水平居中
            Bottom = 2,//底边水平居中
        }
    }
}