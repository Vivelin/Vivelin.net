using System;
using System.Collections.Generic;

using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Vivelin.Web.Home.TagHelpers
{
    [HtmlTargetElement("error-message")]
    public class ErrorMessageTagHelper : TagHelper
    {
        private static readonly Random s_random = new();

        public int? Type { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var error = GetErrorMessage(Type);
            output.TagName = null;
            output.Content.SetContent(error);
        }

        protected virtual string GetErrorMessage(int? type)
        {
            var messages = GetErrorMessages(type);
            if (messages.Count == 0)
                throw new InvalidOperationException($"No error messages are configured for {type}.");

            var i = s_random.Next(0, messages.Count);
            return messages[i];
        }

        protected virtual IReadOnlyList<string> GetErrorMessages(int? type) => type switch
        {
            204 => new[] { @"¯\_(ツ)_/¯", "w(ﾟДﾟ)w", "(•ˋ _ ˊ•)", "(°ロ°)", "(ㆆ_ㆆ)", @"/ᐠ｡ꞈ｡ᐟ\" },
            404 => new[] { @"¯\_(ツ)_/¯", "w(ﾟДﾟ)w", "(•ˋ _ ˊ•)", "(°ロ°)", "(ㆆ_ㆆ)", @"/ᐠ｡ꞈ｡ᐟ\" },
            _ => new[] { @"¯\_(ツ)_/¯" }
        };
    }
}
