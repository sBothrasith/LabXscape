using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Windows;

public class KhmerToKeyboard : MonoBehaviour {

    static readonly Dictionary<char, string> khmerToKeyboard = new Dictionary<char, string>
    {
        //consonants
        {'ក', "k"}, {'ខ', "x"}, {'គ', "K"}, {'ឃ', "X"}, {'ង', "g"},
        {'ច', "c"}, {'ឆ', "q"}, {'ជ', "C"}, {'ឈ', "Q"}, {'ញ', "j"},
        {'ដ', "d"}, {'ឋ', "z"}, {'ឌ', "D"}, {'ឍ', "Z"}, {'ណ', "N"},
        {'ត', "t"}, {'ថ', "f"}, {'ទ', "T"}, {'ធ', "F"}, {'ន', "n"},
        {'ប', "b"}, {'ផ', "p"}, {'ព', "B"}, {'ភ', "P"}, {'ម', "m"},
        {'យ', "y"}, {'រ', "r"}, {'ល', "l"}, {'វ', "v"}, {'ស', "s"},
        {'ហ', "h"}, {'ឡ', "L"}, {'អ', "G"},
        //vowels
        {'ៈ', "³"}, {'័', "½"}, {'ា', "a"}, {'ិ', "i"}, {'ី', "I"},
        {'ឹ', "w"}, {'ឺ', "W"}, {'ុ', "u"}, {'ូ', "U"}, {'ួ', "Y"},
        {'ើ', "eI"}, {'ឿ', "eO"}, {'ៀ', "eo"}, {'េ', "e"}, {'ែ', "E"},
        {'ៃ', "é"}, {'ោ', "ea"}, {'ៅ', "eA"}, {'ំ', "M"}, {'ះ', "H"},
        //indepentdent vowels
        {'ឥ', "\\"}, {'ឦ', "|"}, {'ឧ', "]"}, {'ឩ', "«"}, {'ឪ', "«"}, 
        {'ឫ', "b£"}, {'ឬ', "b¤"}, {'ឮ', "B¤"}, {'ឯ', "É"}, {'ឰ', "B§"}, 
        {'ឱ', "»"}, {'ឳ', "«"},
        //digits
        {'០', "0"}, {'១', "1"}, {'២', "2"}, {'៣', "3"}, {'៤', "4"},
        {'៥', "5"}, {'៦', "6"}, {'៧', "7"}, {'៨', "8"}, {'៩', "9"},
        //diacritic
        {'៉', ":"}, {'៊', "‘"}, {'់', ";"}, {'៏', "¾"}, {'៍', "_"},
        {'៌', "’"}, {'៎', "+"},
        //punctuation
        {'ៗ', "²"}, {'។', "."}
    };

    static string[] checkDiacritics = {":", "‘", ";", "¾", "_", "’", "+"};

    static readonly Dictionary<string, string> khmerToKeyboardSubScript = new Dictionary<string, string> {
        //subscript
        {"្ក", "á"}, {"្ខ", "ç"}, {"្គ", "Á"}, {"្ឃ", "Ç"}, {"្ង", "¶"},
        {"្ច", "©"}, {"្ឆ", "ä"}, {"្ជ", "¢"}, {"្ឈ", "Ä"}, {"្ញ", "J"},
        {"្ដ", "þ"}, {"្ឋ", "æ"}, {"្ឌ", "Ð"}, {"្ឍ", "Æ"}, {"្ណ", "Ñ"},
        {"្ត", "þ"}, {"្ថ", "ß"}, {"្ទ", "Þ"}, {"្ធ", "§"}, {"្ន", "ñ"},
        {"្ប", ","}, {"្ផ", "ö"}, {"្ព", "<"}, {"្ភ", "Ö"}, {"្ម", "µ"},
        {"្យ", "ü"}, {"្រ", "R"}, {"្ល", "ø"}, {"្វ", "V"}, {"្ស", "S"},
        {"្ហ", "ð"}, {"្អ", "¥"}
    };

    public static string ConvertKhmerToKeyboard(string khmerText) {
        StringBuilder result = new StringBuilder();

        for (int i = 0; i < khmerText.Length; i++) {
            if (khmerText[i] == '្' && i + 1 < khmerText.Length) {
                string sub = khmerText.Substring(i, 2);
                if (khmerToKeyboardSubScript.TryGetValue(sub, out string keyboardSub)) {
                    if (keyboardSub == "R") {
                        if (result.Length > 0) {
                            result.Insert(result.Length - 1, keyboardSub);
                        }
                        else {
                            result.Append(keyboardSub);
                        }
                    }
                    else {
                        result.Append(keyboardSub);
                    }
                    i++;
                    continue;
                }
            }
            if (khmerToKeyboard.TryGetValue(khmerText[i], out string keyboardChar)) {
                if (keyboardChar.Contains("eI") || keyboardChar.Contains("eO") || keyboardChar.Contains("eo") || keyboardChar.Contains("ea") || keyboardChar.Contains("eA") || keyboardChar.Contains("é") || keyboardChar.Contains("e") || keyboardChar.Contains("E")) {
                    if (result.Length > 0) {
                        string lastChar = result[result.Length - 1].ToString();
                        string last2Char = result.Length > 1 ? result[result.Length - 2].ToString() : "";
                        if (khmerToKeyboardSubScript.ContainsValue(lastChar) || (khmerToKeyboardSubScript.ContainsValue(last2Char) && last2Char == "R") || checkDiacritics.Contains(lastChar)) {
                            string lastTwoChars = result.ToString(result.Length - 2, 2);
                            result.Remove(result.Length - 2, 2);
                            if (keyboardChar.Length == 1) {
                                result.Append(keyboardChar + lastTwoChars);
                            }
                            else {
                                result.Append(keyboardChar[0] + lastTwoChars + keyboardChar[1]);
                            }
                        } else {
                            result.Remove(result.Length - 1, 1);
                            if (keyboardChar.Length == 1) {
                                result.Append(keyboardChar + lastChar);
                            } else {
                                result.Append(keyboardChar[0] + lastChar + keyboardChar[1]);
                            }
                        }
                    }
                } else {
                    result.Append(keyboardChar);
                }
            }
            else {
                result.Append(khmerText[i]);
            }
        }

        string output = Regex.Replace(result.ToString(), @"ba|bA", m => m.Value == "ba" ? ")a" : ")A");

        return output;
    }
}
