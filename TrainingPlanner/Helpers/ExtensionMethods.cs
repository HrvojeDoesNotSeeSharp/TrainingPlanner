using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;

namespace TrainingPlanner.Helpers
{
    public static class ExtensionMethods
    {
        public static Image ScaleImage(this Image image)
        {
            var ratioX = (double) 250 / image.Width;
            var ratioY = (double) 250 / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);
            Graphics.FromImage(newImage).DrawImage(image, 0, 0, newWidth, newHeight);

            return newImage;
        }

        public static Image ScaleImageMin(this Image image)
        {
            var ratioX = (double)50 / image.Width;
            var ratioY = (double)50 / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);
            Graphics.FromImage(newImage).DrawImage(image, 0, 0, newWidth, newHeight);

            return newImage;
        }
    }
}