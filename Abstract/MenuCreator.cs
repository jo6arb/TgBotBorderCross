using Telegram.Bot.Types.ReplyMarkups;

namespace TgBotBorderCross.Abstract
{
    public class MenuCreator
    {
        public static ReplyKeyboardMarkup MainMenu()
        {
            ReplyKeyboardMarkup mainMenu = new ReplyKeyboardMarkup(true)
                .AddButtons($"Разъяснения по вопросам пропуска через ГГ РФ")
                .AddNewRow($"Разъяснения вопросам пограничного режима")
                .AddNewRow($"Актуальные вакансии");

            return mainMenu;
        }
    }
}
