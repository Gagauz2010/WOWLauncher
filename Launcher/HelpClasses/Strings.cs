namespace Launcher.HelpClasses
{
    internal class Strings
    {
        public class Info
        {
            public static readonly string NOT_SET = "Не задано";

            public static readonly string PATH_CHOOSE = "Выберите папку с клиентом игры";

            public static readonly string INIT = "Инициализация...";

            public static readonly string UPDATE_IN_PROGRESS = "Идет обновление...";
            public static readonly string UPDATE_DONE = "Игра обновлена";
        }

        public class Error
        {
            public static readonly string CONNECTION_ERROR = "Ошибка подключения";
            public static readonly string CONNECTION_ERROR_EXPLAIN = "Невозможно подключиться к сети интернет, проверьте ваше подключение и повторите попытку";

            public static readonly string PATH_ERROR = "Ошибка расположения";
            public static readonly string PATH_ERROR_EXPLAIN = "Файл \"Wow.exe\" не найден!\nПожалуйста посместите программу в папку с игрой или укажите путь к папке с игрой!\n\nУказать путь сейчас?";

            public static readonly string PATH_CHOOSE_ERROR = "Ошибка выбора папки";
            public static readonly string PATH_CHOOSE_ERROR_EXPLAIN = "В выбранной папке файл \"Wow.exe\" не найден!\nПожалуйста выберите корректную папку с игрой!\n\nПовторить попытку выбора?";
        }

    }
}