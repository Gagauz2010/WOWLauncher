using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;

// Управление общими сведениями о сборке осуществляется с помощью 
// набора атрибутов. Измените значения этих атрибутов, чтобы изменить сведения,
// связанные со сборкой.
[assembly: AssemblyTitle("%YourName%")]
[assembly: AssemblyDescription("Программа запуска World of Wacraft")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Jumper's Lab")]
[assembly: AssemblyProduct("%YaourName% Launcher")]
[assembly: AssemblyCopyright("Copyright © Jumper 2014")]
[assembly: AssemblyTrademark("Jumper's World of Warcraft Launchers")]
[assembly: AssemblyCulture("")]

// Параметр ComVisible со значением FALSE делает типы в сборке невидимыми 
// для COM-компонентов.  Если требуется обратиться к типу в этой сборке через 
// COM, задайте атрибуту ComVisible значение TRUE для этого типа.
[assembly: ComVisible(false)]

//Чтобы начать сборку локализованных приложений, задайте 
//<UICulture>CultureYouAreCodingWith</UICulture> в файле .csproj
//внутри <PropertyGroup>.  Например, если используется английский США
//в своих исходных файлах установите <UICulture> в en-US.  Затем отмените преобразование в комментарий
//атрибута NeutralResourceLanguage ниже.  Обновите "en-US" в
//строка внизу для обеспечения соответствия настройки UICulture в файле проекта.

//[assembly: NeutralResourcesLanguage("en-US", UltimateResourceFallbackLocation.Satellite)]


[assembly: ThemeInfo(
    ResourceDictionaryLocation.None, //где расположены словари ресурсов по конкретным тематикам
    //(используется, если ресурс не найден на странице 
    // или в словарях ресурсов приложения)
    ResourceDictionaryLocation.SourceAssembly //где расположен словарь универсальных ресурсов
    //(используется, если ресурс не найден на странице, 
    // в приложении или в каких-либо словарях ресурсов для конкретной темы)
)]


// Сведения о версии сборки состоят из следующих четырех значений:
//
//      Основной номер версии
//      Дополнительный номер версии 
//   Номер сборки
//      Редакция
//
// Можно задать все значения или принять номера сборки и редакции по умолчанию 
// используя "*", как показано ниже:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("2.4.0.2")]
[assembly: AssemblyFileVersion("2.4.0.2")]
[assembly: GuidAttribute("F37E84CB-D76A-49B1-A1AC-88879903087B")]
