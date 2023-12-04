using System.Drawing.Drawing2D;
using System.Drawing;
using System.Drawing.Imaging;

namespace StolovkyZilina.Util
{
	public static class ImageUtil
	{
		public static byte[] CropToSquare(byte[] originalImageBytes)
		{
			// Convert the byte[] to an Image
			using (MemoryStream stream = new MemoryStream(originalImageBytes))
			{
				using (Image originalImage = Image.FromStream(stream))
				{
					int width = originalImage.Width;
					int height = originalImage.Height;

					// Calculate the dimensions of the square using the longer side
					int newSize = Math.Min(width, height);

					// Create a new square Bitmap to hold the cropped image
					using (Bitmap croppedImage = new Bitmap(newSize, newSize))
					{
						using (Graphics graphics = Graphics.FromImage(croppedImage))
						{
							int offsetX = (newSize - width) / 2;
							int offsetY = (newSize - height) / 2;

							// Crop the image
							graphics.DrawImage(originalImage, offsetX, offsetY, width, height);
						}

						// Convert the cropped image to byte[]
						using (MemoryStream ms = new MemoryStream())
						{
							croppedImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg); // Change format if needed
							return ms.ToArray();
						}
					}
				}
			}
		}
        public static byte[] CropTo1x4AspectRatio(byte[] originalImageBytes)
        {
            // Convert the byte[] to an Image
            using (MemoryStream stream = new MemoryStream(originalImageBytes))
            {
                using (Image originalImage = Image.FromStream(stream))
                {
                    int width = originalImage.Width;
                    int height = originalImage.Height;

                    // Calculate the new width and height for the 1:4 aspect ratio

                    int newHeight = width / 4;
                    int newWidth = width;

                    // Calculate the cropping dimensions
                    int cropWidth = Math.Min(width, newWidth);
                    int cropHeight = Math.Min(height, newHeight);

                    // Calculate the cropping offsets
                    int offsetX = (width - cropWidth) / 2;
                    int offsetY = (height - cropHeight) / 2;

                    // Create a new Bitmap to hold the cropped image
                    using (Bitmap croppedImage = new Bitmap(cropWidth, cropHeight))
                    {
                        using (Graphics graphics = Graphics.FromImage(croppedImage))
                        {
                            // Crop the image
                            graphics.DrawImage(originalImage, 0, 0, new Rectangle(offsetX, offsetY, cropWidth, cropHeight), GraphicsUnit.Pixel);
                        }

                        // Convert the cropped image to byte[]
                        using (MemoryStream ms = new MemoryStream())
                        {
                            croppedImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg); // Change format if needed
                            return ms.ToArray();
                        }
                    }
                }
            }
        }
    }
}
