using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using Microsoft;

namespace PMI.Application.Translation.Bing
{
    public static class AzureBingTranslator
    {
        private static readonly Uri serviceRootUri = new Uri("https://api.datamarket.azure.com/Data.ashx/Bing/MicrosoftTranslator/v1");
        private static readonly string accountKey = "NYvTXP4tA6tvbENuT58E1595KFz4rs9KhfCC3cCtJz0";

        public static TranslatorContainer InitializeTranslatorContainer()
        {
            var tc = new TranslatorContainer(serviceRootUri);
            tc.Credentials = new NetworkCredential(accountKey, accountKey);

            return tc;
        }

        public static Microsoft.Translation TranslateString(TranslatorContainer tc, string text, Language source, Language dest)
        {
            var translationQuery = tc.Translate(text, source.Code, dest.Code);
            var translationResults = translationQuery.Execute().ToList();

            var translationResult = translationResults.FirstOrDefault();

            return translationResult;
        }
    }
}