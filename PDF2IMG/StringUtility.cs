using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDF2IMG
{
    public class StringUtility
    {
        public static char TokenStartCharacter = '{';
        public static char TokenEndCharacter = '}';
        public static char FunctionStartCharacter = '(';
        public static char FunctionEndCharacter = ')';
        public static char[] CommaSeparator = new char[] { ',' };
            
        public static string FormatString(string formatSpec, Dictionary<string,string> replacements)
        {
            if (formatSpec.IndexOf(TokenEndCharacter) == -1) return formatSpec; // no replacements
            StringBuilder sb = new StringBuilder();
            sb.Append(formatSpec);

            int i = 0;
            while (i >=0 && i < formatSpec.Length)
            {
                int startIndex = formatSpec.IndexOf(TokenStartCharacter,i);
                if (startIndex == -1) { i = startIndex; continue; }
                int endIndex = formatSpec.IndexOf(TokenEndCharacter, startIndex);
                if (endIndex == -1) { i = endIndex; continue; }
                string tokenWMarker = formatSpec.Substring(startIndex, endIndex - startIndex+1);
                string tokenOnly = formatSpec.Substring(startIndex + 1, endIndex - startIndex - 1).ToLower();
                
                if (replacements.ContainsKey(tokenOnly)) { sb.Replace(tokenWMarker, replacements[tokenOnly]); }
                else
                {
                    // Not found, do functions
                    int startFunction = tokenOnly.IndexOf(FunctionStartCharacter);
                    if (startFunction != -1)
                    {
                        int endFunction = tokenOnly.IndexOf(FunctionEndCharacter, startFunction);
                        if (endFunction == -1) { i = endIndex; continue; }
                        string functionName = tokenOnly.Substring(0, startFunction - 1);
                        string functionParameters = tokenOnly.Substring(startFunction + 1, endFunction - startFunction - 2);
                        string[] parms = functionParameters.Split(CommaSeparator);
                        switch(functionName)
                        {
                            // TODO: Implement functions
                            default:
                                throw new NotImplementedException($"Function {functionName} not implemented");

                        }
                    }

                }
                i = endIndex;
            }
            return sb.ToString();
        }
    }
}
