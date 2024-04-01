﻿using SKitLs.Bots.Telegram.AdvancedMessages.Model.Messages.Text;
using SKitLs.Bots.Telegram.AdvancedMessages.Prototype;
using SKitLs.Bots.Telegram.BotProcesses.Prototype.Processes;
using SKitLs.Bots.Telegram.Core.Model.DeliverySystem.Prototype;
using SKitLs.Bots.Telegram.Core.Model.UpdatesCasting.Signed;
using System.Reflection;

namespace SKitLs.Bots.Telegram.BotProcesses.Model.Defaults.Processes.Partial
{
    /// <summary>
    /// The running version of the <see cref="PartialSubRunning{TResult}"/>. See it for info.
    /// </summary>
    /// <typeparam name="TResult">The type of the wrapped argument, which must not be nullable.</typeparam>
    public class PartialSubRunning<TResult> : ISubRunning<PartialInputRunning<TResult>> where TResult : notnull
    {
        /// <summary>
        /// Represents the owner (parent) bot running process associated with this sub-process.
        /// </summary>
        public PartialInputRunning<TResult> Owner { get; set; }
        /// <summary>
        /// Represents the bot process definition that launched this running process.
        /// </summary>
        public PartialSubProcess<TResult> Launcher { get; set; }

        /// <summary>
        /// Represents the order of the sub-process within the parent bot running process.
        /// </summary>
        public int SubOrder => Launcher.SubOrder;
        /// <summary>
        /// Represents the handling property of the sub-process.
        /// </summary>
        public PropertyInfo HandlingProperty => Launcher.HandlingProperty;
        /// <summary>
        /// Represents the startup message of the sub-process.
        /// </summary>
        public IOutputMessage StartupMessage => Launcher.StartupMessage;
        /// <summary>
        /// Represents the startup message of the sub-process.
        /// </summary>
        public InputPreviewDelegate<IOutputMessage>? InputPreview => Launcher.InputPreview;
        /// <summary>
        /// Represents the parse input delegate for the sub-process.
        /// </summary>
        public ParseInputDelegate ParseInput => Launcher.ParseInput;

        /// <summary>
        /// Represents the wrapped argument value.
        /// </summary>
        public TResult BuildingInstance => Owner.Arguments.BuildingInstance;

        /// <summary>
        /// Initializes a new instance of the <see cref="PartialSubRunning{TResult}"/> class with the specified owner and launcher.
        /// </summary>
        /// <param name="owner">The owner (parent) bot running process.</param>
        /// <param name="launcher">The bot process definition that launched this running sub-process.</param>
        public PartialSubRunning(PartialInputRunning<TResult> owner, PartialSubProcess<TResult> launcher)
        {
            Owner = owner;
            Launcher = launcher;
        }

        /// <summary>
        /// Handles the input update of type <see cref="SignedMessageTextUpdate"/> for the running sub-process.
        /// </summary>
        /// <param name="update">The update containing the input for the sub-process.</param>
        public async Task HandleInput(SignedMessageTextUpdate update)
        {
            var exceptionMes = InputPreview is not null ? await InputPreview.Invoke(update) : null;
            if (exceptionMes is null)
            {
                var result = await ParseInput(update);
                HandlingProperty.SetValue(BuildingInstance, result);
                await Owner.Valid(update);
                //try
                //{
                //}
                //catch (Exception e)
                //{
                //    // TODO : go message
                //    exceptionMes = new OutputMessageText(e.Message);
                //    if (exceptionMes is not null)
                //        await update.Owner.DeliveryService.AnswerSenderAsync(await exceptionMes.BuildContentAsync(update), update);
                //}
            }
            else
                await update.Owner.DeliveryService.AnswerSenderAsync(await exceptionMes.BuildContentAsync(update), update);
        }
    }
}