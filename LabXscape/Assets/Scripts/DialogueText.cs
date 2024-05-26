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
        "សូមស្វាគមន៍មកកាន់ការបង្រៀន",
        "អ្នកអាចរំលងដោយចុច \"រំលង\" នៅលើកំពូល។",
        "អ្នកមិនបានរំលងទេ? ជាការប្រសើរណាស់ តោះ​ចាប់​ផ្ដើម"
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
        "ខ្ញុំបានភ្ញាក់ឡើងនៅក្នុងបន្ទប់ដែលមិនស្គាល់។",
        "ខ្ញុំហាក់ដូចជាមិនចាំអ្វីដែលបានកើតឡើង។",
        "អ្នកវិទ្យាសាស្ត្រទាំងនោះកំពុងនិយាយយ៉ាងស្ងៀមស្ងាត់។ ខ្ញុំមិនអាចលឺពួកគេទាល់តែសោះ។",
        "រាងកាយរបស់ខ្ញុំមានអារម្មណ៍ខុស។",
        "ខ្ញុំក្រឡេកមើលខ្លួនឯងក្នុងការឆ្លុះបញ្ចាំង។",
        "ខ្ញុំជាមនុស្សយន្តមែនទេ?!?",
        "ខ្ញុំមានការភ្ញាក់ផ្អើលប៉ុន្តែមិនមានអ្វីដែលខ្ញុំអាចធ្វើបានទេ។",
        "តោះ​យើងស្វែងយល់ពីខ្ញុំ។"
    };

    private static string[] stage2English = {
        "\"This room seems like a Data Storage Room.\"",
        "\"What is that glowing computer? Is it on?\"",
		"\"I should do something about them.\"",
		"\"Maybe collecting more letters will give me a clue!\""
	};

    private static string[] stage2Khmer = {
        "បន្ទប់នេះហាក់ដូចជាបន្ទប់ផ្ទុកទិន្នន័យ។",
        "តើកុំព្យូទ័រភ្លឺនោះគឺជាអ្វី? តើវានៅលើទេ?",
        "ខ្ញុំគួរតែធ្វើអ្វីមួយ។",
        "ប្រហែលជាការប្រមូលអក្សរជាច្រើនទៀតនឹងផ្តល់ឱ្យខ្ញុំនូវតម្រុយមួយ"
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
        "\"តើនេះជាបន្ទប់អ្វី?\"",
        "\"វាមានអារម្មណ៍ខុសគ្នាពីបន្ទប់មុនទាំងអស់។ វាធំទូលាយណាស់\"",
        "\"ប៊ូតុងភ្លឺហាក់ដូចជាមានគ្រោះថ្នាក់។\"",
        "\"ខ្ញុំគួរតែប្រុងប្រយ័ត្នកុំបោះជំហានទៅលើវា។ \""
    };

    private static string[] stage4English = {
		"\"This looks like a security room!\"",
		"\"Let's move carefully, especially those laser heated pipe.\"",
		"\"I should avoid them to stay alive.\""
	};

    private static string[] stage4Khmer = {
        "\"នេះមើលទៅដូចជាបន្ទប់សុវត្ថិភាព\"",
        "\"តោះ​យើង​គួរតែធ្វើចលនាដោយប្រុងប្រយ័ត្នជាពិសេសបំពង់ឡាស៊ែរទាំងនោះ។\"",
        "\"ខ្ញុំគួរតែចៀសវាងពួកវាឱ្យនៅរស់។\""
    };

    private static string[] stage5English = {
		"The room are getting bigger and bigger the more I move through the elevator.",
		"\"There's a room above me, maybe something is there.\"",
        "\"I might be able to move those platforms if I trigger those levers.\"",
		"\"Let's not forget to follow those suspicious pipe.\""
	};

    private static string[] stage5Khmer = {
        "បន្ទប់កាន់តែធំទៅៗ។",
        "\"មានបន្ទប់មួយនៅពីលើខ្ញុំប្រហែលជាមានអ្វីមួយនៅទីនោះ។\"",
        "\"ខ្ញុំប្រហែលជាអាចបំលាស់ទីក្តារទាំងនោះប្រសិនបើខ្ញុំចុចប៊ូតុង។\""
    };

    private static string[] stage6English = {
        "\"This room seems to have more light!\"",
        "\"Am I closer to being out of this bunker?\"",
        "\"I don't want to touch those lasers, they seem dangerous.\"",
        "\"Better be careful of it!\""
    };

    private static string[] stage6Khmer = {
        "\"បន្ទប់នេះហាក់ដូចជាមានពន្លឺកាន់តែច្រើន\"",
        "\"តើខ្ញុំនៅជិតចេញពីលេនដ្ឋាននេះ?\"",
        "\"ខ្ញុំមិនចង់ប៉ះឡាស៊ែរទាំងនោះទេពួកគេហាក់ដូចជាមានគ្រោះថ្នាក់។\""
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
        "\"បន្ទប់នេះទទេ?\"",
        "ខ្ញុំឃើញទ្វារមួយនៅចម្ងាយ។",
        "\"តោះទៅរកវា។\""
    };

    private static string[] stageCreditsEnglish = {
        "Robot boy made it out to the surface at last",
        "But what awaits him is a ruin from all the war!",
        "Thus, he walks around in the ruins trying to find others",
        "Alas! He have used up all his battery while climbing the bunker.",
        "He lays lifeless, waiting for the day he rots away!",
        "The end...",
        "Thank you for playing <3"
    };

	private static string[] stageCreditsKhmer = {
        "ក្មេងប្រុសរ៉ូបូតបានចេញទៅលើផ្ទៃលើ",
        "ប៉ុន្តែអ្វីដែលកំពុងរង់ចាំគាត់គឺជាការបំផ្លិចបំផ្លាញពីសង្គ្រាមទាំងអស់",
        "ដូច្នេះគាត់ដើរជុំវិញកម្ទេចកម្ទី​ដែលព្យាយាមរកអ្នកដទៃ",
        "គាត់បានប្រើថ្មរបស់គាត់ទាំងអស់នៅពេលឡើងលានលានដ្ឋាន។",
        "គាត់ដេកដោយដោយរង់ចាំថ្ងៃដែលគាត់ស្លាប់",
        "ចប់...",
        "សូមអរគុណសម្រាប់ការលេង"
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
            updatedFont[i] = KhmerToKeyboard.ConvertKhmerToKeyboard(fontKhmer[i]);
        }

        return updatedFont;
    }
}
