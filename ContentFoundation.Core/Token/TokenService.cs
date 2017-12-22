using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContentFoundation.Core.Token
{
    /// <summary>
    /// 把文本里各种Token变量转换成值, Token变量值定义在tokensettings.json里。
    /// Token Pattern: "This is a sample text from {:TokenName}."
    /// </summary>
    public class TokenService
    {
        public List<KeyValuePair<String, String>> Pairs { get; set; }
        public List<KeyValuePair<String, String>> Tokens { get; set; }
        public TokenService(List<KeyValuePair<String, String>> pairs)
        {
            Pairs = pairs;
            Tokens = new List<KeyValuePair<string, string>>();

            Pairs.ForEach(token => {
                if (!String.IsNullOrEmpty(token.Value))
                {
                    var name = token.Key.Split(":").Last();
                    Tokens.Add(new KeyValuePair<string, string>(name, token.Value));
                }

            });
        }

        public String Replace(String text)
        {
            if (!text.Contains("{")) return text;

            Tokens.ForEach(token => {
                text = text.Replace("{:" + token.Key + "}", token.Value);
            });

            return text;
        }
    }
}
