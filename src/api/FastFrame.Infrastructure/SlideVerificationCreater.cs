using System;
using System.DrawingCore;
using System.DrawingCore.Drawing2D;
using System.DrawingCore.Imaging;
using System.IO;
using System.Text;

namespace FastFrame.Infrastructure
{
    /// <summary>
    /// 滑动解锁生成器
    /// </summary>
    public sealed class SlideVerificationCreater
    {
        private readonly Random random = new Random();
        private SlideVerificationCreater() { }

        public static readonly Lazy<SlideVerificationCreater> Instance = new Lazy<SlideVerificationCreater>(() => new SlideVerificationCreater());

        /// <summary>
        /// 创建图片滑动数据
        /// </summary>
        public SlideVerificationOutput Create(SlideVerificationInput pars, out int positionX)
        {
            SlideVerificationOutput result = new SlideVerificationOutput();

            using (var side_image = Image.FromStream(pars.SlideStream))
            {
                using var bg_image = Image.FromStream(pars.BackgroundStream);
                using Bitmap coverImage = ResizeImage(side_image, pars.SlideSize /*new Size(64, 64)*/);
                using Bitmap sourceImage = ResizeImage(bg_image, pars.BackgroundSize /*new Size(400, 225)*/);

                result.BgWidth = sourceImage.Width;
                result.BgHeight = sourceImage.Height;
                result.SlideWidth = coverImage.Width;
                result.SlideHeight = coverImage.Height;

                positionX = random.Next(coverImage.Width, sourceImage.Width - coverImage.Width - coverImage.Width);
                result.PositionY = random.Next(coverImage.Height, sourceImage.Height - coverImage.Height);

                //滑块图片
                result.SlideImg = ImageToBase64(CaptureImage(sourceImage, coverImage, positionX, result.PositionY), ImageFormat.Png);

                //背景图片
                result.BackgroundImg = ImageToBase64(DrawBackground(sourceImage, coverImage, positionX, result.PositionY), ImageFormat.Jpeg);
            }

            return result;
        }

        /// <summary>
        /// 调整图片大小
        /// </summary> 
        private static Bitmap ResizeImage(Image imgToResize, Size? size)
        {
            if (size == null)
                return new Bitmap(imgToResize);

            //获取图片宽度
            int sourceWidth = imgToResize.Width;
            //获取图片高度
            int sourceHeight = imgToResize.Height;
            //计算宽度的缩放比例
            float nPercentW = size.Value.Width / (float)sourceWidth;
            //计算高度的缩放比例
            float nPercentH = size.Value.Height / (float)sourceHeight;


            float nPercent;
            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;

            //期望的宽度
            int destWidth = (int)(sourceWidth * nPercent);
            //期望的高度
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap b = new Bitmap(destWidth, destHeight);
            using (Graphics g = Graphics.FromImage(b))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                //绘制图像
                g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            }
            return b;
        }

        /// <summary>
        /// 画背景图并将滑块画上去
        /// </summary> 
        private byte[] DrawBackground(Bitmap sourceImage, Bitmap coverImage, int positionX, int positionY)
        {
            //背景图片
            using (Graphics graphics = Graphics.FromImage(sourceImage))
            {
                graphics.DrawImage(coverImage, positionX, positionY, coverImage.Width, coverImage.Height);
                graphics.Save();
            }

            return ImageToByteArr(sourceImage, ImageFormat.Jpeg);
        }

        /// <summary>
        /// 从大图中根据像素点，生成图片，作为拼图块
        /// </summary> 
        private byte[] CaptureImage(Bitmap backgroudImage, Bitmap coverImage, int offsetX, int offsetY)
        {
            //创建新图位图
            using Bitmap bitmap = new Bitmap(coverImage.Width, coverImage.Height);
            for (int y = 0; y < coverImage.Height; y++)
            {
                for (int x = 0; x < coverImage.Width; x++)
                {
                    var pointColor = coverImage.GetPixel(x, y);

                    /*扣像素点 PS：透明点不扣*/
                    if (pointColor.A != 0)
                        bitmap.SetPixel(x, y, backgroudImage.GetPixel(offsetX + x, offsetY + y));
                }
            }

            return ImageToByteArr(bitmap, ImageFormat.Png);
        }

        /// <summary>
        /// Image转Byte数组
        /// </summary> 
        private byte[] ImageToByteArr(Image sourceImage, ImageFormat format)
        {
            using MemoryStream stream = new MemoryStream();
            sourceImage.Save(stream, format);
            byte[] buffer = new byte[stream.Length];
            stream.Position = 0;
            stream.Read(buffer, 0, buffer.Length);
            stream.Close();
            return buffer;
        }


        /// <summary>
        /// Image转base64
        /// </summary>  
        public string ImageToBase64(byte[] buffer, ImageFormat format)
        {
            StringBuilder result = new StringBuilder();
            result.Append($"data:image/{format};base64,");
            result.Append(Convert.ToBase64String(buffer));
            return result.ToString();
        }
    }

    public class SlideVerificationInput
    {
        public SlideVerificationInput(Stream backgroundStream, Stream slideStream)
        {
            BackgroundStream = backgroundStream;
            SlideStream = slideStream;
        }

        /// <summary>
        /// 背景大小
        /// </summary>
        public Size? BackgroundSize { get; set; }

        /// <summary>
        /// 滑块大小
        /// </summary>
        public Size? SlideSize { get; set; }

        /// <summary>
        /// 背景内容
        /// </summary>
        public Stream BackgroundStream { get; }

        /// <summary>
        /// 滑块内容
        /// </summary>
        public Stream SlideStream { get; }
    }

    public class SlideVerificationOutput
    {
        /// <summary>
        /// 背景图片base64
        /// </summary>
        public string BackgroundImg { get; set; }

        /// <summary>
        /// 滑块图片base64
        /// </summary>
        public string SlideImg { get; set; }

        /// <summary>
        /// Y轴
        /// </summary>
        public int PositionY { get; set; }

        /// <summary>
        /// 背景高
        /// </summary>
        public int BgHeight { get; set; }

        /// <summary>
        /// 背景宽
        /// </summary>
        public int BgWidth { get; set; }

        /// <summary>
        /// 滑块高
        /// </summary>
        public int SlideHeight { get; set; }

        /// <summary>
        /// 滑块宽
        /// </summary>
        public int SlideWidth { get; set; }
    }
}
