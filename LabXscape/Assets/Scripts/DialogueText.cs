using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueText : MonoBehaviour
{
    private static string[] stage0English = {
        "Welcome to The Tutorial!",
        "You can skip by clicking Skip on Top.",
        "You didn't skip? Alright Let's get moving!"
    };

    private static string[] stage0Khmer = {
        "សូមស្វាគមន៍មកកាន់ការបង្រៀន!",
        "អ្នកអាចរំលងដោយចុច \"Skip\" នៅលើកំពូល។",
        "អ្នកមិនបានរំលងទេ? ជាការប្រសើរណាស់ តោះ​ចាប់​ផ្ដើម!"
    };

    private static string[] stage1English = {
        "I was awoken in an unfamiliar room.",
        "\"Where am I? What am I doing here?\"",
        "\"My body feels different\".",
        "I looked at myself in the reflection.",
        "\"I'm a robot boy!?\". I was shocked but there's nothing that I can do.",
        "\"Let's find out what has happen to me.\""
    };

    private static string[] stage1Khmer = {

    };

    private static string[] stage2English = {
        "\"This room seems like a Data Storage Room.\"",
        "\"What is that glowing computer? Is it on?\"",
        "\"I should do something about them. Maybe I can interact.\""
    };

    private static string[] stage2Khmer = {

    };

    private static string[] stage3English = {
        "\"What is this room?\"",
        "\"It feels different from all the previous room.\"",
        "In the distance, there's a glowing incubation chamber.",
        "\"There must be something hidden there.\"",
        "\"There's also number on the platform.\"",
        "\"I better becareful not to step on those buttons.\""
    };

    private static string[] stage3Khmer = {

    };

    private static string[] stage4English = {
        "\"There's a heated platform ahead. I guess it's dangerous.\"",
        "\"There seem to also be platform to jump from.\"",
        "\"I should try to not fall while I jump.\""
    };

    private static string[] stage4Khmer = {

    };

    private static string[] stage5English = {
        "\"I can see the door right over there.\"",
        "\"There's a room above me, maybe something is there.\"",
        "\"I might be able to move those platforms if I trigger those levers.\""
    };

    private static string[] stage5Khmer = {

    };

    private static string[] stage6English = {
        "\"This room seems to have more light!\"",
        "\"Am I closer to being out of this bunker?\"",
        "\"I don't want to touch those lasers, they seem dangerous.\"",
        "\"Better be careful of it!\""
    };

    private static string[] stage6Khmer = {

    };

    private static string[] stageEndingEnglish = {
        "\"There are nothing in this room.\"",
        "Only one door. I wonder where it leads to."
    };

    private static string[] stageEndingKhmer = {

    };

    public static string[] GetDialogueFromStage (int stage) {
        if (PlayerPrefs.GetString("language") == "khmer") {
            switch (stage) {
                case 0:
                    return stage0Khmer;
                case 1:
                    return stage1Khmer;
                case 2:
                    return stage2Khmer;
                case 3:
                    return stage3Khmer;
                case 4:
                    return stage4Khmer;
                case 5:
                    return stage5Khmer;
                case 6:
                    return stage6Khmer;
                case 7:
                    return stageEndingEnglish;
                default:
                    return stage1Khmer;
            }
        } else {
            switch (stage) {
                case 0:
                    return stage0English;
                case 1:
                    return stage1English;
                case 2:
                    return stage2English;
                case 3:
                    return stage3English;
                case 4:
                    return stage4English;
                case 5:
                    return stage5English;
                case 6:
                    return stage6English;
                case 7:
                    return stageEndingKhmer;
                default:
                    return stage1English;
            }
        }
    }

    private static string[] UpdateFontKhmer(string[] fontKhmer) {
        string[] updatedFont = new string[fontKhmer.Length];
        for (int i = 0; i < fontKhmer.Length; i++) {
            updatedFont[i] = KhmerFontAdjuster.Adjust(fontKhmer[i]);
        }

        return updatedFont;
    }
}
