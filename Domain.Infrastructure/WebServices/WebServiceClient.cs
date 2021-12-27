using Domain.Infrastructure.CrossCuttingConcerns.Exceptions;
using System;
using System.Configuration;
using Domain.Infrastructure.DomainApiService;
using Olipso.Api.ClientApi.OlipsoDomainApi;

namespace Domain.Infrastructure.WebServices
{
    public class WebServiceClient
    {
        public static IDomainApi DomainApiServiceClient
        {
            get
            {
                var userName = ConfigurationManager.AppSettings["WebServisUserName"];
                var passWord = ConfigurationManager.AppSettings["WebServisPassword"];

                if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(passWord))
                    throw new BusinessException("Web Servis Kullanıcı Bilgisi Tanımlanmamış!");

                var url = ConfigurationManager.AppSettings["WebServiceURL"];
                if (string.IsNullOrEmpty(url))
                    throw new BusinessException("Web Servis Url Bilgisi Tanımlanmamış!");

                return (IDomainApi)Activator.CreateInstance(
                    Type.GetType(string.Format(
                        "{0},{1}", "Olipso.Api.ClientApi.DomainApi",
                        "Olipso.Api.ClientApi, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null")
                    ),
                    string.Format("{0}/DomainApi.svc", url),
                    userName,
                    passWord
                );
            }
        }
    }
}
