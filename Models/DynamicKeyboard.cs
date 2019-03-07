using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace AmlakBot.Models
{
    class DynamicKeyboard
    {
        public static KeyboardButton[][] GetKeyboard(string[] stringArray, int Level)
        {
            int mod = 0;
            KeyboardButton[][] keyboardInline = new KeyboardButton[stringArray.Length][];
            mod = stringArray.Length % 2;
            if (mod != 0)
            {
                int j = 2, k = 1;
                var i = 0;
                keyboardInline = new KeyboardButton[stringArray.Length - Level][];
                keyboardInline[0] = new KeyboardButton[1];
                keyboardInline[0][0] = new KeyboardButton(stringArray[0]);
                Level = Level + 2;
                for (i = 1; i <= stringArray.Length - Level; i++)
                {
                    if (j <= stringArray.Length - 1)
                    {

                        keyboardInline[i] = new KeyboardButton[2];
                        keyboardInline[i][0] = new KeyboardButton(stringArray[k]);
                        keyboardInline[i][1] = new KeyboardButton(stringArray[j]);
                        j = j + 2;
                        k = k + 2;
                    }
                }
                keyboardInline[i] = new KeyboardButton[1];
                keyboardInline[i][0] = new KeyboardButton("🔙 بازگشت به قبل");
            }
            else
            {
                int j = 1, k = 0;
                var i = 0;
                keyboardInline = new KeyboardButton[stringArray.Length / 2 + 1][];
                for (i = 0; i < stringArray.Length / 2; i++)
                {
                    if (j <= stringArray.Length)
                    {
                        keyboardInline[i] = new KeyboardButton[2];
                        keyboardInline[i][0] = new KeyboardButton(stringArray[k]);
                        keyboardInline[i][1] = new KeyboardButton(stringArray[j]);
                        j = j + 2;
                        k = k + 2;
                    }
                }
                keyboardInline[i] = new KeyboardButton[1];
                keyboardInline[i][0] = new KeyboardButton("🔙 بازگشت به قبل");
            }

            return keyboardInline;
        }
    }
}
