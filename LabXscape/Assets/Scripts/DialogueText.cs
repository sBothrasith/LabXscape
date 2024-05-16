﻿using System.Collections;
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
        "I can't seem to remember what has happened.",
        "Those scientists are speaking so quietly. I can't hear them at all.",
        "Ughhhh!",
        "\"My body feels different.\"",
        "I looked at myself in the reflection.",
        "\"I'm a ROBOT?!?\"",
		"I was shocked but there's nothing that I can do.",
		"\"Let's find out happened to me.\""
    };

    private static string[] stage1Khmer = {

    };

    private static string[] stage2English = {
        "\"This room seems like a Data Storage Room.\"",
        "\"What is that glowing computer? Is it on?\"",
		"\"I should do something about them.\"",
		"\"Maybe collecting more letters will give me a clue!\""
	};

    private static string[] stage2Khmer = {

    };

    private static string[] stage3English = {
        "\"What is this room?\"",
		"\"It feels different from all the previous room. It's spacious!\"",
        "In the distance, there's a glowing incubation chamber.",
		"\"It's glowing red, something must've happen!\"",
		"\"The glowing button seems dangerous.\"",
		"\"I better becareful not to step on them.\""
	};

    private static string[] stage3Khmer = {

    };

    private static string[] stage4English = {
		"\"This looks like a security room!\"",
		"\"Let's move carefully, especially those laser heated pipe.\"",
		"\"I should avoid them to stay alive.\""
	};

    private static string[] stage4Khmer = {

    };

    private static string[] stage5English = {
		"The room are getting bigger and bigger the more I move through the elevator.",
		"\"There's a room above me, maybe something is there.\"",
        "\"I might be able to move those platforms if I trigger those levers.\"",
		"\"Let's not forget to follow those suspicious pipe.\""
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

    private static string[] stage7English = {

    };

    private static string[] stage7Khmer = {

    };

    private static string[] stageEndingEnglish = {
        "\"This? This room is empty?!\"",
        "Have I been running around for naught?",
        "Wait? I see a door in the distance.",
        "\"Let's try to get on it!\""
    };

    private static string[] stageEndingKhmer = {

    };

    private static string[] stageCreditsEnglish =
    {
        "Robot boy made it out to the surface at last",
        "But what awaits him is a ruin from all the war!",
        "Thus, he walks around in the ruins trying to find his friends",
        "Alas! He have used up all his battery while climbing the bunker.",
        "He lays lifeless, waiting for the day he rots away!",
        "The end...",
        "Thank you for playing <3"
    };

	private static string[] stageCreditsKhmer =
	{

	};

	public static string[] GetDialogueFromStage (int stage) {
        if (PlayerPrefs.GetString("language") == "khmer") {
            switch (stage) {
                case 0:
                    return UpdateFontKhmer(stage0Khmer);
                case 1:
                    return UpdateFontKhmer(stage1Khmer);
                case 2:
                    return UpdateFontKhmer(stage2Khmer);
                case 3:
                    return UpdateFontKhmer(stage3Khmer);
                case 4:
                    return UpdateFontKhmer(stage4Khmer);
                case 5:
                    return UpdateFontKhmer(stage5Khmer);
                case 6:
                    return UpdateFontKhmer(stage6Khmer);
                case 7:
                    return UpdateFontKhmer(stage7Khmer);
                case 8:
                    return UpdateFontKhmer(stageEndingKhmer);
                case 9:
                    return UpdateFontKhmer(stageCreditsKhmer);
                default:
                    return UpdateFontKhmer(stage1Khmer);
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
                    return stage7English;
                case 8:
                    return stageEndingEnglish;
                case 9:
                    return stageCreditsEnglish;
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
