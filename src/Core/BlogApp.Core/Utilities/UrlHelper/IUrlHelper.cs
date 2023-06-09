using System;
namespace BlogApp.Core.Utilities.UrlHelper
{
    public interface IUrlHelper
    {
        string ToSeoUrl(string title);
        string AddBaseUrlToUrl(string url);
        string AddBaseUrlToProfilePhoto(string url);
    }
}

