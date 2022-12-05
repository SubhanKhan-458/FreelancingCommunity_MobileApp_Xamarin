using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IPTMobileApp.MarkupExtensions
{
    [ContentProperty(nameof(ResourceId))]
    public class ImageResourceExtension : IMarkupExtension
    {
        public string ResourceId { get; set; }
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (string.IsNullOrWhiteSpace(ResourceId))
            {
                return null;
            }

            return ImageSource.FromResource(ResourceId);

        }
    }
}
