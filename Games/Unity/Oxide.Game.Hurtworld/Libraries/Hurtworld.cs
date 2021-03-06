﻿using System.Reflection;
using Oxide.Core;
using Oxide.Core.Libraries;

namespace Oxide.Game.Hurtworld.Libraries
{
    /// <summary>
    /// A library containing utility shortcut functions for Hurtworld
    /// </summary>
    public class Hurtworld : Library
    {
        /// <summary>
        /// Returns if this library should be loaded into the global namespace
        /// </summary>
        /// <returns></returns>
        public override bool IsGlobal => false;

        #region Utility

        /// <summary>
        /// Gets private bindingflag for accessing private methods, fields, and properties
        /// </summary>
        [LibraryFunction("PrivateBindingFlag")]
        public BindingFlags PrivateBindingFlag() => (BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);

        /// <summary>
        /// Converts a string into a quote safe string
        /// </summary>
        /// <param name="str"></param>
        [LibraryFunction("QuoteSafe")]
        public string QuoteSafe(string str) => str.Quote();

        #endregion

        #region Chat

        /// <summary>
        /// Broadcasts a chat message to all players
        /// </summary>
        /// <param name="name"></param>
        /// <param name="message"></param>
        [LibraryFunction("BroadcastChat")]
        public void BroadcastChat(string name, string message = null)
        {
            ConsoleManager.SendLog(string.Concat("[Broadcast] ", message != null ? $"{name} {message}" : name));
            ChatManagerServer.Instance.RPC("RelayChat", uLink.RPCMode.Others, message != null ? $"{name} {message}" : name);
        }

        /// <summary>
        /// Sends a chat message to the player
        /// </summary>
        /// <param name="session"></param>
        /// <param name="name"></param>
        /// <param name="message"></param>
        [LibraryFunction("SendChatMessage")]
        public void SendChatMessage(PlayerSession session, string name, string message = null)
        {
            if (session.Player.isConnected) ChatManagerServer.Instance.RPC("RelayChat", session.Player, message != null ? $"{name} {message}" : name);
        }

        #endregion
    }
}
