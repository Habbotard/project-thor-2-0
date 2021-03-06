﻿/*
Thor Server Project
Copyright 2008 Joe Hegarty


This file is part of The Thor Server Project.

The Thor Server Project is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

The Thor Server Project is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License for more details.

You should have received a copy of the GNU Affero General Public License
along with The Thor Server Project.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ThorServer.Game.Users;

using ThorServer.Game.Rooms;

namespace ThorServer.Game.Moderation
{
    public partial class ModerationHandler
    {
        private void TextCommandKick(string dataRaw, long sessionId, int roomId)
        {
            string commandBody = dataRaw.Substring(dataRaw.IndexOf(' ') + 1);

            if (commandBody.Contains(' '))
            {
                string user = commandBody.Substring(0, commandBody.IndexOf(' '));
                string messageToSend = commandBody.Substring(commandBody.IndexOf(' ') + 1);
                RoomUser userInfo = InstanceManager.Game.Rooms.GetRoomInstance(roomId).GetUser(user);

                if (userInfo != null)
                {
                    ModeratorKick(userInfo, userInfo.SessionID, sessionId, messageToSend, "Text Command");
                }
            }
        }

        public void ModeratorKick(RoomUser user, long sessionTo, long sessionFrom, string message, string extraInfo)
        {
            if (ModHasRight(sessionFrom, "fuse_kick"))
            {
                int roomId = InstanceManager.Sessions.GetSession(sessionTo).mRoomID;

                if (roomId > 0)
                {
                    ExecuteKick(sessionTo, message, roomId);
                    if (user != null)
                    {
                        WhisperResponse("Kicked user " + user.UserInfo.sUsername, sessionFrom);
                        LogCommand(InstanceManager.Sessions.GetSession(sessionFrom).mUserInfo.userId, user.UserInfo.userId, "kick", message, extraInfo);
                    }
                }
            }
        }



        public void ExecuteKick(long sessionToAlert, string messageToSend, int roomId)
        {
            mMessage.Init(18); // @R
            InstanceManager.Sessions.GetSession(sessionToAlert).SendMessage(mMessage);

            mMessage.Init(33); // @a
            mMessage.appendString("mod_warn/");
            mMessage.appendString(messageToSend);
            InstanceManager.Sessions.GetSession(sessionToAlert).SendMessage(mMessage);

            InstanceManager.Game.Rooms.GetRoomInstance(roomId).RemoveUser(sessionToAlert);
        }
    }
}
