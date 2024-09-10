using Telegram.Bot.Types.ReplyMarkups;

namespace TgBotBorderCross.Abstract
{
    public class MenuCreator
    {
        public static ReplyKeyboardMarkup MainMenu()
        {
            ReplyKeyboardMarkup mainMenu = new ReplyKeyboardMarkup(true)
                .AddButtons($"Общее по вопросам пропуска через ГГ РФ")
                .AddNewRow($"Разъяснения вопросам пограничного режима")
                .AddNewRow($"Актуальные вакансии");

            return mainMenu;
        }
        public static ReplyKeyboardMarkup MenuPropusk()
        {
            ReplyKeyboardMarkup mainMenu = new ReplyKeyboardMarkup(true)
                .AddButtons($"Разъяснения по вопросам режима в пункте пропуска через ГГ РФ")
                .AddNewRow($"Разъяснения вопросам пропуска через ГГ РФ");

            return mainMenu;
        }

        public static ReplyKeyboardMarkup MenuRegime()
        {
            ReplyKeyboardMarkup mainMenu = new ReplyKeyboardMarkup(true)
                .AddButtons($"Получение пропуска в пункт пропуска через ГГ РФ")
                .AddNewRow($"Отказ в выдаче пропуска через ГГ РФ")
                .AddNewRow($"Сход на берег экипажа заграничного следования");

            return mainMenu;
        }
        public static ReplyKeyboardMarkup MenuPropuskPP()
        {
            ReplyKeyboardMarkup mainMenu = new ReplyKeyboardMarkup(true)
                .AddButtons($"Миграционная карта", "Дети")
                .AddNewRow($"Экипаж морского суда заграничного следования")
                .AddNewRow($"Функционирование пунктов пропуска");

            return mainMenu;
        }

        public static ReplyKeyboardMarkup MenuMigrationCard()
        {
            ReplyKeyboardMarkup mainMenu = new ReplyKeyboardMarkup(true)
                .AddButton($"Получение миграционной карты при следовании иностранного гражданина из Республики Беларусь в Российскую Федерацию")
                .AddNewRow($"Выезд из Российской Федерации без миграционной карты");

            return mainMenu;
        }
        public static ReplyKeyboardMarkup MenuChildren()
        {
            ReplyKeyboardMarkup mainMenu = new ReplyKeyboardMarkup(true)
                .AddButton($"Подверждение гражданства Российской Федерации")
                .AddNewRow($"Выезд из Российской Федерации")
                .AddNewRow($"Заявление о несогласии на выезд из Российской Федерации");

            return mainMenu;
        }
    }
}
