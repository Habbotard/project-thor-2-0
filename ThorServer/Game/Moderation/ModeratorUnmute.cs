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
        private void TextCommandUnmute(string dataRaw, long sessionId, int roomId)
        {
            string commandBody = dataRaw.Substring(dataRaw.IndexOf(' ') + 1);
            RoomUser userInfo = InstanceManager.Game.Rooms.GetRoomInstance(roomId).GetUser(commandBody);

            if (userInfo != null)
            {
                ModeratorUnmute(userInfo, userInfo.SessionID, sessionId, commandBody);
            }
        }

        public void ModeratorUnmute(RoomUser user, long sessionTo, long sessionFrom, string message)
        {
            if (ModHasRight(sessionFrom, "fuse_mute"))
            {
                ExecuteUnmute(sessionTo);
                if (user != null) WhisperResponse("Unmuted user " + user.UserInfo.sUsername, sessionFrom);
                LogCommand(InstanceManager.Sessions.GetSession(sessionFrom).mUserInfo.userId, user.UserInfo.userId, "unmute", message, "Text Command");
            }
        }

        public void ExecuteUnmute(long sessionToMute)
        {
            int roomId = InstanceManager.Sessions.GetSession(sessionToMute).mRoomID;

            if (roomId > 0)
            {
                InstanceManager.Game.Rooms.GetRoomInstance(roomId).RemoveUniqueStatus(sessionToMute, "mute");
            }
        }
    }
}
