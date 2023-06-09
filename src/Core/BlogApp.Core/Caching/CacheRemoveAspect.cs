using System;
using BlogApp.Core.CrossCuttingConcerns.Caching;
using BlogApp.Core.Interceptors;
using BlogApp.Core.Utilities.IoC;
using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;

namespace BlogApp.Core.Caching
{
    //Cache deki veride değişim olursa kullanılır. Veride güncelleme,ekleme, silme
    //gibi veriyi manipüle eden durumlarda kullanılır
    public class CacheRemoveAspect : MethodInterception
    {
        private string _pattern;
        private ICacheManager _cacheManager;

        public CacheRemoveAspect(string pattern)
        {
            _pattern = pattern;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        protected override void OnSuccess(IInvocation invocation)
        {
            _cacheManager.RemoveByPattern(_pattern);
        }
    }
}

