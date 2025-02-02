# SKitLs.Bots.Telegram. Sumup

**Status: work in progress.**


# An idea behind

Since I had discovered [Telgram.Bot](https://github.com/TelegramBots/Telegram.Bot) library I have reduced the time necessary for bot creation in times.
But also I have faced the problem that my code becomes really complex and mostly contains of if-else constructs and repeating checkers for update's nullable content.

At that very moment I decided to review, collect and unify my fragmented code to build one generic solution for the most spreaded problems I have faced.
This library allows you not to waste your time for copy-pasting all the checkers and creating an architecture for your bot, but focus on designing its interior.


# Documentation

Check [project's Wiki](https://github.com/SKitLs-dev/SKitLs.Bots.Telegram/wiki) for the detailed documentation.


# Introduction

This project is a collection of libraries developed in C# with the aim of simplifying the process of creating bots
for interacting with the Telegram messenger using the [Telegram API](https://core.telegram.org/bots/api).
The main emphasis is placed on facilitating code writing and improving its readability.

By itself, the project does not provide any interaction with Telegram through its API and does not directly handle API requests.
The project depends on the [_**Telegram.Bot**_]((https://github.com/TelegramBots/Telegram.Bot)) library, which is used to work with the Telegram API
and provides the basic functionality for processing server messages.

The primary contribution of this project lies in the functionality of typing nullable fields, allowing end-users of the library to create class objects representing specific actions,
rather than relying on numerous if-else constructs.
The internal components of the project handle all necessary checks and data processing, streamlining bot development and simplifying program logic.

Ultimately, this project provides developers using C# with tools to create Telegram bots more quickly and efficiently.
It reduces the amount of code required and enhances program readability by adopting an object-oriented approach and introducing field typing.


## Highlight

Instead of this

```C#
MyBotUser? user = id == 0 ? null : UsersManager.GetData(id);
if (update.Type == UpdateType.CallbackQuery)
{
    string data = update.CallbackQuery.Data;
    if (data.StartsWith("ShowHL."))
    {
        data = data[7..];
        await HighlightCallback(user, data);
    }
}
if (update.Type == UpdateType.Message && update.Message != null && update.Message.Text != null)
{
    string mes = update.Message.Text;
    if (mes == "/me.permanentlydelete")
    {
        UsersManager.Delete(user);
        await Instance.SendMes("��� ������ �������", user.ID);
    }
    else if (mes == "/manageassets" || user.State == State.Managing)
    {
        await ManageAssets(user, mes, id);
    }
    else if (mes == "/profile")
    {
        var message = $"Account {user.InstaName}";
        CancellationToken token = default;

        await Bot.SendTextMessageAsync(
            chatId: user.ID,
                text: message,
                parseMode: null,
                replyMarkup: null,
                cancellationToken: token);
    }
}
```

Do this:

```C#
private static DefaultCommand ProfileCommand => new("profile", Do_ProfileAsync);
private static async Task Do_ProfileAsync(SignedMessageTextUpdate update)
{
    var user = (MyBotUser)update.Sender!;
    var messageText = $"Account {user.InstaName}";
    await new TelegramBotMessage(messageText).AnswerAsync(update);
}
```